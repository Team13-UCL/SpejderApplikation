using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    internal interface IRepository<Type> 
    {
        IEnumerable<Type> GetAll();
        public int AddType<Type>(Type type);
        public Type GetByID<Type>(int id);
        public void EditType<Type>(Type type);
        public void DeleteType<Type>(int id);
    }
}
