using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsConverter
{
	public class RomanNnumeral
	{
		public RomanNnumeral(int number, string romanNumeral)
		{
			Number = number;
			RomanNumeral = romanNumeral;
		}

		/// <summary>
		/// Decimal number
		/// </summary>
		public int Number { get; set; }

		/// <summary>
		/// Roman Numeral
		/// </summary>
		public string RomanNumeral { get; set; }

		/// <summary>
		/// the length of number
		/// </summary>
		public int Level
		{
			get { return Number.ToString().Length; }
		}
	}
}
