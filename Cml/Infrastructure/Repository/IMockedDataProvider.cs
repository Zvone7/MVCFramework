using System.Threading.Tasks;
using Cml.DataModels;

namespace Cml.Infrastructure.Repository
{
    public interface IMockedDataProvider
    {
        Task<MockedData> GetMockedDataAsync();
    }
}