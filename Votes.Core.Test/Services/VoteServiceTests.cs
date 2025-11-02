using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Voters.Core.Models.Config;
using Voters.Core.Models.Requests;
using Voters.Core.Models.Responses;
using Voters.Core.Repositories;
using Voters.Core.Services;
using Votes.Core.Test.Infrastructure;

namespace Votes.Core.Test.Services
{
    public class VoteServiceTests : BaseTests
    {
        private readonly VoteService _voteService;
        private readonly IOptions<AppConfig> _appConfig = Substitute.For<IOptions<AppConfig>>();
        private readonly ILogger<VoteService> _logger = Substitute.For<ILogger<VoteService>>();
        private readonly IVoteRepository _IvoteRepository = Substitute.For<IVoteRepository>();

        public VoteServiceTests()
        {
            _voteService = new VoteService(_appConfig, _logger, _IvoteRepository);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Check_Cast_Vote_Success()
        {
            // Prepare
            var context = GetValidContextModel();

            _IvoteRepository.CastVote(Arg.Any<CastVoteRequest>()).Returns(true);

            var vote = _voteService.CastVote(context, new CastVoteRequest());

            Assert.That(vote, Is.Not.Null);
            Assert.That(vote.IsCompletedSuccessfully, Is.True);
            Assert.That(vote.Result.IsValid, Is.True);
            Assert.That(vote.Result.Value.Response, Is.EqualTo("Your vote has been computed."));
        }

        [Test]
        public void Check_Cast_Vote_Fail()
        {
            // Prepare
            var context = GetValidContextModel();

            _IvoteRepository.CastVote(Arg.Any<CastVoteRequest>()).Returns(false);

            var vote = _voteService.CastVote(context, new CastVoteRequest());

            Assert.That(vote, Is.Not.Null);
            Assert.That(vote.IsCompletedSuccessfully, Is.True);
            Assert.That(vote.Result.IsValid, Is.False);
            Assert.That(vote.Result.ResponseCodes.Count, Is.EqualTo(1));
            Assert.That(vote.Result.ResponseCodes.First().Message, Is.EqualTo("A problem happened."));
            Assert.That(vote.Result.ResponseCodes.First().Code, Is.EqualTo("1000"));
        }

        [Test]
        public void Check_Get_Vote_Success()
        {
            // Prepare
            var context = GetValidContextModel();

            _IvoteRepository.GetElectionResults().Returns(GetElectionResults());

            var vote = _voteService.GetResults(context);

            Assert.That(vote, Is.Not.Null);
            Assert.That(vote.IsCompletedSuccessfully, Is.True);
            Assert.That(vote.Result.Results.Count, Is.EqualTo(4));
        }

        private ElectionResultsResponse GetElectionResults()
        {
            var electionResults = new ElectionResultsResponse();
            electionResults.Results = new List<KeyValuePair<string, int>>(){
                new KeyValuePair<string, int>("John Doe", 10),
                new KeyValuePair<string, int>("Mary", 5),
                new KeyValuePair<string, int>("Robert", 3),
                new KeyValuePair<string, int>("Jake", 1),
            };

            return electionResults;
        }
    }
}