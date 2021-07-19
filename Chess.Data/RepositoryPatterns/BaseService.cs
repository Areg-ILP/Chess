using Chess.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Chess.Data.RepositoryPatterns
{
    /// <summary>
    /// Base Repository abstraction
    /// </summary>
    /// <typeparam name="T">entity</typeparam>
    internal class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        /// <summary>
        /// json file path
        /// </summary>
        private const string _dataPath = @"UserData.json";
        
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">entity id</param>
        /// <returns>entity</returns>
        public T Get(int? id)
        {
            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            return jsonEntites.Where(e => e.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>list of entites</returns>
        public List<T> GetAll()
        {
            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            return jsonEntites;
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity">new entity</param>
        public void Create(T entity)
        {
            if (entity == null)
                throw new NullReferenceException();

            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            jsonEntites.Add(entity);
            var jsonEntitesString = JsonConvert.SerializeObject(jsonEntites,Formatting.Indented);

            File.WriteAllText(_dataPath,jsonEntitesString);
        }

        /// <summary>
        /// Remove entity by id
        /// </summary>
        /// <param name="id">entity id</param>
        public void Remove(int? id)
        {
            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            jsonEntites.Remove(jsonEntites.Find(e => e.Id == id));
            var jsonEntitiesString = JsonConvert.SerializeObject(jsonEntites, Formatting.Indented);
            File.WriteAllText(_dataPath, jsonEntitiesString);
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">new entity</param>
        public void Update(T entity)
        {
            Remove(entity.Id);
            Create(entity);
        }
    }
}
