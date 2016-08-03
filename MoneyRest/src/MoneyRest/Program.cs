using System;
using System.Collections.Generic;
using System.IO;
using MoneyRest.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoneyRest
{
    public class Program
    {
        private const int TimeOut = 60;             // in seconds

        public static void Main(string[] args)
        {
            // кодировка для конслои
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (args.Length == 0)
                Console.WriteLine("usage: moneyrest input_file, [output_file]");
            else
            {
                WriteWelcome();

                string inFile = args[0];
                string outFile = args.Length == 1 ? Path.Combine(Path.GetDirectoryName(Path.GetFullPath(inFile)), "out.txt") : args[0];

                try
                {
                    IDataProvider<MoneySumm> provider = ProviderFactory.Instance.GetProvider();
                    IEnumerable<MoneySumm> moneySumms = provider?.Read(inFile).ToList();

                    if (moneySumms != null)
                    {
                        Task t = Task.Factory.StartNew(() =>
                        {
                            foreach (MoneySumm ms in moneySumms)
                            {
                                ms.CalculateSummands();
                            }
                        });

                        t.Wait(TimeOut * 1000);

                        provider.Save(moneySumms, outFile);
                        Console.WriteLine("Сформирован файл");
                    }
                }
                catch (AggregateException aex)
                {
                    Console.WriteLine();

                    Console.WriteLine(string.Join("\n", aex.InnerExceptions
                        .Where(ex => ex.Source == Assembly.GetEntryAssembly().GetName().Name)
                        .Select(ex => ex.Message)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                }

                Console.ReadLine();
            }

        }

        private static void WriteWelcome()
        {
            Console.WriteLine("MoneyRest");
            Console.WriteLine("-------------------");
            Console.WriteLine($"Таймаут: {TimeOut} сек");
        }
    }
}
