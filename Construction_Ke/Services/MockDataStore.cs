using Construction_Ke.Model;

namespace Construction_Ke.Services
{
    public class MockDataStore : IDataStore<FirstWeight>
    {
        readonly List<FirstWeight> FirstWeights;
        public MockDataStore()
        {
            FirstWeights = new List<FirstWeight>(){
                new FirstWeight { Code = 14, Ticket = 120 , Weight = 20.00, Driver = "davy", Phone = 0723564376, Plate = "kdg 200g", Material = "614", Amount = 1300.00,
                DateTime = Convert.ToDateTime("14-12-2022"), Time = "12:00:00"},
                new FirstWeight { Code = 14, Ticket = 120 , Weight = 20.00, Driver = "davy", Phone = 0723564376, Plate = "kdg 200g", Material = "614", Amount = 1300.00,
                DateTime = Convert.ToDateTime("14-12-2022"), Time = "12:00:00"},
                new FirstWeight { Code = 14, Ticket = 120 , Weight = 20.00, Driver = "davy", Phone = 0723564376, Plate = "kdg 200g", Material = "614", Amount = 1300.00,
                DateTime = Convert.ToDateTime("14-12-2022"), Time = "12:00:00"},
                new FirstWeight { Code = 14, Ticket = 120 , Weight = 20.00, Driver = "davy", Phone = 0723564376, Plate = "kdg 200g", Material = "614", Amount = 1300.00,
                DateTime = Convert.ToDateTime("14-12-2022"), Time = "12:00:00"}
            };
        }
        public async Task<bool> AddItemAsync(FirstWeight item)
        {
            FirstWeights.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(FirstWeight item)
        {
            var oldItem = FirstWeights.Where((FirstWeight arg) => arg.Ticket == item.Ticket).FirstOrDefault();
            FirstWeights.Remove(oldItem);
            FirstWeights.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string ticket)
        {
            var oldItem = FirstWeights.Where((FirstWeight arg) => arg.Ticket.ToString() == ticket).FirstOrDefault();
            FirstWeights.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<FirstWeight> GetItemAsync(string ticket)
        {
            return await Task.FromResult(FirstWeights.FirstOrDefault(s => s.Ticket.ToString() == ticket));
        }

        public async Task<IEnumerable<FirstWeight>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(FirstWeights);
        }
    }
}
