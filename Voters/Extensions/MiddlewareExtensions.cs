using System.Diagnostics.CodeAnalysis;
using Voters.Middlewares;

namespace Voters.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class MiddlewareExtensions
	{
		public static IApplicationBuilder UseContextMiddleWare(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ContextMiddleware>();
		}

		public static IApplicationBuilder UseLoggingMiddleWare(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<LoggingMiddleware>();
		}
	}
}