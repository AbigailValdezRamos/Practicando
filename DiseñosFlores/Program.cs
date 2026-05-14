using DiseñosFlores.Interfaces;
using DiseñosFlores.Repositories;
using DiseñosFlores.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

///JC
builder.Services.AddScoped<IDepartamentoRepository>(sp => new DepartamentoRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IDepartamentoService, DepartamentoService>();

//ABI
builder.Services.AddScoped<IClienteRepository>(sp => new ClienteRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IClienteService, ClienteService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
