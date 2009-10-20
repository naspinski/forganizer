using System.Linq;
using Forganizer.DomainModel.Entities;

namespace Forganizer.DomainModel.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        Category GetCategory(int Id);
        void SaveCategory(Category category);
        void DeleteCategory(Category category);
    }
}
