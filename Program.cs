var builder = WebApplication.CreateBuilder(args);
//swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dotNet 10 later
//builder.Services.AddValidation();

var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.InjectStylesheet("/swagger-ui.css");
    x.DefaultModelExpandDepth(-1);
});

app.MapControllers();

app.Run();