
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System;

namespace TuvaletBekcisi{

public class DataAccess
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;
 
        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("gorilla");      
        }
 
        public IEnumerable<User> GetUsers()
        {
            return _db.GetCollection<User>("User").FindAll();
        }
 
 
        public User GetUser(Guid id)
        {
            var res = Query<User>.EQ(p=>p.UserId,id);
            return _db.GetCollection<User>("User").FindOne(res);
        }
 
        public User Create(User p)
        {
            _db.GetCollection<User>("User").Save(p);
            return p;
        }
 
        // public void Update(ObjectId id,User p)
        // {
        //     p.UserId = id;
        //     var res = Query<User>.EQ(pd => pd.Id,id);
        //     var operation = Update<User>.Replace(p);
        //     _db.GetCollection<User>("Products").Update(res,operation);
        // }
        // public void Remove(ObjectId id)
        // {
        //     var res = Query<U>.EQ(e => e.Id, id);
        //     var operation = _db.GetCollection<Product>("Products").Remove(res);
        // }
    }

}