using System;
using System.IO;
using MoneyRest.Data;

namespace MoneyRest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
                Console.WriteLine("usage: moneyrest input_file, [output_file]");

            string inFile = args[0];
            string outFile = args.Length == 1 ? Path.Combine(Path.GetFullPath(inFile), "out.txt") : args[0];

            IDataProvider<MoneySumm> provider = ProviderFactory.Instance.GetProvider();

            Console.ReadLine();


        }
    }
}
