using Voters.Core.Models.Requests;
using Voters.Core.Repositories;
using Votes.Core.Test.Infrastructure;

namespace Votes.Core.Test.Repositories
{
	public class VoteRepositoryTests : BaseTests
	{
		private readonly VoteRepository _repository;

		public VoteRepositoryTests()
		{
			_repository = new VoteRepository();
		}

		[Test]
		public async Task Check_Get_Vote_Sorting()
		{
			// Prepare
			await _repository.CastVote(new CastVoteRequest() { Candidate = "Mary" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "John" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "John" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "Peter" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "Dave" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "John" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "Jack" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "John" });
			await _repository.CastVote(new CastVoteRequest() { Candidate = "Mary" });

			var vote = _repository.GetElectionResults();

			Assert.That(vote, Is.Not.Null);
			Assert.That(vote.IsCompletedSuccessfully, Is.True);
			Assert.That(vote.Result.Results.Count, Is.EqualTo(3)); // Only Top 3 should show
			Assert.That(vote.Result.Response, Is.EqualTo("At the moment, John is winning with 4 votes"));
		}
	}
}