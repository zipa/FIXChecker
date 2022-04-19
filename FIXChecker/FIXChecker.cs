using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXChecker
{
    public class FIXChecker
    {
        public void Check(string fileName)
        {
            List<FIXDetail> fixEntries = new();
            if(File.Exists(fileName))
            {
                var lines = File.ReadAllLines(fileName).ToList();
                foreach(var line in lines)
                {
                    fixEntries.Add(new FIXDetail(line));
                    FIXChain.Add(fixEntries.Last());
                };

                FIXChain.FIXChains.ForEach(chain =>
                {
                    Console.Write(chain.Order + "\x09");
                    foreach(var id in chain.ChainIds)
                    {
                        Console.Write(id + ", ");
                    }
                    Console.Write("\x09");
                    Console.WriteLine($"{chain.LastStatus}\x09{chain.Timestamp}.");
                });
            }
            else
            {
                Console.WriteLine($"File {fileName} does not exist.");
            }
        }
    }
}
