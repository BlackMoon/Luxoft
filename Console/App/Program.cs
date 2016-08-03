using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MoneyRest;
using MoneyRest.Data;

namespace App
{
    class Program
    {
        private const int TimeOut = 60; // in seconds

        static void Main(string[] args)
        {
            if (args.Length == 0)
                Console.WriteLine("usage: app input_file, [output_file]");
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
                        CancellationTokenSource cts = new CancellationTokenSource();
                        CancellationToken token = cts.Token;
                        cts.CancelAfter(TimeOut * 1000);
                        
                        Task.Factory.StartNew(() =>
                        {
                            foreach (MoneySumm ms in moneySumms)
                            {
                                if (token.IsCancellationRequested)
                                    return;
                                
                                ms.CalculateSummands();
                            }
                        }, token);

                        provider.Save(moneySumms, outFile);
                        Console.WriteLine($"Сформирован файл: {outFile}");
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
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        private static void WriteWelcome()
        {
            Console.WriteLine("MoneyRest");
            Console.WriteLine("-------------------");
            Console.WriteLine($"Таймаут: {TimeOut} сек");
        }
    }
}
