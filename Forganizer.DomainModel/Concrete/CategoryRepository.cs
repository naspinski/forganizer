using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using System.Data.Linq;

namespace Forganizer.DomainModel.Concrete
{
    class CategoryRepository : ICategoryRepository
    {
        private Table<Category> categoryTable;
        public CategoryRepository(string connectionString)
        {
            categoryTable = (new DataContext(connectionString)).GetTable<Category>();
        }

        public IQueryable<Category> Categories
        {
            get { return categoryTable.OrderBy(x => x.Name); }
        }
    }
}
