using ARVTech.Shared.Security.Interfaces;
using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ARVTech.Shared.Security.Implementations
{
    public class Argon2IdPasswordHasher : IPasswordHasher
    {
        private readonly IPepperProvider _pepperProvider;

        public Argon2IdPasswordHasher(IPepperProvider pepperProvider)
        {
            this._pepperProvider = pepperProvider;
        }

        public string Hash(string password)
        {
            var pepper = this._pepperProvider.GetPepper();

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var pepperBytes = Encoding.UTF8.GetBytes(pepper);

            var combined = Combine(passwordBytes, pepperBytes);

            var salt = RandomNumberGenerator.GetBytes(16);

            var argon2 = new Argon2id(
                combined)
            {
                Salt = salt,
                DegreeOfParallelism = 2,
                MemorySize = 65536,
                Iterations = 3
            };

            var hash = argon2.GetBytes(32);

            return $"$argon2id$v=19$m=65536,t=3,p=2${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string storedHash)
        {
            var pepper = _pepperProvider.GetPepper();

            var parts = storedHash.Split('$');

            var parameters = parts[3]; // m=...,t=...,p=...
            var salt = Convert.FromBase64String(parts[4]);
            var hash = Convert.FromBase64String(parts[5]);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var pepperBytes = Encoding.UTF8.GetBytes(pepper);

            var combined = Combine(passwordBytes, pepperBytes);

            var argon2 = new Argon2id(combined)
            {
                Salt = salt,
                DegreeOfParallelism = Extract(parameters, "p"),
                MemorySize = Extract(parameters, "m"),
                Iterations = Extract(parameters, "t")
            };

            var newHash = argon2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(hash, newHash);
        }

        private static byte[] Combine(byte[] a, byte[] b)
        {
            var result = new byte[a.Length + b.Length];

            Buffer.BlockCopy(a, 0, result, 0, a.Length);
            Buffer.BlockCopy(b, 0, result, a.Length, b.Length);

            return result;
        }

        private static int Extract(string parameters, string key)
        {
            var parts = parameters.Split(',');

            foreach (var part in parts)
            {
                var kv = part.Split('=');

                if (kv[0] == key)
                    return int.Parse(kv[1]);
            }

            throw new Exception("Parâmetro inválido");
        }
    }
}