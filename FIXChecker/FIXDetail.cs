using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXChecker
{
    public class FIXDetail
    {
        public Dictionary<string, string> Tags = new();
        public FIXDetail(string fixEntry)
        {
            fixEntry.Split('|').ToList().ForEach(tag =>
            {
                var kvp = tag.Split('=');
                if (kvp.Length == 2)
                {
                    Tags.Add(kvp[0], kvp[1]);
                }
            });
        }
    }
}