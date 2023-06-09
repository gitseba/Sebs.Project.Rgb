using Sebs.Server.AspCoreApi.Rgb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register SignalR services

builder.Services.AddCors(options =>
{
    options.AddPolicy("DisableCorsPolicy",
        builder =>
        {
            builder.WithOrigins("https://localhost:7283")
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddSignalR();

/**************************************************************************/
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("DisableCorsPolicy");
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();


// Use SignalR
app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapHub<RgbHub>("/rgb");
    });

app.Run();
