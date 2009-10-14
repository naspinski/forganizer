using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Forganizer.DomainModel.Abstract;

namespace Forganizer.DomainModel.Entities
{
    public static class EntityUtilities
    {
        public class Categories
        {
            public static string EnsureValid(IDataErrorInfo validatable, ICategoryRepository categoryRepository, params string[] properties)
            {
                Category category = (Category)validatable;
                EntityUtilities.EnsureValid(validatable, properties);
                if (categoryRepository.Categories.Any(x => x.Id != category.Id && x.Name == category.Name))
                    return "name is a duplicate";
                else return (string)null;
            }
        }

        public static void EnsureValid(IDataErrorInfo validatable, params string[] properties)
        {
            if (properties.Any(x => validatable[x] != null))
                throw new InvalidOperationException(validatable.GetType().ToString() + " is invalid");
        }
    }
}
