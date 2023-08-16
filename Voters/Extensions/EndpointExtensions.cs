using System.Diagnostics.CodeAnalysis;
using Voters.Endpoints;

namespace Voters.Extensions
{
	[ExcludeFromCodeCoverage]
	public static class EndpointExtensions
	{
		public static void UseEndpointExtensions(this WebApplication app)
		{
			PingEndpoints.MapEndpoints(app);
			VoteEndpoints.MapEndpoints(app);
		}
	}
}