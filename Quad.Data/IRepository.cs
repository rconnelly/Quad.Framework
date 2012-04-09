// /* 
//     Copyright Quad IO, inc 2012
// */
using System;
using Quad.Dto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Linq;

namespace Quad.Data
{
	public interface IRespository
	{
		T Add<T>(T dto) where T : IDto ;
		T GetById<T>(Guid id) where T : IDto;
		SafeModeResult Delete<T>(Guid id) where T : IDto;
		IQueryable Fetch<T>(MongoDatabase db) where T : IDto;
	}
}

