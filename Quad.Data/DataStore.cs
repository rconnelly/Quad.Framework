using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using FluentMongo.Linq;
using System.Linq;

using Quad.Dto;

namespace Quad.Data
{
	public class DataStore
	{
		public static string ConnectionString { get { return "mongodb://localhost"; } } 
		public static string DbName { get { return "quad"; } } 
		
		static DataStore()
		{
			BsonClassMap.RegisterClassMap<User>();
		}
		
		public DataStore ()
		{
		}
		
		public User AddUser(User user)
		{
			MongoServer server = MongoServer.Create(ConnectionString);
			MongoDatabase db = server.GetDatabase(DbName);
			
			using (server.RequestStart(db)) {
				MongoCollection<User> users = db.GetCollection<User>("users");
				users.Insert(user);
				return users.FindOne();
			}
		}
		
		public User GetUserById(Guid id)
		{
			MongoServer server = MongoServer.Create(ConnectionString);
			MongoDatabase db = server.GetDatabase(DbName);
			
			using (server.RequestStart(db)) {				
				MongoCollection<User> users = db.GetCollection<User>("users");
				//var q = Query.EQ("Id",id);
				return users.FindOneByIdAs<User>(id);
			}
		}
		
		public User GetUserByName(string name)
		{
			MongoServer server = MongoServer.Create(ConnectionString);
			MongoDatabase db = server.GetDatabase(DbName);
			
			using (server.RequestStart(db)) {				
				MongoCollection<User> users = db.GetCollection<User>("users");
				var user = users.AsQueryable().Select(x => new User(){ Name=x.Name, Id=x.Id  }).First(x => x.Name == name);
				return user;
                //.QueryDump(Log)
                //.Select(x => x.Name)
                //.ToList();
			}
		}
	}
}

