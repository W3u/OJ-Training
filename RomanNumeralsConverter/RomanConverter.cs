using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsConverter
{
	/// <summary>
	/// the convert between Roman number and Decimal number
	/// </summary>
	public class RomanConverter
	{
		/// <summary>
		/// the map from Decimal number to Roman number
		/// </summary>
		private IDictionary<int, string> _romanNumeralsDictionary;

		private const int _MinNumber = 1;
		private const int _MaxNumber = 3999;

		public RomanConverter()
		{
			_romanNumeralsDictionary = new Dictionary<int, string>();
			_romanNumeralsDictionary.Add(1, "I");
			_romanNumeralsDictionary.Add(5, "V");
			_romanNumeralsDictionary.Add(10, "X");
			_romanNumeralsDictionary.Add(50, "L");
			_romanNumeralsDictionary.Add(100, "C");

			_romanNumeralsDictionary.Add(500, "D");
			_romanNumeralsDictionary.Add(1000, "M");
		}



		#region ToDecimalNumber

		/// <summary>
		/// Convert roman number to decimal number
		/// </summary>
		/// <param name="romanNumber"></param>
		/// <returns></returns>
		public string ToDecimalNumber(string romanNumber)
		{
			StringBuilder sbOutput = new StringBuilder();





			return string.Empty;
		}

		/// <summary>
		/// Basic validation
		/// </summary>
		/// <param name="input"></param>
		private void CheckRomanInput(string input)
		{
			foreach (var VARIABLE in input)
			{
				
			}
		}

		#endregion



		#region ToRomanNumber

		/// <summary>
		/// Convert decimal number to roman number
		/// </summary>
		/// <param name="decimalNumber"></param>
		/// <returns></returns>
		public string ToRomanNumber(string decimalNumber)
		{
			CheckDecimalInput(decimalNumber);

			StringBuilder sbOutput = new StringBuilder();
			for (int i = 0; i < decimalNumber.Length; i++)
			{
				int power = decimalNumber.Length - i - 1;
				//char convert to int
				int number = int.Parse(decimalNumber[i].ToString());
				number = (int)(number * Math.Pow(10, power));

				sbOutput.Append(ParseDecimal(number));
			}
			return sbOutput.ToString();
		}

		/// <summary>
		/// Basic validation
		/// </summary>
		/// <param name="input"></param>
		private void CheckDecimalInput(string input)
		{
			int number;
			if (int.TryParse(input, out number) == false)
				throw new Exception("invalid input,please enter a number(0<number<4000):");

			if (number < _MinNumber || number > _MaxNumber)
				throw new Exception("invalid input,please enter a number(0<number<4000):");
		}

		private string ParseDecimal(int number)
		{
			string retString = string.Empty;
			if (number <= 0)
				return retString;

			int tempNumber = number;
			while (tempNumber > 0)
			{
				foreach (int keyValue in _romanNumeralsDictionary.Keys.OrderByDescending(s => s))
				{
					if (tempNumber - keyValue < 0)
						continue;

					retString += _romanNumeralsDictionary[keyValue];
					tempNumber -= keyValue;
					break;
				}
			}

			//handle the number start with 4 or 9
			int maxRepeatTimes = 3;
			char repeatChar = retString[retString.Length - 1];
			int repeatTimes = retString.Count(s => s == repeatChar);
			if (repeatTimes > maxRepeatTimes)
			{
				retString = repeatChar.ToString();
				int repeatCharNumber = _romanNumeralsDictionary.FirstOrDefault(s => s.Value == repeatChar.ToString()).Key;
				retString += _romanNumeralsDictionary[repeatCharNumber + number];
			}

			return retString;
		}

		#endregion

	}
}
