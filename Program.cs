using GameStore.Api.Data;

var builder = WebApplication.CreateBuilder(args);
//swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dotNet 10 later
//builder.Services.AddValidation();

const string connString = "Data Source=GameStore.db;";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.InjectStylesheet("/swagger-ui.css");
    x.DefaultModelExpandDepth(-1);
});

app.MapControllers();
app.MigrateDb();

app.Run();