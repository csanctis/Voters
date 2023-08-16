using Microsoft.AspNetCore.Mvc;
using Voters.Core.Models.Requests;
using Voters.Core.Models.Responses;
using Voters.Core.Services;

namespace Voters.Endpoints
{
	public class VoteEndpoints
	{
		public static void MapEndpoints(WebApplication app)
		{
			app.MapPost("vote/", CastVote);
			app.MapGet("vote/results", GetResults);
		}

		internal static async Task<CastedVoteResponse> CastVote(
			[FromServices] IContextService contextService,
			[FromServices] IVoteService voteService,
			[FromBody] CastVoteRequest request)
		{
			return await voteService.CastVote(contextService.GetContext(), request);
		}

		internal static async Task<ElectionResultsResponse> GetResults(
			[FromServices] IContextService contextService,
			[FromServices] IVoteService voteService)
		{
			return await voteService.GetResults(contextService.GetContext());
		}
	}
}