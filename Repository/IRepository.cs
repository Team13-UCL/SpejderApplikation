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
        public int AddType(Type entity);
        public void EditType(Type entity);
        public void DeleteType(Type entity);
        IEnumerable<Type> GetAll();
        public Type GetByID(int id);
    }
}
