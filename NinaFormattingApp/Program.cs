using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NinaFormattingApp
{
	class Program
	{
		// static string inputFilename = "testdata.csv";
		// static string outputFilename = "testdata_output.csv";

		static string inputFilename = "nina_source_data.csv";
		static string outputFilename = "nina_data_output.csv";
		// static string outputFilename = "nina_data_output_no_quotes.csv";

		static string sourceText;

		static void Main(string[] args)
		{
			Console.WriteLine("\nNINAS FORMATTING APP");
			Console.WriteLine("------------------------------------------------------------------------------------------------\n");

			sourceText = File.ReadAllText(inputFilename);

			string resultText = RegexProcessor.Process(sourceText);
			Console.Write(resultText);

			File.WriteAllText(outputFilename, resultText, Encoding.UTF8);

			Console.WriteLine("\nPress any key to quit");
			Console.ReadKey(false);
		}
	}


	public static class RegexProcessor
	{
		static Regex regex = new Regex(@"^""([^""]*)\s([\w-_\.]+)"",\s""""(.*)$", RegexOptions.Multiline);

		public static string Process(string sourceText)
		{
			string resultText;

			resultText = regex.Replace(sourceText, @"""$1"", ""$2""$3");

			resultText = Regex.Replace(resultText, @"^(""[^""]*"", )(""[^""]*"")", (Match match) => {
				string result = match.ToString();
				result = Regex.Replace(result, @"\b\w", (Match innerMatch) => innerMatch.ToString().ToUpper());
				// result = Regex.Replace(result, @"\B\w", (Match innerMatch) => innerMatch.ToString().ToLower());
				return result;
			}, RegexOptions.Multiline);

			// resultText = resultText.Replace("\"", "");

			return resultText;
		}
	}
}
