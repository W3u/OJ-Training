using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmountInWords
{
	public class DollarsCentsConverter
	{
		private IDictionary<int, string> _numberDictionary;

		private string[] _units = new string[4] { "billion", "million", "thousand", "hundred" };

		private const string _Dollars = "dollars";
		private const string _Cents = "cents";

		public DollarsCentsConverter()
		{
			_numberDictionary = new Dictionary<int, string>();
			_numberDictionary.Add(1, "one");
			_numberDictionary.Add(2, "two");
			_numberDictionary.Add(3, "three");
			_numberDictionary.Add(4, "four");
			_numberDictionary.Add(5, "five");

			_numberDictionary.Add(6, "six");
			_numberDictionary.Add(7, "seven");
			_numberDictionary.Add(8, "eight");
			_numberDictionary.Add(9, "nine");
			_numberDictionary.Add(10, "ten");

			_numberDictionary.Add(11, "eleven");
			_numberDictionary.Add(12, "twelve");
			_numberDictionary.Add(13, "thirteen");
			_numberDictionary.Add(14, "fourteen");
			_numberDictionary.Add(15, "fifteen");

			_numberDictionary.Add(16, "sixteen");
			_numberDictionary.Add(17, "seventeen");
			_numberDictionary.Add(18, "eighteen");
			_numberDictionary.Add(19, "nineteen");
			_numberDictionary.Add(20, "twenty");

			_numberDictionary.Add(30, "thirty");
			_numberDictionary.Add(40, "forty");
			_numberDictionary.Add(50, "fifty");
			_numberDictionary.Add(60, "sixty");
			_numberDictionary.Add(70, "seventy");

			_numberDictionary.Add(80, "eighty");
			_numberDictionary.Add(90, "ninety");
		}


		public void Check(string input)
		{
			decimal amount;
			string strDollars = string.Empty;
			string strCents = string.Empty;
			int dollars;
			int cents;

			if (input.Contains('.'))
			{
				if (decimal.TryParse(input, out amount) == false)
					throw new Exception("invalid input.");

				strDollars = input.Split('.')[0];
				if (int.TryParse(strDollars, out dollars) == false || dollars >= 2000000000)
					throw new Exception("invalid dollars input.");

				strCents = input.Split('.')[1];
				if (int.TryParse(strCents, out cents) == false || cents.ToString().Length > 2)
					throw new Exception("invalid cents input.");
			}
			else
			{
				strDollars = input;
				if (int.TryParse(strDollars, out dollars) == false || dollars >= 2000000000)
					throw new Exception("invalid input.");
			}
		}

		public string ToWords(string input)
		{
			string strDollars = string.Empty;
			string strCents = string.Empty;
			if (input.Contains('.'))
			{
				strDollars = input.Split('.')[0];
				strCents = input.Split('.')[1];
			}
			else
			{
				strDollars = input;
			}

			string dollarsInWords = DollarsToWords(strDollars);
			string centsInWords = CentsToWords(strCents);

			if (dollarsInWords.Length != 0 && centsInWords.Length != 0)
				return dollarsInWords + " and " + centsInWords;
			else if (dollarsInWords.Length != 0)
				return dollarsInWords;
			else if (centsInWords.Length != 0)
				return centsInWords;
			else
				return string.Empty;
		}

		private string DollarsToWords(string strDollars)
		{
			if (string.IsNullOrEmpty(strDollars))
				return string.Empty;

			StringBuilder sbOutout = new StringBuilder();
			string strDollarsTemp = strDollars;
			int groupLength = 3;
			int mod = strDollarsTemp.Length % groupLength;
			for (int i = 0; i < groupLength - mod; i++)
			{
				strDollarsTemp = "0" + strDollarsTemp;
			}

			int groupCount = strDollarsTemp.Length / groupLength;
			for (int i = 0; i < groupCount; i++)
			{
				string group = strDollarsTemp.Substring(0, groupLength);
				int firstNumber = int.Parse(group[0].ToString());
				int secondNumber = int.Parse(group[1].ToString());
				int thirdNumber = int.Parse(group[2].ToString());

				if (firstNumber != 0)
				{
					sbOutout.Append(_numberDictionary[firstNumber]);
					sbOutout.Append(" " + _units[3]);
				}

				int secondThirdSum = secondNumber * 10 + thirdNumber;
				if (secondThirdSum > 0)
				{
					if (firstNumber != 0)
						sbOutout.Append(" and ");
					if (_numberDictionary.ContainsKey(secondThirdSum))
					{
						sbOutout.Append(_numberDictionary[secondThirdSum]);
					}
					else
					{
						sbOutout.Append(_numberDictionary[secondNumber * 10]);
						sbOutout.Append("-");
						sbOutout.Append(_numberDictionary[thirdNumber]);
					}
				}
				//append unit and ,
				if (firstNumber != 0 || secondNumber != 0 || thirdNumber != 0)
				{
					string unit = GetUnit(strDollarsTemp.Length);
					if (string.IsNullOrEmpty(unit) == false)
					{
						sbOutout.Append(" ");
						sbOutout.Append(GetUnit(strDollarsTemp.Length));
						sbOutout.Append(",");
					}
				}
				strDollarsTemp = strDollarsTemp.Remove(0, 3);
			}
			sbOutout.Append(" ").Append(_Dollars);

			return sbOutout.ToString();
		}

		private string CentsToWords(string strCents)
		{
			if (string.IsNullOrEmpty(strCents))
				return string.Empty;

			StringBuilder sbOutout = new StringBuilder();
			int firstNumber = int.Parse(strCents[0].ToString());
			int secondNumber = int.Parse(strCents[1].ToString());

			int firstSecondSum = firstNumber * 10 + secondNumber;
			if (firstSecondSum > 0)
			{
				if (_numberDictionary.ContainsKey(firstSecondSum))
				{
					sbOutout.Append(_numberDictionary[firstSecondSum]);
				}
				else
				{
					sbOutout.Append(_numberDictionary[firstNumber * 10]);
					sbOutout.Append("-");
					sbOutout.Append(_numberDictionary[secondNumber]);
				}

				sbOutout.Append(" ").Append(_Cents);
			}

			return sbOutout.ToString();
		}


		private string GetUnit(int length)
		{
			if (length >= 10)
				return _units[0];
			else if (length >= 7)
				return _units[1];
			else if (length >= 4)
				return _units[2];
			else
				return string.Empty;
		}

	}
}
