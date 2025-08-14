using Microsoft.AspNetCore.Http.HttpResults;

namespace exercise1.endPoints
{
    public static class CustomerEndPoints
    {
        public static void MapCustomer(this WebApplication app)
        {
            var customerGroupe = app.MapGroup("customers").WithTags("customer");
            customerGroupe.MapGet("", async Task<Ok<List<CustomerModel>>> (CustomerService customerService) =>
            {
                var customers = await customerService.getAllAsync("EF");
                return TypedResults.Ok(customers);
            });

            customerGroupe.MapGet("{id}",static async Task<Results<NotFound, Ok<CustomerModel>>> (CustomerService customerService, int id) =>
            {
                try
                {
                    var customer = await customerService.getByIdAsync("",id);
                    return TypedResults.Ok(customer);
                }
                catch (Exception ex)
                {
                    return TypedResults.NotFound();
                };
            });

            customerGroupe.MapPost("", static async Task<Results<ValidationProblem,Created<CustomerDto>>> (CustomerService customerService, CustomerDto customer, IMapper mapper, IValidator<CustomerDto> validator) =>
            {
                var validatorResult = await validator.ValidateAsync(customer);
                if (!validatorResult.IsValid)
                {
                    return TypedResults.ValidationProblem(validatorResult.ToDictionary());
                }
                var customerModel = mapper.Map<CustomerModel>(customer);
                var SavedCustomer = await customerService.addAsync("EF",customerModel);
                var customerDto = mapper.Map<CustomerDto>(SavedCustomer);

                return TypedResults.Created($"api/products/{customerDto.GuId}", customerDto);
            });

            customerGroupe.MapPut("", async Task<Created<CustomerModel>> (CustomerService customerService, CustomerModel customer) =>
            {
                var updatedCustomer = await customerService.updateAsync("EF",customer);

                return TypedResults.Created($"api/products/{updatedCustomer.GuId}", updatedCustomer);
            });

            customerGroupe.MapDelete("{id}", async Task<Results<NotFound, Ok<string>>> (CustomerService customerService, int id) =>
            {
                try
                {
                    var customerId = await customerService.deleteAsync("ef",id);
                    return TypedResults.Ok("customer with ID " + customerId + "deleted");
                }
                catch (Exception ex)
                {
                    return TypedResults.NotFound();
                };
            });
        }
    }
}
