

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDbContext<customerDbContext>(o => o.UseInMemoryDatabase("CustomerDb"));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(customerProfile));
builder.Services.Configure<NotificationSettingsConfig>(builder.Configuration.GetSection("NotificationSettings"));
builder.Services.AddScoped<CustomerJsonRepository>();
builder.Services.AddScoped<CustomerDbContextRepository>();
builder.Services.AddScoped<Func<string, ICustomerRepository>>(sp => key => 
key switch { 
    "ef" => sp.GetRequiredService<CustomerDbContextRepository>(),
    _ => sp.GetRequiredService<CustomerJsonRepository>()
}
);
builder.Services.AddScoped<CustomerService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseStaticFiles();
var customerGroupe = app.MapGroup("customers").WithTags("customer");

//app.MapControllers();
app.MapCustomer();

app.Run();


