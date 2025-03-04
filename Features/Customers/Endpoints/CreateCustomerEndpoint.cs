using Carter;
using Carter.ModelBinding;
using ContactsManagerAPI.Dtos;
using ContactsManagerAPI.Models;
using System.Reflection.Metadata;
namespace ContactsManagerAPI.Features.Customers.CreateCustomer
{
    public record CreateCustomerRequest : CustomerDto { };
    public record CreateCustomerResponse(Guid Id);
    public class CreateCustomerEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/customers", Handle);

        }

        public async Task<IResult> Handle(HttpRequest httpRequest, ContactsDbContext context, CreateCustomerRequest request)
        {
            var result = httpRequest.Validate<CreateCustomerRequest>(request);
            if (!result.IsValid)
            {
                return Results.BadRequest(result.GetFormattedErrors());
            }

            var customer = new Customer();
            customer.Name = request.Name;
            customer.CustomerId = Guid.CreateVersion7();
            context.Add<Customer>(customer);
            await context.SaveChangesAsync();
            var response = new CreateCustomerResponse(customer.CustomerId);
            return Results.Ok(response);
        }
    }
}

/*
  var request = await req.BindAndValidate<CreateCustomerRequest>();
                if (!request.ValidationResult.IsValid)
                {
                    res.StatusCode = 400;
                    return res.AsJson(request.ValidationResult.GetFormattedErrors());
                }
                var response = new CreateCustomerResponse();
                return res.AsJson(response);
 */
