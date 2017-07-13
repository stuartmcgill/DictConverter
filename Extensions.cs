using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictConverter
{
	static class Extensions
	{
		/// <summary>
		/// Returns the first few characters of the string with a length
		/// specified by the given parameter. If the string's length is less than the 
		/// given length the complete string is returned. If length is zero or 
		/// less an empty string is returned
		/// </summary>
		/// <param name="s">the string to process</param>
		/// <param name="length">Number of characters to return</param>
		/// <returns></returns>
		public static string Left(this string s, int length)
		{
			length = Math.Max(length, 0);

			if (s.Length > length)
			{
				return s.Substring(0, length);
			}
			else
			{
				return s;
			}
		}

		/// <summary>
		/// Returns the last few characters of the string with a length
		/// specified by the given parameter. If the string's length is less than the 
		/// given length the complete string is returned. If length is zero or 
		/// less an empty string is returned
		/// </summary>
		/// <param name="s">the string to process</param>
		/// <param name="length">Number of characters to return</param>
		/// <returns></returns>
		public static string Right(this string s, int length)
		{
			length = Math.Max(length, 0);

			if (s.Length > length)
			{
				return s.Substring(s.Length - length, length);
			}
			else
			{
				return s;
			}
		}

		public static bool IsCicipu(this Contributor c)
		{
			if (c.EthnicGroupId <= 7)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool IsCicipu(this ContributorLanguage cl)
		{
			if (cl.LanguageId <= 7)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool IsHausa(this ContributorLanguage cl)
		{
			if (cl.LanguageId == 8)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		
	}
}
