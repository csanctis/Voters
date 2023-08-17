namespace Voters.Core.Models.Responses
{
	public class ElectionResultsResponse
	{
		public List<KeyValuePair<string, int>> Results { get; set; }
		public string Response { get; set; }
	}
}