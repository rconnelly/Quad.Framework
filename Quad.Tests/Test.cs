// /* 
//     Copyright Quad IO, inc 2012
// */
using System;
using NUnit.Framework;
using Quad.Data;
using Quad.Dto;

namespace Quad.Tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void add_user_returns_valid_user_with_id ()
		{
			var dbName = "quad";
			var connectionString = "mongodb://localhost";
			var server = MongoDB.Driver.MongoServer.Create (connectionString);
			var dStore = new Repository (new RepositoryConfiguration () {DBName = dbName,
								ConnectionString=connectionString,Server = server});
			var user = dStore.Add(new User () { Email="test@test.com" });
			Assert.IsNotNull (user.Id);
			
			var newUser = dStore.GetById<User>(user.Id);
			
			Assert.IsNotNull (user);
			
			Assert.AreEqual (user.Email, newUser.Email);
			Assert.AreEqual (user.Id, newUser.Id);
			
			dStore.Delete<User>(user.Id);
			
			Assert.IsNull (dStore.GetById<User>(user.Id));
		}
	}
}

