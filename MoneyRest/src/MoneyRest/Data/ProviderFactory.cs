namespace MoneyRest.Data
{
    /// <summary>
    /// ProviderFactory - фабрика провайдеров
    /// </summary>
    public sealed class ProviderFactory
    {
        private static ProviderFactory _instance;

        public static ProviderFactory Instance => _instance ?? (_instance = new ProviderFactory());

        private ProviderFactory()
        {
        }

        /// <summary>
        /// Получить провайдер
        /// </summary>
        /// <returns>Провайдер</returns>
        public IDataProvider<MoneySumm> GetProvider()
        {
            IDataProvider<MoneySumm> dataProvider = new FileSystemDataProvider();
            return dataProvider;
        }
    }
}
