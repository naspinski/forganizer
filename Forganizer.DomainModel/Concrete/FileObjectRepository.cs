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

        public IQueryable<FileObject> FileObjects
        {
            get { return fileObjectTable.Where(x => x.Active); }
        }

        public IQueryable<FileObject> AllFileObjects
        {
            get { return fileObjectTable; }
        }

        public FileObject GetFileObject(int Id)
        {
            return fileObjectTable.SingleOrDefault(x => x.Id == Id);
        }

        public void DeleteFileObject(FileObject fileObject)
        {
            fileObject.Active = false;
            fileObjectTable.Context.SubmitChanges();
        }
    }
}
