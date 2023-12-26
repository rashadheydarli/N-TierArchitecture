using System;
using Common.Entities;
using DataAccess.Repository.Base;

namespace DataAccess.Repository.Abstract
{
	public interface ICategoryRepository : IRepository<Category>
	{
		Task<Category> GetByTitleAsync(string title);
	}
}

