using Hash_algorithm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Models
{
    class Transaction
    {
        private readonly HashService HashingService = new HashService();

        public string TransactionID { get; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public double Amount { get; set; }

        public Transaction(string sender, string receiver, double amount)
        {
            Sender = sender;
            Receiver = receiver;
            Amount = amount;

            TransactionID = HashingService.Hash(sender + receiver + amount.ToString());
        }
    }
}
