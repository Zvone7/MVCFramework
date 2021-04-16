using System.Threading.Tasks;

namespace MvcFrameworkCml.Infrastructure.Repository
{
    public interface IMockedDataProvider
    {
        Task<MockedData> GetMockedDataAsync();
    }
}