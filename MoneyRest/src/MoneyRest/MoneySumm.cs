using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyRest
{
    public class MoneySumm
    {
        /// <summary>
        /// Итоговая сумма
        /// </summary>
        public decimal Total { get; }
        

        /// <summary>
        /// Набор чисел для получения суммы Total
        /// </summary>
        public decimal[] Numbers { get; }


        /// <summary>
        /// Слагаемые для получения суммы Total (может быть пустым)
        /// </summary>
        public IList<decimal> Summands { get; private set; }

        public MoneySumm(decimal[] numbers)
        {
            if (numbers != null)
            {
                int len = numbers.Length;

                // 1е число -> сумма
                if (len > 0)
                    Total = numbers[0];

                if (len > 1)
                {
                    Numbers = new decimal[len - 1];
                    Array.Copy(numbers, 1, Numbers, 0, len - 1);
                    Array.Sort(Numbers);
                }
            }
        }

        public void CalculateSummands()
        {
            if (Numbers != null)
            {
                int len = Numbers.Length;

                for (int ix = 0; ix < len; ix++)
                {
                    bool found = false;
                    decimal summ = 0;

                    IList<decimal> rests = new List<decimal>();
                    for (int i = ix; i < len; i++)
                    {
                        summ += Numbers[i];
                        rests.Add(Numbers[i]);

                        if (summ >= Total)
                        {
                            if (summ == Total)
                                found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        Summands = rests;
                        break;
                    }
                }
            }
        }
    }
}
