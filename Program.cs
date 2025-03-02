using ContactsManagerAPI;
using ContactsManagerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AppSettings appSettings=new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddDbContextPool<ContactsDbContext>(opt=>opt.UseNpgsql( appSettings.ConnectionStrings.ContactsDbContext));
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// fetch all contacts
app.MapGet("/contacts", (ContactsDbContext context) => 
{ 
    var contacts=context.Contacts.ToList();
    return contacts;

});

// fetch contact by id
app.MapGet("/contacts/{id}", (Guid id,ContactsDbContext context) =>
{
    var contact = context.Contacts.Where(c=>c.ContactId==id).FirstOrDefault();
    if(contact==null)
    {
        return Results.NotFound("Contact Not Found");
    }
    return Results.Ok(contact);

});

// create new contact
app.MapPost("/contacts", (ContactsDbContext context,Contact contact) =>
{
    context.Contacts.Add(contact);
    context.SaveChanges();

    return contact;

});

// update contact
app.MapPut("/contacts/{id}",  (Guid id, Contact contact, ContactsDbContext context) => 
{

    if(id!=contact.ContactId)
    {
        return Results.BadRequest("Contact ID not found");
    }

    context.Entry(contact).State=EntityState.Modified;
    context.SaveChanges();
    return Results.Ok( contact);


});

// delete contact
app.MapDelete("/contacts/{id}", (Guid id, ContactsDbContext context) =>
{
    var ctct=context.Contacts.Find(id);
    if (ctct== null)
    {
        return Results.NotFound("Contact ID not found");
    }

    context.Remove(ctct);
    context.SaveChanges();
    return Results.Ok();


});

// create new customer
app.MapPost("/customers", (ContactsDbContext context, Customer customer) =>
{
    context.Customers.Add(customer);
    context.SaveChanges();

    return customer;

});

// fetch all customer
app.MapGet("/customers", (ContactsDbContext context) =>
{
    var customer = context.Customers.ToList();
  
    return Results.Ok(customer);

});



app.Run();
