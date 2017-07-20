using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmountInWords
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Please enter Dollar and Cents(split by '.'):");

			string input = Console.ReadLine();
			while (string.IsNullOrEmpty(input) == false)
			{
				DollarsCentsConverter converter = new DollarsCentsConverter();
				try
				{
					converter.Check(input);
					string output = converter.ToWords(input);
					Console.WriteLine(output);
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error: " + ex.Message);
				}
				Console.WriteLine("Please enter Dollar and Cents(split by '.'):");
				input = Console.ReadLine();
			}


			Console.ReadKey();
		}
	}
}
