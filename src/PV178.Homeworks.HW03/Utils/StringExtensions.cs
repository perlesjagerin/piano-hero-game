using System;
using System.Collections.Generic;
using System.Linq;

namespace PV178.Homeworks.HW03.Utils
{
	public static class StringExtensions
	{
		private static List<Func<int, int, bool>> conditions = new List<Func<int, int, bool>>
		{
			(x, y) => x % 3 == 0 && y % 5 != 0,
			(x, y) => y % 5 == 0 && x % 3 != 0,
			(x, y) => (x + y) % 7 == 0
		};

		public static bool IsCodePremium(this string code)
		{
			string[] parts = code.Split('I');

			if (parts.Length != 2)
			{
				return false;
			}

			string xStr = parts[0];
			string yStr = parts[1];

			if (xStr.Length != 2 || yStr.Length != 2 || xStr[0] == '0' || yStr[0] == '0')
			{
				return false;
			}

			if (!int.TryParse(xStr, out int x) || !int.TryParse(yStr, out int y))
			{
				return false;
			}

			return conditions.Any(condition => condition(x, y));
		}
	}
}

