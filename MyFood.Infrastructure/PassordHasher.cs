﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MyFood.Infrastructure
{
    public class PassordHasher : IPasswordHasher
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iteration = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimeter = ';';
        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iteration, _hashAlgorithmName, KeySize);

            return String.Join(Delimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(Delimeter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iteration, _hashAlgorithmName, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
