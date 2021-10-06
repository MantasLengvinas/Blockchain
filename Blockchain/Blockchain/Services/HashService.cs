using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hash_algorithm.Services
{
    class HashService
    {
        public string Hash(string input)
        {
            string hash = "";
            char[] hexChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            UInt64 sum = 0;

            foreach(char c in input)
            {
                UInt64 inputValue = Convert.ToUInt32(c);
                inputValue = BitOperations.RotateRight(inputValue, 70403 * Convert.ToInt32(c));
                inputValue = c ^ ~inputValue ^ c;

                sum += inputValue - BitOperations.RotateLeft((UInt64)c, 30509 * Convert.ToInt32(c));
                sum = BitOperations.RotateLeft(sum, 309967 * Convert.ToInt32(c));
            }

            char[] sumInChars = sum.ToString().ToCharArray();

            for (int i = 0; i < 64; i++)
            {
                int index;

                if (sumInChars.Length > i)
                    index = sumInChars[i] + (i + 1) * 30509;
                else index = sumInChars[i % sumInChars.Length] + (i + 1) * 70403;

                if (index + i >= hexChars.Length)
                    index %= hexChars.Length;

                hash += hexChars[index];
            }

            return hash;
        }
    }
}
