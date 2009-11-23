using System;
using System.Data.Linq;
using System.Linq;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;
using Forganizer.DomainModel.Extensions;

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

        public Category GetCategory(int Id)
        {
            Category category = categoryTable.SingleOrDefault(x => x.Id == Id);
            if (category == null) category = new Category() { IsValid = false };
            else category.IsValid = true;
            return category;
        }

        public void SaveCategory(Category category)
        {
            EntityUtilities.Categories.EnsureValid(category, this, "Name", "ExtensionString");
            category.ExtensionString = category.ExtensionString.SplitTags().ToSpacedString();
            if (category.Id == 0) categoryTable.InsertOnSubmit(category);
            else
            {
                try { categoryTable.Attach(category); }
                catch (InvalidOperationException ex) { if (!ex.Message.Equals("Cannot attach an entity that already exists.")) throw ex; } //already attached
                catch (Exception ex) { throw ex; }
                categoryTable.Context.Refresh(RefreshMode.KeepCurrentValues, category);
            }
            categoryTable.Context.SubmitChanges();
        }

        public void DeleteCategory(Category category)
        {
            categoryTable.DeleteOnSubmit(category);
            categoryTable.Context.SubmitChanges();
        }
    }
}
