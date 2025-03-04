
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AppSettings appSettings=new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddDbContextPool<ContactsDbContext>(opt=>opt.UseNpgsql( appSettings.ConnectionStrings.ContactsDbContext));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = appSettings.Jwt.IdentityBaseUrl;
        options.Audience = appSettings.Jwt.Audience;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer=false,
            ValidateAudience=false,
            ValidateIssuerSigningKey=false
        };
        options.Events = new()
        {
            OnTokenValidated = async context =>
            {
                if (context.Principal?.Identity is ClaimsIdentity claimsIdentity)
                {
                    Claim? scopeClaim = claimsIdentity.FindFirst("scope");
                    if (scopeClaim is not null)
                    {
                        claimsIdentity.RemoveClaim(scopeClaim);
                        claimsIdentity.AddClaims(scopeClaim.Value.Split(" ").Select(s => new Claim("scope", s)).ToList());
                    }
                }

                await Task.CompletedTask;
            }
        };
    });




builder.Services.AddCarter();
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
/*
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

app.MapGet("/customers/{id}", (Guid id,ContactsDbContext context) =>
{
    var customer = context.Customers.Where(c=>c.CustomerId==id).Include(c=>c.Contacts);

    return Results.Ok(customer);

});
*/

app.MapCarter();

app.Run();
