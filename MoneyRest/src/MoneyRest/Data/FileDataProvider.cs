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
        public string FileName { get; private set; }

        public string Separator { get; private set; }

        public FileSystemDataProvider()
        {
            Separator = ";";
        }
       
        public void Save(IEnumerable<MoneySumm> items)
        {
            if (string.IsNullOrEmpty(FileName))
                throw new ArgumentNullException("csvFileName");
            /*
            using (StreamWriter sw = new StreamWriter(FileName))
            {
                /*foreach (Order o in items)
                {
                    string line = string.Join(",", o.Code.ToString(), o.Description, o.Amount.ToString(CultureInfo.InvariantCulture), o.Price.ToString(CultureInfo.InvariantCulture));
                    sw.WriteLine(line);

                    o.State = ObjectState.None;
                }
            }*/
        }

        public IEnumerable<MoneySumm> Read()
        {
            if (string.IsNullOrEmpty(FileName))
                throw new ArgumentNullException("FileName");

            foreach (string line in File.ReadLines(FileName))
            {
                string[] x = line.Split(new []{ Separator }, StringSplitOptions.RemoveEmptyEntries);

                int code;
                if (int.TryParse(x[0], out code))
                {
                    decimal amount;
                    decimal.TryParse(x[2], out amount);

                    decimal price;
                    decimal.TryParse(x[3], out price);
                    
                    yield return new MoneySumm();
                }
            }
        }
    }
}
