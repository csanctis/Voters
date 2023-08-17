namespace Voters.Core.Models.Responses
{
	public class CastedVoteResponse
	{
		public CastedVoteResponse()
		{
			Response = "Your vote has been computed.";
		}

		public string Response { get; set; }
	}
}