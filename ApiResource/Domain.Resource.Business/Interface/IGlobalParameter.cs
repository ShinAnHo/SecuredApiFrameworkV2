using Domain.Api;
using Domain.Database;
using Domain.Resource.Data;
using System.Threading.Tasks;

namespace Domain.Resource.Business
{
    public interface IGlobalParameter : IBusinessLogic<GlobalParameter>
    {
        Task<GlobalParameter> GetById(string ParameterId);
    }
}
