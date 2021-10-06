using Hash_algorithm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Models
{
    class Block
    {
        private readonly HashService HashService = new HashService();

        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Version { get; set; }
        public string MerkleHash { get; }
        public int Nonce { get; set; } = 0;
        public int Difficulty { get; set; }
        public bool Mined { get; set; } = false;
        public bool Transacted { get; set; } = false;
        public List<Transaction> Transactions { get; set; }

        // For empty initializiation
        public Block() { }

        public Block(string previousHash, DateTime timeStamp, string version, int difficulty, List<Transaction> transactions)
        {
            PreviousHash = previousHash;
            TimeStamp = timeStamp;
            Version = version;
            Difficulty = difficulty;
            Transactions = transactions;

            List<string> hashes = new List<string>();
            foreach (Transaction t in Transactions)
            {
                hashes.Add(HashService.Hash(t.TransactionID));
            }

            MerkleHash = CreateMerkleHash(hashes);

            Hash = HashService.Hash(HashService.Hash(PreviousHash + TimeStamp + MerkleHash) + Nonce);
        }

        private string CreateMerkleHash(List<string> h)
        {
            if (h == null || !h.Any())
                return string.Empty;

            if (h.Count == 1)
                return h.First();

            if (h.Count % 2 > 0)
                h.Add(h.Last());

            List<string> merkle = new List<string>();

            for (int i = 0; i < h.Count; i += 2)
            {
                string leafPair = string.Concat(h[i], h[i + 1]);
                merkle.Add(HashService.Hash(HashService.Hash(leafPair)));
            }

            return CreateMerkleHash(merkle);
        }

        public void Mine()
        {
            int nonce = 0;
            string guessHash = HashService.Hash(HashService.Hash(PreviousHash + TimeStamp + Version + MerkleHash) + nonce);

            StringBuilder rebuildHash = new StringBuilder(Hash);
            for (int i = 0; i < Difficulty; i++)
                rebuildHash[i] = '0';
            string rebuildedHash = rebuildHash.ToString();

            while (!Mined)
            {
                if (guessHash.CompareTo(rebuildedHash) > 0)
                {
                    nonce++;
                    guessHash = HashService.Hash(HashService.Hash(PreviousHash + TimeStamp + Version + MerkleHash) + nonce);
                }
                else
                {
                    Mined = true;
                    Nonce = nonce;
                }
            }
        }
    }
}
