using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsConverter
{
	public class RomanNumeral
	{
		public RomanNumeral(int arabic, string roman)
		{
			Arabic = arabic;
			Roman = roman;
		}

		/// <summary>
		/// Arabic Number
		/// </summary>
		public int Arabic { get; set; }

		/// <summary>
		/// Roman Number
		/// </summary>
		public string Roman { get; set; }

		/// <summary>
		/// the length of Arabic Number
		/// </summary>
		public int LengthOfArabic
		{
			get { return Arabic.ToString().Length; }
		}
	}
}
