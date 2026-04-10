using VF_CRIB_CREDITINFO.Business.Authentication;
using VF_CRIB_CREDITINFO.Business.ConnectionHandler;
using VF_CRIB_CREDITINFO.Business.CRIBHandler;
using VF_CRIB_CREDITINFO.Business.UserHandler;
using VF_CRIB_CREDITINFO.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<_ConnectionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ADAuthentication>();
builder.Services.AddHttpClient<ITokenService, TokenService>();

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.AddHttpContextAccessor();

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

// Enable session before authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
