using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictConverter
{
	public static class Utils
	{
		public static void MakeOrthographic(List<Lexeme> lexemes)
		{
			for(int index = 0; index < lexemes.Count; index++)
			{
				MakeOrthographic(lexemes[index]);
			}
		}

		public static void MakeOrthographic(Lexeme lexeme)
		{
			List<LexemeEntry> entries = lexeme.LexemeEntries1.ToList();

			for (int jIndex = 0; jIndex < entries.Count; jIndex++)
			{
				LexemeEntry entry = entries[jIndex];

				entry.CitationOrtho = ConvertOrthographic(entry.Citation);

				if(entry.PluralForm != null)
				{
					entry.PluralForm = ConvertOrthographic(entry.PluralForm);
				}

				if (entry.SingularForm != null)
				{
					entry.SingularForm = ConvertOrthographic(entry.SingularForm);
				}

				if (entry.VerbalComment != null)
				{
					entry.VerbalComment = ConvertOrthographic(entry.VerbalComment);
				}
			}

			lexeme.LexemeOrtho = ConvertOrthographic(lexeme.Lexeme1);
		}

		private static string ConvertOrthographic(string raw)
		{
			string ortho = raw;

			ortho = ortho.Replace("ã́", "ã");
			ortho = ortho.Replace("ẽ́", "ẽ");
			ortho = ortho.Replace("ĩ́", "ĩ");
			ortho = ortho.Replace("ṍ", "õ");
			ortho = ortho.Replace("ø̃́", "ø̃");
			ortho = ortho.Replace("ṹ", "ũ");

			ortho = ortho.Replace("ã̀", "ã");
			ortho = ortho.Replace("ẽ̀", "ẽ");
			ortho = ortho.Replace("ĩ̀", "ĩ");
			ortho = ortho.Replace("õ̀", "õ");
			ortho = ortho.Replace("ø̃̀", "ø̃");
			ortho = ortho.Replace("ũ̀", "ũ");

			ortho = ortho.Replace("ã̂", "ã");
			ortho = ortho.Replace("ẽ̂", "ẽ");
			ortho = ortho.Replace("ĩ̂", "ĩ");
			ortho = ortho.Replace("õ̂", "õ");
			ortho = ortho.Replace("ø̃̂", "ø̃");
			ortho = ortho.Replace("ũ̂", "ũ");

			ortho = ortho.Replace("á", "a");
			ortho = ortho.Replace("é", "e");
			ortho = ortho.Replace("í", "i");
			ortho = ortho.Replace("ó", "o");
			ortho = ortho.Replace("ǿ", "ø");
			ortho = ortho.Replace("ú", "u");

			ortho = ortho.Replace("à", "a");
			ortho = ortho.Replace("è", "e");
			ortho = ortho.Replace("ì", "i");
			ortho = ortho.Replace("ò", "o");
			ortho = ortho.Replace("ø̀", "ø");
			ortho = ortho.Replace("ù", "u");

			ortho = ortho.Replace("â", "a");
			ortho = ortho.Replace("ê", "e");
			ortho = ortho.Replace("î", "i");
			ortho = ortho.Replace("ô", "o");
			ortho = ortho.Replace("ø̂", "ø");
			ortho = ortho.Replace("û", "u");

			//capitals
			ortho = ortho.Replace("Á", "A");
			ortho = ortho.Replace("É", "E");
			ortho = ortho.Replace("Í", "I");
			ortho = ortho.Replace("Ó", "O");
			ortho = ortho.Replace("Ǿ", "Ø");
			ortho = ortho.Replace("Ú", "U");

			ortho = ortho.Replace("À", "A");
			ortho = ortho.Replace("È", "E");
			ortho = ortho.Replace("Ì", "I");
			ortho = ortho.Replace("Ò", "O");
			ortho = ortho.Replace("Ø̀", "Ø");
			ortho = ortho.Replace("Ù", "U");

			return ortho;
		}
	}
}