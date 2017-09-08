
using System;
using System.Security.Cryptography;

namespace BLL
{
    public static class Utilities
    {
        public static Guid GenerateGuid()
        {
            using (var provider = RandomNumberGenerator.Create())
            {
                var bytes = new byte[16];
                provider.GetBytes(bytes);

                return new Guid(bytes);
            }
        }
    }
}