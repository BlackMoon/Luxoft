using System.Collections.Generic;

namespace MoneyRest.Data
{
    /// <summary>
    /// Интерфейс провайдера данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataProvider<T> where T : class
    {
        /// <summary>
        /// Сохранить записи
        /// </summary>
        /// <param name="items"></param>
        /// <param name="path"></param>
        void Save(IEnumerable<T> items, string path);

        /// <summary>
        /// Чтение записей
        /// </summary>
        /// <param name="path"></param>
        /// <returns>IEnumerable<T></returns>
        IEnumerable<T> Read(string path);
    }
}
