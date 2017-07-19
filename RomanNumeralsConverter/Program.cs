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

			Console.WriteLine("Option 1: Convert Roman numeral to Decimal number(VII -> 7).");
			Console.WriteLine("Option 2: Convert Decimal numeral to Roman number(7 -> VII).");
			Console.WriteLine("Please choose a number(1 or 2):");

			string inputOption = Console.ReadLine();
			while (string.IsNullOrEmpty(inputOption) == false)
			{
				int option;
				if (int.TryParse(inputOption, out option) == false || !(option == 1 || option == 2))
				{
					Console.WriteLine("Error: invalid input,please choose a number(1 or 2):");
				}
				else
				{
					RomanConverterV2 romanConverter = new RomanConverterV2();
					try
					{
						string inputNumber = string.Empty;
						string output = string.Empty;
						if (option == 1)
						{
							Console.WriteLine("Please enter a Roman numeral(0<roman number's value<4000):");
							inputNumber = Console.ReadLine();
							output = romanConverter.ToDecimalNumber(inputNumber);
						}
						else if (option == 2)
						{
							Console.WriteLine("Please enter a Decimal number(0<decimal number's value<4000):");
							inputNumber = Console.ReadLine();
							output = romanConverter.ToRomanNumber(inputNumber);
						}
						Console.WriteLine($"{inputNumber} --> {output}");
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: " + ex.Message);
					}
				}
				Console.WriteLine("Please choose a number(1 or 2):");
				inputOption = Console.ReadLine();
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
