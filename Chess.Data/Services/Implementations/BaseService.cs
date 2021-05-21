using Chess.Data.Models;
using Chess.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Chess.Data.Services.Implementations
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        private const string _dataPath = @"C:\Users\Admin\source\repos\ChessILP\JsonTest\bin\Debug\net5.0\UserData.json";

        public T Get(int? id)
        {
            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            return jsonEntites.Where(e => e.Id == id).FirstOrDefault();
        }

        public List<T> GetAll()
        {
            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            return jsonEntites;
        }

        public void Create(T entity)
        {
            if (entity == null)
                throw new NullReferenceException();

            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            jsonEntites.Add(entity);
            var jsonEntitesString = JsonConvert.SerializeObject(jsonEntites,Formatting.Indented);

            File.WriteAllText(_dataPath,jsonEntitesString);
        }

        public void Remove(T entity)
        {
            if(entity == null)
                throw new NullReferenceException();

            var jsonEntites = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(_dataPath));
            jsonEntites.Remove(jsonEntites.Find(e => e.Id == entity.Id));
            var jsonEntitiesString = JsonConvert.SerializeObject(jsonEntites, Formatting.Indented);
            File.WriteAllText(_dataPath, jsonEntitiesString);
        }

        public void Update(T entity)
        {
            Remove(entity);
            Create(entity);
        }
    }
}
