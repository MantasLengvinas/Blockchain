using Blockchain.Models;
using Blockchain.Services;
using Hash_algorithm.Services;
using System;
using System.Collections.Generic;

namespace Blockchain
{
    class Program
    {

        //if (Mantas kutena && neklauso && nevalgo) 
        //    mantas.MiegotiAntZemes()
        //else
        //    continue;

        static void Main(string[] args)
        {
            Random r = new Random();
            HashService hashingService = new HashService();
            DataService dataService = new DataService();

            GeneratedData data = dataService.GenerateData(1000);
            List<Block> blocks = new List<Block>();

            Console.WriteLine("Started mining..");

            while(data.Transactions.Count > 0)
            {
                List<Block> candidates = new List<Block>();

                for(int i = 0; i < 3; i++)
                {
                    string previousHash = "";

                    if(blocks.Count == 0)
                    {
                        previousHash = new string('0', 64);
                    }
                    else
                    {
                        previousHash = blocks[blocks.Count - 1].Hash;
                    }

                    int count;
                    if (data.Transactions.Count >= 1000)
                    {
                        count = r.Next(1000) + 1;
                    }
                    else
                    {
                        count = data.Transactions.Count;
                    }

                    candidates.Add(new Block(previousHash, DateTime.Now, "1", 1, data.Transactions.GetRange(0, count)));
                }
            }

        }
    }
}
