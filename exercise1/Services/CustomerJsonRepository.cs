
namespace exercise1.Services
{
    public class CustomerJsonRepository : ICustomerRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "customers.json");
        private List<CustomerModel> customers;
        private ILogger _logger;

        public CustomerJsonRepository(ILogger<CustomerJsonRepository> logger)
        {
            _logger = logger;
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                customers = JsonSerializer.Deserialize<List<CustomerModel>>(json) ?? new();
            }
            else
            {
                throw new Exception(" - customers.json file not found in the current directory: " + Directory.GetCurrentDirectory());
            }
        }

        public async Task<List<CustomerModel>> getAllCustomers()
        {
            return customers;
        }
        public async Task<CustomerModel> addCustomer(CustomerModel customer)
        {
            customers.Add(customer);

            writeInJson();

            return customer;
        }



        public async Task<CustomerModel> getCustomerById(int id)
        {
            var customer = getCustomer(id);
            return customer;
        }

        public async Task<int> deleteCustomer(int id)
        {

            var customer = getCustomer(id);
            customers.Remove(customer);
            writeInJson();
            return id;
        }

        public async Task<CustomerModel> updateCustomer(CustomerModel customerToUpdate)
        {

            int index = customers.FindIndex(c => c.GuId == customerToUpdate.GuId);
            if (index == -1)
            {
                throw new Exception(" - Customer not found with id: " + customerToUpdate.GuId);
            }
            else
            {
                customers[index] = customers[index] with { Email = customerToUpdate.Email, Name = customerToUpdate.Name };
            }
            writeInJson();
            return customerToUpdate;

        }

        private async void writeInJson()
        {
            var updatedJson = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, updatedJson);
        }

        public CustomerModel getCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.GuId == id);
            if (customer == null)
            {
                throw new Exception(" - Customer not found with id: " + id);
            }
            return customer;
        }
    }
}
