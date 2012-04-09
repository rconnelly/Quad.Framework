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
		
		public T Add<T>(T dto) where T : IDto 
		{			
			using (Server.RequestStart(Database)) 
			{
				MongoCollection<T> dtos = Database.GetCollection<T>(dto.TableName);
				dtos.Insert(dto);
				return dtos.FindOne();
			}
		}
		
		public static T CreateInstance<T>()
		{
			Type d1 = typeof(T);
 			//Type[] typeArgs = { typeof(string) };
			Type t = d1.MakeGenericType();
		 	return (T)Activator.CreateInstance(t);
		}
		
		public string GetTableName<T>() where T : IDto
		{
			T t = Repository.CreateInstance<T>();
			string tableName =  t.TableName;
			return tableName;
		}
		
		public T GetById<T>(Guid id) where T : IDto 
		{
			using (Server.RequestStart(Database)) 
			{				
				MongoCollection<T> dtos = Database.GetCollection<T>(GetTableName<T>());
				return dtos.FindOneByIdAs<T>(id);
			}
		}

		public SafeModeResult Delete<T>(Guid id) where T : IDto 
		{
			using (Server.RequestStart(Database)) 
			{	
				MongoCollection<T> dtos = Database.GetCollection<T>(GetTableName<T>());
				SafeModeResult result = dtos.Remove(Query.EQ("_id", id));
				return result;
			}
		}
		
		public IQueryable Fetch<T>(MongoDatabase db) where T : IDto
		{
			MongoCollection<T> dtos = db.GetCollection<T>(GetTableName<T>());
			return dtos.AsQueryable();
		}
	}
}

