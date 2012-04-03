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
			var dStore = new DataStore();
			var user = dStore.AddUser(new User() { Name="Test User" });
			Assert.IsNotNull(user.Id);
			
			var newUser = dStore.GetUserById(user.Id);
			
			Assert.IsNotNull(user);
			
			Assert.AreEqual(user.Name, newUser.Name);
			Assert.AreEqual(user.Id, newUser.Id);
		}
	}
}

