using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class Security
    {
        private const int NumberOfHashIterations = 10000;
        private const int hashSize = 20;
        private const int saltSize = 16;
        /// <summary>
        /// Takes a password, create a long random salt, create a hash and concatenate them.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptPassword(string password)
        {
            var salt = new byte[saltSize];
            RandomNumberGenerator.Create().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, NumberOfHashIterations);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[hashSize + saltSize];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, hashSize);
            return Convert.ToBase64String(hashBytes);
        }
        public bool ValidatePassword(string originalPassword, string enteredPassword)
        {
            if (String.IsNullOrEmpty(originalPassword) || String.IsNullOrEmpty(enteredPassword))
                return false;
            var enteredPasswordHashBytes = new byte[hashSize + saltSize];
            var saltFromOriginalPassword = new byte[saltSize];
            var hashBytesFromOriginalPassword = Convert.FromBase64String(originalPassword);

            Array.Copy(hashBytesFromOriginalPassword, 0, saltFromOriginalPassword, 0, saltSize);
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltFromOriginalPassword, NumberOfHashIterations);
            var hashBytesFromEnteredPassword = pbkdf2.GetBytes(20);
            Array.Copy(saltFromOriginalPassword, 0, enteredPasswordHashBytes, 0, saltSize);
            Array.Copy(hashBytesFromEnteredPassword, 0, enteredPasswordHashBytes, saltSize, hashSize);
            return hashBytesFromOriginalPassword.SequenceEqual(enteredPasswordHashBytes);
        }
    }
}
