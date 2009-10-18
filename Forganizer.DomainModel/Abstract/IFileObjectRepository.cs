using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forganizer.DomainModel.Entities;

namespace Forganizer.DomainModel.Abstract
{
    public interface IFileObjectRepository
    {
        IQueryable<FileObject> FileObjects { get; }
        IQueryable<FileObject> AllFileObjects { get; }
        FileObject GetFileObject(int Id);
        FileObject GetFileObject(string filePath);
        void DeleteFileObject(FileObject fileObject);
        void SaveFileObject(FileObject fileObject);
        void SubmitChanges();
        IEnumerable<string> Cleanup();
    }
}
