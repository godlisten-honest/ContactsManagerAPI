using Carter;
using Carter.ModelBinding;
using ContactsManagerAPI.Dtos;
using ContactsManagerAPI.Models;
using System.Reflection.Metadata;
namespace ContactsManagerAPI.Features.Customers.GetCustomers
{
    public record GetCustomerResponse : CustomerDto { };
    public class GetCustomersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/customers", Handle);

        }

        public async Task<IResult> Handle(ContactsDbContext context)
        {

            var customers = context.Customers.Select(c=>new GetCustomerResponse { CustomerId=c.CustomerId,Name=c.Name }).ToList();
;
            return Results.Ok(customers);
        }
    }
}