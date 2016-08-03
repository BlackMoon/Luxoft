using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyRest.Data
{
    /// <summary>
    /// FileSystemDataProvider
    /// </summary>
    public class FileSystemDataProvider : IDataProvider<MoneySumm>
    {
        private const string No = "NO";

        public string Separator { get; }

        public FileSystemDataProvider()
        {
            Separator = ";";
        }
       
        public void Save(IEnumerable<MoneySumm> items, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            using (FileStream fs = File.OpenWrite(path))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (MoneySumm ms in items)
                    {
                        string line = No;

                        if (ms.Summands != null && ms.Summands.Any())
                            line = string.Join(";", ms.Summands);

                        sw.WriteLine(line);
                    }
                }
            }
        }

        public IEnumerable<MoneySumm> Read(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            foreach (string line in File.ReadLines(path))
            {
                string[] x = line.Split(new []{ Separator }, StringSplitOptions.RemoveEmptyEntries);
                decimal [] numbers = new decimal[x.Length];
                for (var i = 0; i < x.Length; i++)
                {
                    numbers[i] = Convert.ToDecimal(x[i]);
                }
                    
                yield return new MoneySumm(numbers);
            }
        }
    }
}
