using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NLog;
using project_staff.Contracts;
using project_staff.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), @"\nlog.config"));

builder.Services.ConfigCors();
builder.Services.ConfigLoggerService();
builder.Services.ConfigRepositoryManager();
builder.Services.ConfigServiceManager();
builder.Services.ConfigSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigResponseCaching();
builder.Services.ConfigHttpCacheHeaders();

builder.Services.AddAuthentication();
builder.Services.ConfigIdentity();
builder.Services.ConfigJWT(builder.Configuration);


builder.Services.Configure<ApiBehaviorOptions>(options => // Enable custom responces.
{
	options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(config =>
{
	config.RespectBrowserAcceptHeader = true;
	config.ReturnHttpNotAcceptable = true;
	config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
	{
		Duration = 120
	});
})
	.AddXmlDataContractSerializerFormatters()
	.AddApplicationPart(typeof(project_staff.Presentation.AssemblyReference).Assembly); //To use Controllers in Presentation project.



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
	s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Place to add JWT with Bearer",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	s.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
						Name = "Bearer",
					},
					new List<string>()
				}
			});
});

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
	await SeedRolesAsync(roleManager);
}

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(s =>
	{
		s.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Staff SIBERS API");
	});
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
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
{
	string[] roles = { "руководитель", "менеджер проекта", "сотрудник" };
	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			// Создаем новую роль с типом IdentityRole<Guid>
			var newRole = new IdentityRole<Guid>(role)
			{
				NormalizedName = role.ToUpperInvariant()
			};
			await roleManager.CreateAsync(newRole);
		}
	}
}