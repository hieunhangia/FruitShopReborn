using Core.Interfaces.Services;
using Markdig;
using Microsoft.SemanticKernel;
using Service;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsecrets.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton(new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .Build());
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();
builder.Services.AddAzureOpenAIChatCompletion(
    builder.Configuration["AzureOpenAIChatCompletionDeploymentName"] ?? "",
    builder.Configuration["AzureOpenAIEndpoint"] ?? "",
    builder.Configuration["AzureOpenAIKey"] ?? ""
);
builder.Services.AddSingleton<IAiService,AiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();