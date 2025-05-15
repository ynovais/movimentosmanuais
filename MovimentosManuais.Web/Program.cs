using Microsoft.EntityFrameworkCore;
using MovimentosManuais.Application.Queries;
using MovimentosManuais.Application.Services;
using MovimentosManuais.Domain.Dtos;
using MovimentosManuais.Domain.Interfaces;
using MovimentosManuais.Infrastructure.Data;
using MovimentosManuais.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder
            .WithOrigins("http://localhost:4200", "https://localhost:5001", "https://localhost:61373") // Allow dev and prod origins
            .AllowAnyMethod() // Allow GET, POST, etc.
            .AllowAnyHeader() // Allow headers like Content-Type
            .AllowCredentials(); // Allow cookies or auth headers if needed
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=.\\SQLEXPRESS;Database=MovimentosManuais;User Id=candidato;Password=candidato123;TrustServerCertificate=True;"));
builder.Services.AddScoped<IMovimentoManualRepository, MovimentoManualRepository>();
builder.Services.AddScoped<MovimentoManualService>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();


builder.Services.AddScoped<IProdutoCosifRepository, ProdutoCosifRepository>();
builder.Services.AddScoped<ProdutoCosifService>();


builder.Services.AddScoped<IQueryHandler<ProdutoQuery, IEnumerable<ProdutoDto>>, ProdutoQueryHandler>();
builder.Services.AddScoped<IQueryHandler<ProdutoQuery, ProdutoDto>, ProdutoSingleQueryHandler>();
builder.Services.AddScoped<IQueryHandler<ProdutoCosifQuery, IEnumerable<ProdutoCosifDto>>, ProdutoCosifQueryHandler>();
builder.Services.AddScoped<IQueryHandler<ProdutoCosifQuery, ProdutoCosifDto>, ProdutoCosifSingleQueryHandler>();
builder.Services.AddScoped<IQueryHandler<MovimentoManualQuery, IEnumerable<MovimentoManualDto>>, MovimentoManualQueryHandler>();
builder.Services.AddScoped<IQueryHandler<MovimentoManualQuery, MovimentoManualDto>, MovimentoManualSingleQueryHandler>(); 

// Configure SPA
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist/movimentos-manuais-app";
});

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "ClientApp";
//    if (app.Environment.IsDevelopment())
//    {
//        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
//    }
//    else
//    {
//        // Fallback if index.html is missing
//        if (!File.Exists(Path.Combine("ClientApp", "dist", "movimentos-manuais-app", "browser","index.html")))
//        {
//            Console.WriteLine("Warning: Angular build is missing. Please run 'ng build --configuration production' in ClientApp.");
//        }
//    }
//});

app.Run();