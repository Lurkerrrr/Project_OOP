using CarParts.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ISupplierService, SupplierService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();

//learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//for the actual port configuration, please refer to the 'CarPartsGUI/Program.cs'
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<CarParts.Middleware.ExceptionHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
