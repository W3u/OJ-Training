using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsConverter
{
	public class RomanConverterV2
	{
		/// <summary>
		/// the collection of Decimal number and Roman number
		/// </summary>
		private IList<RomanNumeral> _romanNumerals;

		private const int _MinNumber = 1;
		private const int _MaxNumber = 3999;

		public RomanConverterV2()
		{
			_romanNumerals = new List<RomanNumeral>();
			_romanNumerals.Add(new RomanNumeral(1, "I"));
			_romanNumerals.Add(new RomanNumeral(5, "V"));
			_romanNumerals.Add(new RomanNumeral(10, "X"));
			_romanNumerals.Add(new RomanNumeral(50, "L"));
			_romanNumerals.Add(new RomanNumeral(100, "C"));

			_romanNumerals.Add(new RomanNumeral(500, "D"));
			_romanNumerals.Add(new RomanNumeral(1000, "M"));
		}



		#region ToDecimalNumber

		/// <summary>
		/// Convert roman number to decimal number
		/// </summary>
		/// <param name="romanNumber"></param>
		/// <returns></returns>
		public string ToDecimalNumber(string romanNumber)
		{
			CheckRomanInput(romanNumber);

			int decimalNumber = 0;
			RomanNumeral lastRoman = _romanNumerals.FirstOrDefault(s => s.Roman == romanNumber[0].ToString());
			int i = 1;
			for (; i < romanNumber.Length; i++)
			{
				RomanNumeral roman = _romanNumerals.FirstOrDefault(s => s.Roman == romanNumber[i].ToString());
				if (lastRoman.Arabic >= roman.Arabic)
				{
					decimalNumber += lastRoman.Arabic;
				}
				else
				{
					decimalNumber -= lastRoman.Arabic;
				}
				lastRoman = roman;
			}
			if (i == romanNumber.Length)
				decimalNumber += lastRoman.Arabic;

			return decimalNumber.ToString();
		}

		/// <summary>
		/// Basic validation
		/// </summary>
		/// <param name="input"></param>
		private void CheckRomanInput(string input)
		{
			var romanList = _romanNumerals.Select(s => s.Roman);
			for (int i = 0; i < input.Length; i++)
			{
				string roman = input[i].ToString();
				if (romanList.Contains(roman) == false)
				{
					throw new Exception($"{roman} is not a valid roman numeral.");
				}
			}

			int rightAddCount = 0;
			int leftSubstractCount = 0;
			int rightMultiplyCount = 0;

			//the max repeat times of roman numeral
			int maxRepeatTimes = 3;
			int maxLeftSubstractCount = 1;
			//左减
			string[] leftSubtractArray = { "I", "X", "C" };

			RomanNumeral lastRoman = _romanNumerals.FirstOrDefault(s => s.Roman == input[0].ToString());
			for (int i = 1; i < input.Length; i++)
			{
				RomanNumeral roman = _romanNumerals.FirstOrDefault(s => s.Roman == input[i].ToString());
				if (lastRoman.Arabic > roman.Arabic)
				{
					rightAddCount++;
					leftSubstractCount = 0;
					rightMultiplyCount = 0;
				}
				else if (lastRoman.Arabic < roman.Arabic)
				{
					if (roman.LengthOfArabic - lastRoman.LengthOfArabic > 1)
						throw new Exception("左减时不可跨越一个位值。");
					if (leftSubtractArray.Contains(roman.Roman) == false)
						throw new Exception("左减的数字有限制，仅限于I、X、C。");

					leftSubstractCount++;
					rightAddCount = 0;
					rightMultiplyCount = 0;
				}
				else
				{
					if (rightMultiplyCount == 0) rightMultiplyCount++;
					rightMultiplyCount++;
					rightAddCount = 0;
					leftSubstractCount = 0;
				}
				if (leftSubstractCount > maxLeftSubstractCount)
					throw new Exception("左减数字必须为一位。");

				if (rightMultiplyCount > maxRepeatTimes)
					throw new Exception("右加数字不可连续超过三位。");

				lastRoman = roman;
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
				foreach (int value in _romanNumerals.Select(s => s.Arabic).OrderByDescending(s => s))
				{
					if (tempNumber - value < 0)
						continue;

					RomanNumeral romanNumeral = _romanNumerals.FirstOrDefault(s => s.Arabic == value);
					if (romanNumeral != null)
						retString += romanNumeral.Roman;
					tempNumber -= value;
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
				RomanNumeral firstRomanNumeral = _romanNumerals.FirstOrDefault(s => s.Roman == repeatChar.ToString());
				if (firstRomanNumeral != null)
				{
					RomanNumeral secondRomanNumeral = _romanNumerals.FirstOrDefault(s => s.Arabic == firstRomanNumeral.Arabic + number);
					if (secondRomanNumeral != null)
						retString += secondRomanNumeral.Roman;
				}
			}

			return retString;
		}

		#endregion
	}
}
