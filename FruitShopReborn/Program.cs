using Core;
using Core.Entities.Users;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Repository.Repositories;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();
        
builder.Services.AddDbContext<FruitShopRebornDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options => 
    {
        options.Password.RequiredLength = BussinessRuleConstant.PasswordMinLength;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
    
        options.User.RequireUniqueEmail = true; 
    })
    .AddEntityFrameworkStores<FruitShopRebornDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.AccessDeniedPath = "/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

builder.Services.AddSingleton(new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UseTargetBlankUrl()
    .Build());

builder.Services.AddSingleton<IChatCompletionService>(
    new AzureOpenAIChatCompletionService(
        builder.Configuration["AzureOpenAIChatCompletionDeploymentName"]!,
        builder.Configuration["AzureOpenAIEndpoint"]!,
        builder.Configuration["AzureOpenAIApiKey"]!
    )
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAiService,AiService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        services.GetRequiredService<FruitShopRebornDbContext>().Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        services.GetRequiredService<ILogger<Program>>()
            .LogError(ex, "An error occurred while creating the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();
app.MapControllers();
app.Run();