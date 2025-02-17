﻿using project_staff.Contracts;
using project_staff.LoggerService;

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
					.AllowAnyHeader();
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
	}
}
