using CategoriasMvc.Services;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configura ClientHttp para fazer requisi��es a API. Poder� ser injetada em toda a aplica��o
builder.Services.AddHttpClient("CategoriasApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:CategoriasApi"]);
});

//Configura ClientHttp para fazer requisi��es a API. Poder� ser injetada em toda a aplica��o
builder.Services.AddHttpClient("AutenticaApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:AutenticaApi"]);
    c.DefaultRequestHeaders.Accept.Clear(); //Limpa o cabe�alho 
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Difere esse ClientHttp do outro (CategoriasApi), criado acima.
});

//Configura ClientHttp para fazer requisi��es a API. Poder� ser injetada em toda a aplica��o
builder.Services.AddHttpClient("ProdutosApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ServiceUri:ProdutosApi"]);
});

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IAutenticacao, Autenticacao>();

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
