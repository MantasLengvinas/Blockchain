using Blockchain.Models;
using Blockchain.Services;
using Hash_algorithm.Services;
using System;

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
            HashService hashingService = new HashService();
            DataService dataService = new DataService();

            GeneratedData data = dataService.GenerateData(1000);

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{data.Transactions[i].Amount}");
            }
        }
    }
}
