using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Models
{
    class Transaction
    {
        public string TransactionID { get; }
        public string SentFrom { get; set; }
        public string SentTo { get; set; }
    }
}
