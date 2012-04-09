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
	
	public class RepositoryConfiguration
	{
		public MongoServer Server {get;set;}
		public string DBName {get;set;}
		public string ConnectionString {get;set;}
	}
	
	interface IRespository
	{
		User AddUser(User user);
		User GetUserById(Guid id);
		SafeModeResult DeleteUser(Guid id);
		User GetUserByEmail(string email);
	}
		
	public class Repository : IRespository
	{
		private string ConnectionString {get;set;}
		private MongoServer Server {get;set;}
		private MongoDatabase Database {get;set;}
		
		static Repository()
		{
			BsonClassMap.RegisterClassMap<User>();			
		}
		
		public Repository (RepositoryConfiguration config)
		{
			ConnectionString = config.ConnectionString;
			Server = MongoServer.Create(config.ConnectionString);
			Database = Server.GetDatabase(config.DBName);
		}
		
		public User AddUser(User user)
		{			
			using (Server.RequestStart(Database)) 
			{
				MongoCollection<User> users = Database.GetCollection<User>("users");
				users.Insert(user);
				return users.FindOne();
			}
		}
		
		public User GetUserById(Guid id)
		{
			
			using (Server.RequestStart(Database)) 
			{				
				MongoCollection<User> users = Database.GetCollection<User>("users");
				return users.FindOneByIdAs<User>(id);
			}
		}

		public SafeModeResult DeleteUser(Guid id)
		{
			using (Server.RequestStart(Database)) 
			{				
				MongoCollection<User> users = Database.GetCollection<User>("users");
				SafeModeResult result = users.Remove(Query.EQ("_id", id));
				
				return result;
			}
		}
		
		public User GetUserByEmail(string email)
		{
			using (Server.RequestStart(Database)) 
			{
				MongoCollection<User> users = Database.GetCollection<User>("users");
				User user = users.AsQueryable().Select(x => new User(){ Email=x.Email, Id=x.Id  })
					.Where(x => x.Email == email).First();
				return user;
			}
		}
	}
}

