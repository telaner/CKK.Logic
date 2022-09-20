using CKK.Logic.Repository.Interfaces;
using CKK.Logic.Repository.Implementation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<DatabaseConnectionFactory>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(sp =>
{
    var factory = sp.GetRequiredService<DatabaseConnectionFactory>();
    return new ProductRepository(factory);
});
builder.Services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>(sp =>
{
    var factory = sp.GetRequiredService<DatabaseConnectionFactory>();
    return new ShoppingCartItemRepository(factory);
});
builder.Services.AddScoped<IOrderRepository, OrderRepository>(sp =>
{
    var factory = sp.GetRequiredService<DatabaseConnectionFactory>();
    return new OrderRepository(factory);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
