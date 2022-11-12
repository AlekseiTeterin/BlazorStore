using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyStore.BlazorClient;
using MyStore.HttpApiClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IMyStoreClient, MyStoreClient>();

builder.Services.AddSingleton<ICatalog, InMemoryCatalog>();
builder.Services.AddScoped<IBasket, InMemoryBasket>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
