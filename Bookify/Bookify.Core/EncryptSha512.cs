using System;
using System.Security.Cryptography;
using System.Text;

namespace Bookify.Core
{
    public class EncryptSha512
    {
        private static string Encrypt(string input, byte[] saltBytes)
        {
            // get input bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // create salt + input array
            byte[] saltAndInput = new byte[saltBytes.Length + inputBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, saltAndInput, 0, saltBytes.Length);
            Buffer.BlockCopy(inputBytes, 0, saltAndInput, saltBytes.Length, inputBytes.Length);

            // hash the salt and input
            byte[] data = GetHash(saltAndInput);

            // append the salt length and raw salt to hashed data
            byte ss = Convert.ToByte(saltBytes.Length);
            byte[] finalBytes = new byte[1 + saltBytes.Length + data.Length];
            finalBytes[0] = ss;
            Buffer.BlockCopy(saltBytes, 0, finalBytes, 1, saltBytes.Length);
            Buffer.BlockCopy(data, 0, finalBytes, saltBytes.Length + 1, data.Length);

            // convert to base64 string
            string hash = ByteArrayToString(finalBytes);
            return hash;
        }

        private static byte[] GetHash(byte[] input)
        {
            using (HashAlgorithm ha = new SHA512CryptoServiceProvider())
            {

                // copy into the return byte array
                byte[] data = new byte[input.Length];
                Array.Copy(input, data, input.Length);

                // process this atleast 1000 times
                for (int i = 0; i < 1000; i++)
                {
                    // Convert the input string to a byte array and compute the hash.
                    data = ha.ComputeHash(data);
                }
                return data;
            }
        }

        private static byte[] GetSaltBytes(string input, int saltSize)
        {
            byte[] saltBytes = null;
            // get some random bytes
            using (Rfc2898DeriveBytes rdb = new Rfc2898DeriveBytes(input, saltSize, 1000))
            {
                // get salt
                saltBytes = rdb.Salt;
            }
            return saltBytes;
        }

        private static int GetTrueRandomNumber()
        {
            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // return a random number between 8 and 24
            return random.Next(8, 24);
        }

        public static string GetPassword(string input)
        {
            // get random salt size
            int saltSize = GetTrueRandomNumber();

            // get random salt bytes
            byte[] saltBytes = GetSaltBytes(input, saltSize);

            // using this totally random and variable length salt
            // generate a crypto hash of the input string
            string hash = Encrypt(input, saltBytes);
            return hash;
        }



        public static bool VerifyPassword(string input, string encPassword)
        {
            // convert encrypted password to bytes
            byte[] finalBytes = StringToByteArray(encPassword);
        
            // get salt size
            int saltSize = Convert.ToInt32(finalBytes[0]);

            // now get raw salt 
            byte[] saltBytes = new byte[saltSize];
            Array.Copy(finalBytes, 1, saltBytes, 0, saltSize);

            // using this recovered salt
            // generate a crypto hash of the input string
            string hash = Encrypt(input, saltBytes);

            // check for match
            return (string.CompareOrdinal(encPassword, hash) == 0);
        }

        public static string ByteArrayToString(byte[] value)
        {
            var byteArray = (byte[])value;
            return Convert.ToBase64String(byteArray);
        }

        public static  byte[] StringToByteArray(string value)
        {
            var s = (string)value;
            return Convert.FromBase64String(s);
        }
    }
}
