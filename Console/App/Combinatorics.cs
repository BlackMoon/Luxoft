namespace App
{
    /// <summary>
    /// Комбинаторика
    /// </summary>
    public static class Combinatorics
    {
        /// <summary>
        /// C из n по k
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int C(int n, int k)
        {
            return Factorial(n) / Factorial(k) / Factorial(n - k);
        }


        /// <summary>
        /// Факториал
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Factorial(int n)
        {
            int f = 1;
            for (var i = n; i > 1; i--)
                f *= i;

            return f;
        }
    }
}
