using Air;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBasic();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.UseHealthCheck();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewares();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.ConfigureCors();
app.UseHealthcheck();

app.Run();