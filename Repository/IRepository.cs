using SpejderApplikation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpejderApplikation.Repository
{
    public interface IRepository<Type>  // interface for the repository
    {
        // Retrieve all records of the specified type
        IEnumerable<Type> GetAll();

        // Retrieve a specific record by ID
        public Type GetByID(int id);

        // Edit an existing record
        public void EditType(Type entity);

        // Add a new record
        public int AddType(Type entity, int ID);

        // Delete an existing record
        public void DeleteType(Type entity);

        // Connect two related entities
        public void ConnectTypes(Type entity, ScoutsMeeting JoinedEntity);
    }
}
