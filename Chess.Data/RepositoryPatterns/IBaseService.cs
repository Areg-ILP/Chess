using Chess.Data.Models;
using System.Collections.Generic;

namespace Chess.Data.RepositoryPatterns
{
    /// <summary>
    /// Repository pattern base interface
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IBaseRepository<T> where T : Entity
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>list of entity</returns>
        List<T> GetAll();
        /// <summary>
        /// Get entity
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>entity</returns>
        T Get(int? id);
        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="entity">new entity</param>
        void Create(T entity);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">new entity</param>
        void Update(T entity);
        /// <summary>
        /// Remove entity
        /// </summary>
        /// <param name="id">entity id</param>
        void Remove(int? id);
    }
}
