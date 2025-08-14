namespace exercise1.Contracts
{
    public interface ICustomerRepository
    {
        public Task<List<CustomerModel>> getAllCustomers();
        public Task<CustomerModel> addCustomer(CustomerModel customer);
        public Task<CustomerModel> updateCustomer(CustomerModel customer);
        public Task<CustomerModel> getCustomerById(int id);
        public Task<int> deleteCustomer(int id);
    }
}
