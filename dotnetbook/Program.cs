var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<CoreBookService>();
Trace.WriteLine(builder.HostEnvironment.BaseAddress);
Console.WriteLine(builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();
