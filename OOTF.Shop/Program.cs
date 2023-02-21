//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using OOTF.Shopping.Helpers;

try
{
    Console.WriteLine($"Starting up, time = {DateTime.UtcNow:s}");
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureServices();
    builder.Configure().Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Application start-up failed. Exception = '{ex.Message}'");
}
