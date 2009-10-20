using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
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

        public FileObject GetFileObject(string filePath)
        {
            try { return fileObjectTable.First(x => x.FilePath.Equals(filePath)); }
            catch { return new FileObject(); }
        }

        public void DeleteFileObject(FileObject fileObject)
        {
            fileObject.Active = false;
        }

        public void SaveFileObject(FileObject fileObject)
        {
            EntityUtilities.EnsureValid(fileObject, "FilePath", "Name");
            fileObject.UpdateName();
            if (fileObject.Id == 0) fileObjectTable.InsertOnSubmit(fileObject);
            else
            {
                try { fileObjectTable.Attach(fileObject); }
                catch (InvalidOperationException ex) { if (!ex.Message.Equals("Cannot attach an entity that already exists.")) throw ex; } //already attached
                catch (Exception ex) { throw ex; }
                fileObjectTable.Context.Refresh(RefreshMode.KeepCurrentValues, fileObject);
            }
        }

        public void SubmitChanges()
        {
            fileObjectTable.Context.SubmitChanges();
        }

        public IEnumerable<string> Cleanup()
        {
            List<string> deleted = new List<string>();
            foreach (FileObject fileObject in fileObjectTable.Where(x => x.Active))
            {
                if (!File.Exists(fileObject.FilePath))
                {
                    fileObject.Active = false;
                    deleted.Add(fileObject.FilePath);
                }
            }
            return deleted;
        }
    }
}
