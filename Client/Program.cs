using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Plantify.Client;
using Plantify.Client.Services;
using Plantify.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Plantify.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Plantify.ServerAPI"));

builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IListPedidoService, ListPedidoService>();
builder.Services.AddScoped<IQueryService, QueryService>();

builder.Services.AddScoped<ClienteGlobal>();
builder.Services.AddScoped<PedidoDTO>();

await builder.Build().RunAsync();
