﻿using Microsoft.AspNetCore.Diagnostics;
using project_staff.Contracts;
using project_staff.Entities.ErrorModel;
using System.Net;

namespace project_staff.Extensions
{
	public static class ExceptionMiddlewareExtensions
	{
		public static void ConfigExceptionHandler(this WebApplication app, ILoggerManager logger)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						logger.LogError($"Something went wrong: {contextFeature.Error}");
						await context.Response.WriteAsync(new ErrorDetails()
						{
							StatusCode = context.Response.StatusCode,
							Message = "Internal Server Error.",
						}.ToString());
					}
				});
			});
		}
	}
}
