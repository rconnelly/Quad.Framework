using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Quad.Dto
{
	public class UserResponse : Response
	{
		public User User { get; set; }
	}
}

