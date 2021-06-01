using Chess.Data.Models;
using Chess.Data.Services;

namespace Chess.Data
{
    /// <summary>
    /// Class for get services, (di)
    /// </summary>
    public static class ServiceHelper
    {
        /// <summary>
        /// method to get service
        /// </summary>
        /// <typeparam name="T">ervice entity type</typeparam>
        /// <returns>service</returns>
        public static IBaseService<T> GetService<T>() where T : Entity
        {
            return new BaseService<T>();
        }
    }
}
