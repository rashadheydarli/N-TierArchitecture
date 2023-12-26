using System;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repository.Abstract;
using DataAccess.Repository.Base;

namespace DataAccess.Repository.Concrete
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
		{
            _context = context;
        }
        //constructor ona gore yaratdiq ki repositorinin constructoru parametrilidir ve burda da onu gozleyir 

        public async Task<Category> GetByTitleAsync(string title)
        {
            return _context.Categories.FirstOrDefault(c => c.Title.ToLower().Trim() == title.ToLower().Trim());
        }
    }
}

