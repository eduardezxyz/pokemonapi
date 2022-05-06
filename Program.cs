using System.Text.Json.Serialization;
using Demoapi.EntityModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<pokedbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("pokedb")));

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers().AddOData(opt => {
    opt.AddRouteComponents("odata", GetEdmModel());
    opt.Filter().Select().Expand().OrderBy().Count().SetMaxTop(null);
});

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    builder.EntitySet<PokeTrainer>("PokeTrainers").EntityType.HasKey(c => c.Id).Name = "PokeTrainer";
    builder.EntitySet<Pokemon>("Pokemons").EntityType.HasKey(c => c.PokedexEntry).Name = "Pokemon";
    // builder.EntitySet<Pokedetail>("Pokedetails").EntityType.HasKey(c => c.PokedexEntry).Name = "Pokedetail";
    // builder.EntitySet<PokeType>("PokeTypes").EntityType.HasKey(c => c.Id).Name = "PokeType";
    builder.EntitySet<MyPokemon>("MyPokemons").EntityType.HasKey(c => c.Id).Name = "MyPokemon";
    // builder.EntitySet<Trainerpokevw>("Trainerpokevws").EntityType.HasKey(c => c.Id).Name = "Trainerpokevw";
    builder.EntitySet<PokeParty>("PokeParties").EntityType.HasKey(c => c.TrainerId).Name = "PokeParty";

    return builder.GetEdmModel();
}

var app = builder.Build();
app.UseCors("corsapp");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
