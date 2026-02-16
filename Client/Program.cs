using Client;
using Client.Services;
using Client.Services.Interfases;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(uriString: "https://localhost:7144/") });
builder.Services.AddMudServices();

builder.Services.AddScoped<IAppointmentApiService, AppointmentApiService>();
builder.Services.AddScoped<IClinicApiService, ClinicApiService>();
builder.Services.AddScoped<IPatientApiService, PatientApiService>();


await builder.Build().RunAsync();
