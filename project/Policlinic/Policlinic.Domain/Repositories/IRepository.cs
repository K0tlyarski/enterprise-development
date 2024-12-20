namespace Policlinic.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    public interface IRepository<T, TKey>
    {
        /// <summary>
        /// Вернуть все элементы
        /// </summary>
        /// <returns>Список элементов</returns>
        public Task<List<T>> GetAll();

        /// <summary>
        /// Вернуть элемент по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Элемент или null</returns>
        public Task<T?> Get(TKey id);

        /// <summary>
        /// Удалить элемент по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Результат удаления: true - успешно, false - ошибка</returns>
        public Task<bool> Delete(TKey id);

        /// <summary>
        /// Добавить новый элемент
        /// </summary>
        /// <param name="newObj">Объект для добавления</param>
        public Task Post(T newObj);

        /// <summary>
        /// Обновить элемент по идентификатору
        /// </summary>
        /// <param name="newObj">Объект с обновлёнными данными</param>
        /// <param name="id">Идентификатор элемента</param>
        /// <returns>Результат обновления: true - успешно, false - ошибка</returns>
        public Task<bool> Put(T newObj, TKey id);
    }
}
