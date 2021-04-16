using System;
using System.IO;
using System.Threading.Tasks;
using MvcFrameworkCml;
using MvcFrameworkCml.Infrastructure.Repository;
using Newtonsoft.Json;

namespace MvcFrameworkDbl
{
    public class MockedDataProvider : IMockedDataProvider
    {
        private const String MOCKED_DATA_FILE = "MockedDb.json";
        private MockedData _mockedData;

        public async Task<MockedData> GetMockedDataAsync()
        {
            if (_mockedData != null) return _mockedData;
            try
            {
                Console.WriteLine($"Reading {MOCKED_DATA_FILE}");
                using (var r = new StreamReader(MOCKED_DATA_FILE))
                {
                    var json = await r.ReadToEndAsync();
                    _mockedData = JsonConvert.DeserializeObject<MockedData>(json);
                }

                return _mockedData;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception reading {MOCKED_DATA_FILE}:{e.Message}.\nApplication exiting.");
                return null;
            }
        }
    }
}