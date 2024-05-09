using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend;
using HandyMan_.Frontend.Repositories;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMatBlazor();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5271/") }); 
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddSweetAlert2();


await builder.Build().RunAsync();
