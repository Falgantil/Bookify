﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bookify.API.Controllers
{
    public class PersonsController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> Create()
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Update(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Subscribe(int id)
        {
            return Ok();
        }
    }
}