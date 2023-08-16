namespace Voters.Core.Models.Responses
{
	public class CastedVoteResponse
	{
		public CastedVoteResponse(bool isSuccess)
		{
			if (isSuccess)
			{
				Response = "Your vote has been computed.";
			}
			else
			{
				Response = "There was a problem casting your vote. Please try again.";
			}
		}

		public string Response { get; set; }
	}
}