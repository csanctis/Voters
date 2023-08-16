using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Voters.Core.Extensions;
using Voters.Core.Models.Config;
using Voters.Core.Models.Infrastructure;
using Voters.Core.Models.Requests;
using Voters.Core.Models.Responses;
using Voters.Core.Repositories;

namespace Voters.Core.Services
{
	public interface IVoteService
	{
		Task<ServiceResponse<CastedVoteResponse>> CastVote(ContextBase context, CastVoteRequest request);

		Task<ElectionResultsResponse> GetResults(ContextBase context);
	}

	public class VoteService : IVoteService
	{
		private readonly ILogger<VoteService> _logger;
		private readonly IOptions<AppConfig> _appConfig;
		private readonly IVoteRepository _iVoteRepository;

		public VoteService(IOptions<AppConfig> appConfig, ILogger<VoteService> logger, IVoteRepository iVoteRepository)
		{
			_appConfig = appConfig;
			_logger = logger;
			_iVoteRepository = iVoteRepository;
		}

		public async Task<ServiceResponse<CastedVoteResponse>> CastVote(ContextBase context, CastVoteRequest request)
		{
			_logger.LogTrace("Starting to CastVote");
			var response = new ServiceResponse<CastedVoteResponse>();

			var result = await _iVoteRepository.CastVote(request);

			return result
				? response.With(new CastedVoteResponse(result))
				: response.WithError(ServiceResponseErrorType.SystemError, new List<ResponseCode>() {
					new ResponseCode ("1000", "A problem happened.")
				});
		}

		public async Task<ElectionResultsResponse> GetResults(ContextBase context)
		{
			_logger.LogTrace("Getting Vote Results");

			return await _iVoteRepository.GetElectionResults();
		}
	}
}