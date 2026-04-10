using Microsoft.EntityFrameworkCore;
using Finance.Core.Repositories;
using Finance.Core.UseCases;
using Finance.Infrastructure.Repositories;
using Finance.Infrastructure.Data;
using System.Text.Json.Serialization;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<FinanceDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOpenApi();

builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
builder.Services.AddScoped<IInvestimentoRepository, InvestimentoRepository>();

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

builder.Services.AddScoped<CriarInvestimentoUseCase>();
builder.Services.AddScoped<ListarInvestimentosUseCase>();
builder.Services.AddScoped<ObterInvestimentoPorIdUseCase>();
builder.Services.AddScoped<RealizarAporteUseCase>();
builder.Services.AddScoped<RealizarSaqueUseCase>();
builder.Services.AddScoped<AtualizarSaldoInvestimentoUseCase>();
builder.Services.AddScoped<RemoverInvestimentoUseCase>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Finance API V1");
    });
}

app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.MapControllers();
app.Run();