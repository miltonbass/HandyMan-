using CurrieTechnologies.Razor.SweetAlert2;
using HandyMan_.Frontend;
using HandyMan_.Frontend.Repositories;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HandyMan_.Frontend.AuthenticationProviders;
using HandyMan_.Frontend.Services;
using Blazored.Modal;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMatBlazor();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

//var uriBack = "https://handymanplus.azurewebsites.net";
var uriBack = "https://localhost:7002/";

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(uriBack) }); 
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredModal();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
