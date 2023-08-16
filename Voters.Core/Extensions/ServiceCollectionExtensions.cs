using Microsoft.Extensions.DependencyInjection;
using Voters.Core.Repositories;
using Voters.Core.Services;

namespace Voters.Core.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddCoreServices(this IServiceCollection services)
		{
			// Services
			services.AddSingleton<IVoteService, VoteService>();

			// Repositories
			services.AddSingleton<IVoteRepository, VoteRepository>();
		}
	}
}