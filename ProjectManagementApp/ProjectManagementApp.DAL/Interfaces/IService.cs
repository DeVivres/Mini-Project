using System.Collections.Generic;

namespace ProjectManagementApp.ProjectManagementApp.Interfaces
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}
