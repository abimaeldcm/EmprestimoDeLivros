using EmprestimoLivros.Web.Services;
#nullable disable

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<ILivrosService, LivrosService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("UsuarioApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]);
});
builder.Services.AddHttpClient("LivroApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProductApi"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
