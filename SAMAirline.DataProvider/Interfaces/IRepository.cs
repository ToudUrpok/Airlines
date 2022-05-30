using SAMAirline.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace SAMAirline.DataProvider.Interfaces
{
    public interface IRepository<T> 
        where T : class, new()
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool IsExists(int id);
        PagedList<T> GetPagedList(IEnumerable<T> source, int pageNumber, int pageSize, bool isAdmin);
    }
}
