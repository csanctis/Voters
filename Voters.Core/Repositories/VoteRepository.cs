using Voters.Core.Models.Requests;
using Voters.Core.Models.Responses;

namespace Voters.Core.Repositories
{
	public interface IVoteRepository
	{
		Task<bool> CastVote(CastVoteRequest castVoteRequest);

		Task<ElectionResultsResponse> GetElectionResults();
	}

	public class VoteRepository : IVoteRepository
	{
		private Dictionary<string, int> _ballot;

		public VoteRepository()
		{
			_ballot = new Dictionary<string, int>();
		}

		public async Task<bool> CastVote(CastVoteRequest castVoteRequest)
		{
			// Check if the key exists in the dictionary
			if (_ballot.ContainsKey(castVoteRequest.Candidate))
			{
				// Increment the existing value
				_ballot[castVoteRequest.Candidate] += 1;
			}
			else
			{
				// Add the key with the specified incrementValue
				_ballot[castVoteRequest.Candidate] = 1;
			}
			return await Task.FromResult(true);
		}

		public async Task<ElectionResultsResponse> GetElectionResults()
		{
			var electionResults = new ElectionResultsResponse();

			if (_ballot.Count == 0)
			{
				electionResults.Response = "Sorry, no votes yet";
			}
			else
			{
				List<KeyValuePair<string, int>> sortedList = _ballot.ToList().OrderByDescending(item => item.Value).ToList();
				if (sortedList.Count > 3)
				{
					electionResults.Results = sortedList.Take(3).ToList();
				}
				else
				{
					electionResults.Results = sortedList;
				}
				electionResults.Response = $"At the moment, {electionResults.Results.First().Key} is winning with {electionResults.Results.First().Value} votes";
			}

			return await Task.FromResult(electionResults);
		}
	}
}