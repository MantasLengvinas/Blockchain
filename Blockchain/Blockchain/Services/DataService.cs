using Blockchain.Models;
using Hash_algorithm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Services
{
    class DataService
    {
        private readonly HashService HashingService = new HashService();
        public GeneratedData GenerateData(int n)
        {
            Random r = new Random();
            List<User> users = new List<User>();
            List<Transaction> transactions = new List<Transaction>();

            for (int i = 0; i < n; i++)
            {
                users.Add(new User()
                {
                    Name = RandomStringGenerator(r.Next(20)),
                    PublicKey = HashingService.Hash(RandomStringGenerator(r.Next(500)))
                });
            }

            return new GeneratedData() { 
                Users = users,
                Transactions = new List<Transaction>()
            };
        }

        public static string RandomStringGenerator(int l)
        {
            string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string result = "";

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintB = new byte[sizeof(uint)];

                while (l-- > 0)
                {
                    rng.GetBytes(uintB);
                    uint number = BitConverter.ToUInt32(uintB, 0);
                    result += (possibleChars[(int)(number % (uint)possibleChars.Length)]);
                }
            }

            return result;
        }
    }
}
