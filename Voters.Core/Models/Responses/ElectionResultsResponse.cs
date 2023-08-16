namespace Voters.Core.Models.Responses
{
	public class ElectionResultsResponse
	{
		public Dictionary<string, int> Results { get; set; }
		public string Response { get; set; }
	}
}