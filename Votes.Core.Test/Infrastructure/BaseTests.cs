using Voters.Core.Models.Infrastructure;

namespace Votes.Core.Test.Infrastructure
{
	public abstract class BaseTests
	{
		protected const string StringRandomToken = "#STRING#";
		protected static readonly Random rd = new();

		protected static void SetProperty(string compoundProperty, object target, object value)
		{
			if (value != null && value is string && value.ToString().StartsWith(StringRandomToken))
			{
				int stringLength = Convert.ToInt32(value.ToString().Replace(StringRandomToken, string.Empty));
				value = CreateString(stringLength);
			}

			string[] bits = compoundProperty.Split('.');
			for (int i = 0; i < bits.Length - 1; i++)
			{
				var propertyToGet = target.GetType().GetProperty(bits[i]);
				target = propertyToGet.GetValue(target, null);
			}
			var propertyToSet = target.GetType().GetProperty(bits.Last());
			propertyToSet.SetValue(target, value, null);
		}

		protected static string CreateString(int stringLength)
		{
			const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
			char[] chars = new char[stringLength];

			for (int i = 0; i < stringLength; i++)
			{
				chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
			}

			return new string(chars);
		}

		protected static Context GetValidContextModel()
		{
			return new Context()
			{
				IsValid = true
			};
		}
	}
}