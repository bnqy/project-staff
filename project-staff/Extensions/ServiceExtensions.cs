using Microsoft.EntityFrameworkCore;
using project_staff.Contracts;
using project_staff.LoggerService;
using project_staff.Repository;
using project_staff.Service.Contracts;
using project_staff.Service;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Identity;
using project_staff.Entities.Models;

namespace project_staff.Extensions
{
	/// <summary>
	/// Registers services in the container.
	/// </summary>
	public static class ServiceExtensions
	{
		/// <summary>
		/// Configures CORS.
		/// </summary>
		/// <param name="services">IServiceCollection type.</param>
		public static void ConfigCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
				{
					builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader().WithExposedHeaders("X-Pagination");
				}) ;
			});
		}

		/// <summary>
		/// Configures LoggerService.
		/// </summary>
		/// <param name="services">IServiceCollection type.</param>
		public static void ConfigLoggerService(this IServiceCollection services)
		{
			services.AddSingleton<ILoggerManager, LoggerManager>();
		}

		/// <summary>
		/// Adds Repository Manager to the IoC.
		/// </summary>
		/// <param name="services">IServiceCollection type.</param>
		public static void ConfigRepositoryManager(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryManager, RepositoryManager>();
		}

		/// <summary>
		/// Registers DbContext at Runtime.
		/// </summary>
		/// <param name="services">IServiceCollection type.</param>
		/// <param name="configuration">IConfiguration type.</param>
		public static void ConfigSqlContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
		}

		/// <summary>
		/// Adds Service Manager to the IoC.
		/// </summary>
		/// <param name="services">IServiceCollection type.</param>
		public static void ConfigServiceManager(this IServiceCollection services)
		{
			services.AddScoped<IServiceManager, ServiceManager>();
		}

		/// <summary>
		/// Response caching.
		/// </summary>
		/// <param name="services">IServiceCollection type.</param>
		public static void ConfigResponseCaching(this IServiceCollection services)
		{
			services.AddResponseCaching();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		public static void ConfigHttpCacheHeaders(this IServiceCollection services)
		{
			services.AddHttpCacheHeaders(
				(expirationOpt) =>
				{
					expirationOpt.MaxAge = 120;
					expirationOpt.CacheLocation = CacheLocation.Private;
				},
				(validationOpt) =>
				{
					validationOpt.MustRevalidate = true;
				});
		}

		public static void ConfigIdentity(this IServiceCollection services)
		{
			var builder = services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(o =>
			{
				o.Password.RequireDigit = true;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 10;
				o.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<RepositoryContext>()
			.AddDefaultTokenProviders();
		}
	}
}
