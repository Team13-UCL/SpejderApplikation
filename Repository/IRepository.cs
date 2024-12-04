using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    public interface IRepository<Type> 
    {
        IEnumerable<Type> GetAll();
        public Type GetByID(int id);
        public void EditType(Type entity);
        public int AddType(Type entity, int ID);
        public void DeleteType(Type entity);
    }
}
