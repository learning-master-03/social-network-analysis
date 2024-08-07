using System;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Books
{
    public interface  ModificationAuditing : CreationAuditing
    {

        public  DateTime? LastModificationTime { get; set; }
    
        public  string LastModificationBy { get; set; }
        

    }
    public interface ModificationNoneKey : CreationNoneKey
    {

        public  DateTime? LastModificationTime { get; set; }

        public  string LastModificationBy { get; set; }


    }
public interface  CreationAuditing
    {
        public  DateTime CreationTime { get; set; }
    
        public  string CreationBy { get; set; }
        

    }
    public interface CreationNoneKey
    {
        public  DateTime CreationTime { get; set; }

        public  string CreationBy { get; set; }


    }
}