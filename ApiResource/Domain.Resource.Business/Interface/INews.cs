using Domain.Api;
using Domain.Database;
using Domain.Resource.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Resource.Business
{
    public interface INews : IBaseBusinessLogic<News>
    {
        Task<News> GetById(long Id);
        Task<IEnumerable<News>> GetByUser(string user);
    }
}
