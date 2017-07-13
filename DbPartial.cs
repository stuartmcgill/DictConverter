using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictConverter
{
	partial class Sens
	{
		public List<string> TempXReferences = new List<string>();
	}

	partial class LexemeEntry
	{
		public string TempMainEntry { get; set; }
	}
}
