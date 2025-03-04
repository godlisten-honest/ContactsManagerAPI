using Carter;
using Carter.ModelBinding;
using ContactsManagerAPI.Dtos;
using ContactsManagerAPI.Models;
using System.Reflection.Metadata;
namespace ContactsManagerAPI.Features.Customers.GetCustomerById
{
    public record GetCustomerByIdResponse : CustomerDto { };
    public class GetCustomerByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/customers/{id}", Handle)
                //.RequireAuthorization()
                .WithName("GetCustomerById")
                .Produces<GetCustomerByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Customer By Id")
                .WithDescription("This method returns a single customer with the specified Id or retuns a Not Found error");

        }

        public async Task<IResult> Handle(Guid id,ContactsDbContext context)
        {

            var customer = context.Customers.Where(c=>c.CustomerId==id).Select(c => new GetCustomerByIdResponse { CustomerId = c.CustomerId, Name = c.Name }).FirstOrDefault();
            if(customer==null)
            {
                return Results.NotFound("Customer with the specified Id was not found");
            }
            return Results.Ok(customer);
        }
    }
}