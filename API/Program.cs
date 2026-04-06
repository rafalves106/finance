using Microsoft.EntityFrameworkCore;
using Finance.Core.Repositories;
using Finance.Core.UseCases;
using Finance.Infrastructure.Repositories;
using Finance.Infrastructure.Data;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<MovimentacaoDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOpenApi();

builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
builder.Services.AddScoped<CriarMovimentacaoUseCase>();
builder.Services.AddScoped<ListarMovimentacoesUseCase>();
builder.Services.AddScoped<AtualizarMovimentacaoUseCase>();
builder.Services.AddScoped<RemoverMovimentacaoUseCase>();
builder.Services.AddScoped<BuscarMovimentacaoUseCase>();
builder.Services.AddScoped<BuscarEntradaUseCase>();
builder.Services.AddScoped<BuscarSaidaUseCase>();
builder.Services.AddScoped<BuscarMovimentacoesPorPeriodoUseCase>();
builder.Services.AddScoped<BuscarEntradasPorPeriodoUseCase>();
builder.Services.AddScoped<BuscarSaidasPorPeriodoUseCase>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Finance API V1");
    });
}

app.MapControllers();
app.Run();