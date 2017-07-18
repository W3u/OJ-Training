using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumeralsConverter
{
	class Program
	{
		static void Main(string[] args)
		{
			ConsoleConversionTable();

			Console.WriteLine("please enter a number(0<number<4000):");

			string input = Console.ReadLine();
			while (string.IsNullOrEmpty(input) == false)
			{
				RomanConverter romanConverter = new RomanConverter();
				try
				{
					string output = romanConverter.ToRomanNumber(input);
					Console.WriteLine(output);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				input = Console.ReadLine();
			}

			Console.ReadKey();
		}

		static void ConsoleConversionTable()
		{
			Console.WriteLine("Roman numerals conversion table:");
			Console.WriteLine("Number\t	Roman numeral\t");
			Console.WriteLine("1\t	 I\t");
			Console.WriteLine("5\t	 V\t");
			Console.WriteLine("10\t	 X\t");
			Console.WriteLine("50\t	 L\t");
			Console.WriteLine("100\t	 C\t");
			Console.WriteLine("500\t	 D\t");
			Console.WriteLine("1000\t	 M\t");
			Console.WriteLine("---------------------------------");
		}
	}
}
