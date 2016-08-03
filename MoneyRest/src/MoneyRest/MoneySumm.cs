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
                    Array.Reverse(Numbers);
                }
            }
        }

        /// <summary>
        /// Найти max ближайшее к числу
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private decimal? FindNearest(decimal value, int startIndex = 0)
        {
            decimal? result = null;

            if (Numbers != null)
            {
                decimal delta = decimal.MaxValue;

                for(int i = startIndex; i < Numbers.Length; i++)
                {
                    decimal num = Numbers[i];
                    decimal newDelta = Math.Abs(value - num);
                    if (newDelta < delta)
                    {
                        result = num;
                        delta = newDelta;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// C из n по k
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private int C(int n, int k)
        {
            return Factorial(n) / Factorial(k) / Factorial(n - k);
        }

        /// <summary>
        /// Факториал
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private int Factorial(int n)
        {
            int res = 1;
            for (var i = n; i > 1; i--)
                res *= i;

            return res;
        }

        private List<decimal> Combination(int index, int k, IEnumerable<decimal> inputs)
        {
            return null;
        } 

        public void CalculateSummands()
        {
            if (Numbers != null)
            {
                for (int i = 0; i < Numbers.Length; i++)
                {
                    decimal num = Numbers[i];
                    decimal summ = 0;
                    IList<decimal> rests = new List<decimal>();

                    // поиск max ближайшего к value
                    summ += num;
                    rests.Add(num);
                    
                    while (summ < Total)
                    {
                        decimal nearest = FindNearest(Total - summ) ?? 0;

                        if (summ + nearest <= Total)
                        {
                            summ += nearest;
                            rests.Add(nearest);
                        }
                        else
                            break;
                    }

                    if (summ == Total)
                    {
                        Summands = rests;
                        break;
                    }

                    /*
                    for (int i = i; i < len; i++)
                    {
                        //decimal nearest = FindNearest(value) ?? 0;
                        summ += nearest;

                        rests.Add(nearest);
                        if (summ == Total)
                        {
                            found = true;  
                            break;
                        }
                        value = Total - summ;
                    }*/
                }
                /*
                for (int ix = 0; ix < len; ix++)
                {
                    bool found = false;
                    decimal summ = 0;

                    IList<decimal> rests = new List<decimal>();
                    for (int i = ix; i < len; i++)
                    {
                        decimal num = Numbers[i];
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
                }*/
            }
        }
    }
}
