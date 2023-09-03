using ElectronNET.API;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Zine.Commands;
using Zine.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddElectron();
builder.WebHost.UseElectron(args);

builder.Services.AddMudServices();

builder.Services.AddDbContext<ComicBookContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("ComicBookContext")
	                  ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.StartAsync();

if (app.Environment.IsDevelopment())
{
	var logger = LoggerFactory.Create(config => config.AddConsole());
	var tailwindBuildCommand = new TailwindBuildCommand(new Logger<TailwindBuildCommand>(logger));
	tailwindBuildCommand.Start();
	app.Lifetime.ApplicationStopping.Register(tailwindBuildCommand.Stop);

	app.UseDeveloperExceptionPage();
	app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<ComicBookContext>();
	context.Database.EnsureCreated();
}


if (HybridSupport.IsElectronActive)
{
	var window = await Electron.WindowManager.CreateWindowAsync();
	window.OnClosed += () => Electron.App.Quit();
}

app.WaitForShutdown();
