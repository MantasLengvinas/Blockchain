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
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string hash = "";
            char[] hexChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            UInt64 sum = 0;

            char prev = input[0];

            foreach(char c in input)
            {
                UInt64 Cint64 = Convert.ToUInt32(c);
                Cint64 = BitOperations.RotateLeft(Cint64, 9845320 * Convert.ToInt32(c));
                Cint64 = Cint64 ^ c ^ prev;

                sum += Cint64 - BitOperations.RotateRight((UInt64)prev, 4056840 * Convert.ToInt32(c));
                sum = BitOperations.RotateLeft(sum, 5788480 * Convert.ToInt32(prev));
                prev = c;

            }

            char[] sumInChars = sum.ToString().ToCharArray();

            for (int i = 0; i < 64; i++)
            {
                int salt;

                if (sumInChars.Length > i)
                    salt = sumInChars[i] + i * 9845320;
                else salt = sumInChars[i % sumInChars.Length] + i * 4056840;

                if (salt >= hexChars.Length)
                    salt = salt % hexChars.Length;

                if (i != 0)
                    hash += hexChars[salt];
                else hash += hexChars[1];
            }

            return hash;
        }
    }
}
