using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Quad.Dto
{
	public class User
	{
		public string Name {get;set;}
		
		[BsonId(IdGenerator = typeof(CombGuidGenerator))]
   		public Guid Id { get; set; }
		public User ()
		{
		}
	}
}

