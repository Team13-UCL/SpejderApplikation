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
        public int AddType(Type entity);
        public Type GetByID(int id);
        public void EditType(Type entity);
        public void DeleteType(int id);
    }
}
