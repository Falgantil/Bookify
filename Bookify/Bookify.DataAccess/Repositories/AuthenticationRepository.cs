using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

using Bookify.Common.Commands.Auth;
using Bookify.Common.Exceptions;
using Bookify.Common.Models;
using Bookify.Common.Repositories;
using Bookify.DataAccess.Models;

namespace Bookify.DataAccess.Repositories
{
    public class AuthenticationRepository : GenericRepository<Person>, IAuthenticationRepository
    {
        public AuthenticationRepository(BookifyContext context)
            : base(context)
        {
        }

        public async Task<AuthTokenDto> Login(AuthenticateCommand command)
        {
            var queryable = await this.Get(p => p.Email == command.Email);
            var person = await queryable.SingleOrDefaultAsync();
            if (person == null || !EncryptSha512.VerifyPassword(command.Password, person.Password))
            {
                throw new InvalidCredentialsException("Invalid email & password");
            }

            var now = DateTimeOffset.Now.ToUniversalTime();
            var unixNow = now.ToUnixTimeSeconds();
            var unixExpired = now.AddYears(1).ToUnixTimeSeconds();
            var payload = new Dictionary<string, object>
            {
                { "issdate", unixNow },
                { "expdate", unixExpired },
                { "userid", person.Id }
            };
            var token = JWT.JsonWebToken.Encode(payload, SecretKey, JWT.JwtHashAlgorithm.HS256);
            return new AuthTokenDto
            {
                Token = token,
                Role = person.Roles.Select(x=>x.ToPersonRoleDto())
            };
        }

        public async Task<PersonDto> Register(CreateAccountCommand command)
        {
            var queryable = await this.Get(p => p.Email == command.Email);
            var person = await queryable.FirstOrDefaultAsync();
            if (person != null)
            {
                throw new BadRequestException("An account already exists with the specified email.");
            }

            var entity = new Person
            {
                Firstname = command.FirstName,
                Lastname = command.LastName,
                Email = command.Email,
                Password = EncryptSha512.GetPassword(command.Password),
                Alias = command.Username
            };
            entity = await this.Add(entity);
            return entity.ToDto();
        }

        private const string SecretKey = "yuoypr3QeRZkwGcfj24y4XGODwnkXOy1";

        public async Task<Person> VerifyToken(string accessToken)
        {
            var obj = JWT.JsonWebToken.DecodeToObject<Dictionary<string, object>>(accessToken, SecretKey);
            var issuedDate = DateTimeOffset.FromUnixTimeSeconds((long)obj["issdate"]);
            if (issuedDate > DateTimeOffset.Now)
            {
                throw new InvalidAccessTokenException();
            }

            var expirationDate = DateTimeOffset.FromUnixTimeSeconds((long)obj["expdate"]);
            if (expirationDate.ToLocalTime() < DateTimeOffset.Now)
            {
                throw new InvalidAccessTokenException();
            }

            var userId = (int)obj["userid"];
            return await this.Find(userId);
        }
    }
}
