namespace exercise1.Services
{
    public class CustomerService(Func<string,ICustomerRepository> repoResolver)
    {
        public async Task<List<CustomerModel>> getAllAsync(string repoName)
        {
            var repo = repoResolver(repoName);
            var customers = await repo.getAllCustomers();
            return customers;
        }

        public async Task<CustomerModel> getByIdAsync(string repoName, int id)
        {
            var repo = repoResolver(repoName);
            var customer = await repo.getCustomerById(id);
            return customer;
        }

        public async Task<CustomerModel> updateAsync(string repoName , CustomerModel customerToUpdate)
        {
            var repo = repoResolver(repoName);
            var customer = await repo.updateCustomer(customerToUpdate);
            return customer;
        }

        public async Task<CustomerModel> addAsync(string repoName, CustomerModel customerToAdd)
        {
            var repo = repoResolver(repoName);
            var customer = await repo.addCustomer(customerToAdd);
            return customer;
        }

        public async Task<int> deleteAsync(string repoName, int id)
        {
            var repo = repoResolver(repoName);
            var idDeleted = await repo.deleteCustomer(id);
            return idDeleted;
        }
    }
}
