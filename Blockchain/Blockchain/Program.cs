using Blockchain.Models;
using Blockchain.Services;
using Hash_algorithm.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            HashService hashService = new HashService();
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

                Block minedBlock = new Block();

                Parallel.ForEach(candidates, (blockCandidate, state) =>
                {
                    blockCandidate.Mine();

                    foreach (Block c in candidates)
                        if (c.Mined)
                        {
                            minedBlock = c;
                            state.Break();
                            break;
                        }
                });

                int transactionCount = 1;

                if (minedBlock.Mined)
                {
                    List<string> transactions = new List<string>();
                    List<string> failedTransactions = new List<string>();

                    foreach (Transaction t in minedBlock.Transactions)
                    {
                        int sender = data.Users.FindIndex(x => x.PublicKey == t.Sender);
                        int receiver = data.Users.FindIndex(x => x.PublicKey == t.Receiver);

                        if (hashService.Hash(t.Sender + t.Receiver + t.Amount) == t.TransactionID && data.Users[sender].Balance >= t.Amount)
                        {
                            bool transactionSuccess = false;

                            if (data.Users[sender].Balance >= t.Amount)
                            {
                                transactionSuccess = true;
                                data.Users[receiver].Balance += t.Amount;
                                data.Users[sender].Balance -= t.Amount;
                            }

                            if (transactionSuccess)
                            {
                                Console.WriteLine($"#{transactionCount} Sender: {data.Users[sender].PublicKey} Receiver: {data.Users[receiver].PublicKey} Amount: {t.Amount}");
                                transactionCount++;
                            }
                        }
                        else 
                        {
                            failedTransactions.Add(t.TransactionID); 
                        }
                        transactions.Add(t.TransactionID);
                    }
                    data.Transactions.RemoveAll(x => transactions.Contains(x.TransactionID));
                    minedBlock.Transactions.RemoveAll(x => failedTransactions.Contains(x.TransactionID));
                    if (transactionCount != 1)
                    {
                        minedBlock.Transacted = true;

                    }
                }
                if(transactionCount != 1)
                {
                    blocks.Add(minedBlock);
                }
            }

        }
    }
}
