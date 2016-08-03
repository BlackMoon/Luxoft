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
        /// <para>Описание: Пусть у нас есть m целых положительных элементов a[i] и трубеится найти можно ли из них составить сумму равную n. </para>
        /// <para>Создадим массив can[i][k] размера m на n+1. can[i][k] = 1, если мы можем получить каким-то образом суммируя только элементы a[0]...a[i] сумму равную k. </para>
        /// <para>Нас интерисует значение can[m][n], т.е. можем ли мы получить используя элементы a[0]...a[m] сумму n.</para>
        /// <para>Очевидно can[0][k] = 0, для всех k != a[0] и k != 0. can[0][0] = 1 и can[0][a[0]] = 1. Т.е.используя один элемент мы можем получить только сумму равную 0 или этому элементу.</para>
        /// <para>Рассмотрим как по массиву can[i - 1] можно построить can[i]. </para>
        /// <para>Очевидно, что используя i элементов всегда можно получить те же суммы, что и используя i=1 элемент(просто не добавляя к прежним суммам i-й элемент). </para>
        /// <para>Т.е.если can[i - 1][k]=1, то can[i][k]=1 так же.Кроме того, к любой из прежних сумм мы можем добавить i-й элемент, т.е.если can[i-1][k]= 1, то can[i][k+a[i]]=1 так же.</para>
        /// <para>Во всех остальных случаях can[i][k]= 0.</para>
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
        /// <para>Описание: Берется i-й элемент. </para>
        /// <para>Начиная с i-го элемента и до конца множества вычисляется сумма. </para>
        /// <para>Если она не совпадает с базовой происходит увеличение i.</para>
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
