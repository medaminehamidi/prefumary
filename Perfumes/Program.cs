using PostgreSQL.Products;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IReadProduct, ReadProduct>();
builder.Services.AddTransient<IWriteProduct, WriteProduct>();
builder.Services.AddTransient<IReadPromo, ReadPromo>();
builder.Services.AddTransient<IWritePromo, WritePromo>();
builder.Services.AddTransient<IProductWithPromo, ProductWithPromo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
