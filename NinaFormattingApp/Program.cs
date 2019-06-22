using System;
using System.IO;

namespace NinaFormattingApp
{
	class Program
	{
		static string sourceText;

		static void Main(string[] args)
		{
			Console.WriteLine("\nNINAS FORMATTING APP");
			Console.WriteLine("------------------------------------------------------------------------------------------------\n");

			sourceText = File.ReadAllText("testdata.csv");

			Console.Write(sourceText);

			Console.WriteLine("\nPress any key to quit");
			Console.ReadKey(false);
		}
	}
}
