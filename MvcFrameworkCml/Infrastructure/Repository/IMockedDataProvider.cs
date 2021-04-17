using System.Threading.Tasks;
using MvcFrameworkCml.DataModels;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IMockedDataProvider
    {
        Task<MockedData> GetMockedDataAsync();
    }
}