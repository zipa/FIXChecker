using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXChecker
{
    public class FIXChain
    {
        public readonly static List<FIXChain> FIXChains = new();

        public static void Add(FIXDetail fix)
        {
            string id = string.Empty;
            string id2 = string.Empty;
            string lastStatus = string.Empty;
            string timestamp = string.Empty;
            string order = string.Empty;
            FIXChain? chain = null;

            if (fix.Tags.ContainsKey("11"))
            {
                id = fix.Tags["11"];
            }
            if (fix.Tags.ContainsKey("41"))
            {
                id2 = fix.Tags["41"];
            }

            if (fix.Tags.ContainsKey("39"))
            {
                lastStatus = fix.Tags["39"];
            }

            if (fix.Tags.ContainsKey("52"))
            {
                timestamp = fix.Tags["52"];
            }

            if (fix.Tags.ContainsKey("17"))
            {
                order = fix.Tags["17"];
            }

            if (FIXChains.Where(x => x.ChainIds.Contains(id)).Any())
            {
                chain = FIXChains.Where(x => x.ChainIds.Contains(id)).FirstOrDefault();
                if (chain != null)
                {
                    if(string.IsNullOrEmpty(chain.Order))
                    {
                        chain.Order = order;
                    }
                    chain.LastStatus = lastStatus;
                    chain.Timestamp = timestamp;
                }
            }
            else if (FIXChains.Where(x => x.ChainIds.Contains(id2)).Any())
            {
                chain = FIXChains.Where(x => x.ChainIds.Contains(id2)).FirstOrDefault();
                if (chain != null)
                {
                    chain.ChainIds.Add(id);
                    if (string.IsNullOrEmpty(chain.Order))
                    {
                        chain.Order = order;
                    }
                    chain.LastStatus = lastStatus;
                    chain.Timestamp = timestamp;
                }
            }
            else
            {
                chain = new(id, order, lastStatus, timestamp);
                FIXChains.Add(chain);
            }
        }

        public List<string> ChainIds { get; set; } = new();
        public string Order { get; set; }
        public string LastStatus { get; set; }
        public string Timestamp { get; set; }

        public FIXChain(string id, string order, string lastStatus, string timestamp)
        {
            ChainIds.Add(id);
            Order = order;
            LastStatus = lastStatus;
            Timestamp = timestamp;
        }
    }
}
