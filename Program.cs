using INTEXII.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using INTEXII.Models;
using Microsoft.ML.OnnxRuntime;
using Npgsql;

// this is a comment by Angelina
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RDSContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RDSContext")));



builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("RDSContext");
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(60);
    options.ExcludedHosts.Add("example.com");
    options.ExcludedHosts.Add("www.example.com");
});
builder.Services.AddDbContext<RDSContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
//    options.HttpsPort = 5001;
//});




//Second line(add Roles) is authenicat
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false) //was true it made it false
    .AddEntityFrameworkStores<RDSContext>();
//.AddRoles<IdentityRole>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<InferenceSession>(
    new InferenceSession("wwwroot/supervisedmodel (3).onnx")
);
builder.Services.AddSingleton<InferenceSession>(provider => {
    var env = provider.GetService<IWebHostEnvironment>();
    var modelPath = Path.Combine(env.ContentRootPath, "wwwroot", "supervisedmodel (3).onnx");
    return new InferenceSession(modelPath);
});



builder.Services.Configure<IdentityOptions>(options =>
{
    // Harsher Password Requirements.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 14;
    options.Password.RequiredUniqueChars = 2;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

app.UseCookiePolicy();
app.Use(async (context, next) =>
{
    //context.Response.Headers.Add("Content-Security-Policy", "{default-src 'self'; img-src 'self'}");
    context.Response.Headers.Add("Content-Security-Policy", "{default-src 'self'; img-src 'self' cdn.example.com}");
    await next();
});
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


// Nathaniel