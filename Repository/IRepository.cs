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
        public Type GetByID(int id);
        public int AddOrEditType(Type entity, int ID);
        public void DeleteType(Type entity);
    }
}
