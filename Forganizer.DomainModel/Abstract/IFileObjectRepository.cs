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
        void DeleteFileObject(FileObject fileObject);
    }
}
