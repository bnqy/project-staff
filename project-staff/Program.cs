using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using project_staff.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));

builder.Services.ConfigCors();
builder.Services.ConfigLoggerService();
builder.Services.ConfigRepositoryManager();
builder.Services.ConfigServiceManager();
builder.Services.ConfigSqlContext(builder.Configuration);

builder.Services.AddControllers()
	.AddApplicationPart(typeof(project_staff.Presentation.AssemblyReference).Assembly); //To use Controllers in Presentation project.



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
