using OnlineStore.Data;
using OnlineStore.Data.Models.Product;
using OnlineStore.Services.Images;
using OnlineStore.Services.PayOut;
using OnlineStore.Services.Product;
using OnlineStore.Services.ShoppingCart;
using OnlineStore.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton<LoggedUserService>();
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IPayOutService, PayOutService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();