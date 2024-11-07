using SpejderApplikation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    internal class UnitRepository : IRepository<Unit>
    {
        public int AddType<Type>(Type type)
        {
            throw new NotImplementedException();
        }

        public void DeleteType<Type>(int id)
        {
            throw new NotImplementedException();
        }

        public void EditType<Type>(Type type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Unit> GetAll()
        {
            throw new NotImplementedException();
        }

        public Type GetByID<Type>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
