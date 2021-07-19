using Chess.Data.Models;
using Chess.Data.RepositoryPatterns;

namespace Chess.Data
{
    /// <summary>
    /// Class for get services, (di)
    /// </summary>
    public static class RepositoryHelper
    {
        /// <summary>
        /// method to get service
        /// </summary>
        /// <typeparam name="T">ervice entity type</typeparam>
        /// <returns>service</returns>
        public static IBaseRepository<T> GetRepository<T>() where T : Entity
        {
            return new BaseRepository<T>();
        }
    }
}
