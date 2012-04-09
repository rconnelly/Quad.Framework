using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Quad.Dto
{
	public class User
	{
		
		[BsonId(IdGenerator = typeof(CombGuidGenerator))]
   		public Guid Id { get; set; }
		
		public string Email { get; set; }
		public string Password { get; set; }
		public string FacebookToken { get; set; }
		public string LinkedInToken { get; set; }
		public string TwitterToken { get; set; }
	
		public User ()
		{
		}
	}
}

