
using ElectronNET.API;
using MudBlazor.Services;
using Zine.Commands;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddElectron();
builder.WebHost.UseElectron(args);

builder.Services.AddMudServices();


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
}


if (HybridSupport.IsElectronActive)
{
    var window = await Electron.WindowManager.CreateWindowAsync();
    window.OnClosed += () => Electron.App.Quit();
}

app.WaitForShutdown();
