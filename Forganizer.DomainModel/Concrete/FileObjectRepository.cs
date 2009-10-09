using System.Data.Linq;
using System.Linq;
using Forganizer.DomainModel.Abstract;
using Forganizer.DomainModel.Entities;

namespace Forganizer.DomainModel.Concrete
{
    public class FileObjectRepository : IFileObjectRepository
    {
        private Table<FileObject> fileObjectTable;
        public FileObjectRepository(string connectionString)
        {
            fileObjectTable = (new DataContext(connectionString)).GetTable<FileObject>();
        }
        
        public IQueryable<Forganizer.DomainModel.Entities.FileObject> FileObjects
        {
            get { return fileObjectTable; }  
        }
    }
}
