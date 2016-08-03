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
        public long Total { get; }
        

        /// <summary>
        /// Набор чисел для получения суммы Total
        /// </summary>
        public long[] Numbers { get; }


        /// <summary>
        /// Слагаемые для получения суммы Total (может быть пустым)
        /// </summary>
        public IList<long> Summands { get; private set; }

        public MoneySumm(long[] numbers)
        {
            if (numbers != null)
            {
                int len = numbers.Length;

                // 1е число -> сумма
                if (len > 0)
                    Total = numbers[0];

                if (len > 1)
                {
                    Numbers = new long[len - 1];
                    Array.Copy(numbers, 1, Numbers, 0, len - 1);
                }
            }
        }

        /// <summary>
        /// Поиск слагаемых суммы (точный)
        /// </summary>
        private void CombinationsExact()
        {
            long m = Total;
            long[] way = new long[m + 1];
            way[0] = 1;

            foreach (long i in Numbers)
            {
                for (long j = m; j > 0; --j)
                {
                    if (way[j] == 0 && j >= i && way[j - i] != 0)
                        way[j] = i;
                }
            }

            if (way[m] > 0)
            {
                Summands = new List<long>();
                for (long i = m; i != 0; i -= way[i])
                {
                    Summands.Add(way[i]);
                }
            }
        }

        /// <summary>
        /// Поиск слагаемых суммы (неточный)
        /// <para>Для сумм >= Int32.MaxValue</para>
        /// </summary>
        private void CombinationsNonExact()
        {
            int len = Numbers.Length;
            for (int ix = 0; ix < len; ix++)
            {
                bool found = false;
                decimal summ = 0;

                IList<long> rests = new List<long>();
                for (int i = ix; i < len; i++)
                {
                    long num = Numbers[i];
                    if (summ + num <= Total)
                    {
                        summ += num;
                        rests.Add(num);

                        if (summ == Total)
                        {
                            found = true;
                            break;
                        }
                    }
                }

                if (found)
                {
                    Summands = rests;
                    break;
                }
            }
        }

        public void CalculateSummands()
        {
            if (Numbers != null)
            {
                if (Total < Int32.MaxValue)
                    CombinationsExact();
                else
                    CombinationsNonExact();
            }
        }
    }
}
