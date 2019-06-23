using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NinaFormattingApp
{
	// App for formatting Nina's csv-formatted email subscription list.
	// Converts the fullname field to a firstname and a lastname field.
	// See 'testdata_expected_output.csv' for examples.

	class Program
	{
		static string inputFilename = "testdata.csv";
		static string outputFilename = "testdata_output.csv";

		// static string inputFilename = "nina_source_data.csv";
		// static string outputFilename = "nina_data_output.csv";

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
		// Both regexes do only match entries with a non-empty firstname field and an empty lastname field
		// Entries that have both a firstname and a lastname and entries that only have a lastname are left untouched.
		// Though, all entries are subject to character casing conversion.

		// Regex 1: "Peter Birger Olsen", "" -> "Peter Birger", "Olsen"
		// All words sre placed in 'firstname' ($1) except last word which is placed in 'lastname' ($2).
		// @"^""([^""]*)\s([\w-_\.]+)"",\s""""(.*)$", @"""$1"", ""$2""$3"

		// Regex 2: "Peter Birger Olsen", "" -> "Peter", "Birger Olsen"
		// First word in 'firstname' ($1), remaining words in 'lastname' ($2).
		static Regex regex = new Regex(@"^""([\w-_\.]+)\s([^""]*)"",\s""""(.*)$", RegexOptions.Multiline);

		public static string Process(string sourceText)
		{
			string resultText;

			// 1) Fullname to firstname/lastname conversion
			resultText = regex.Replace(sourceText, @"""$1"", ""$2""$3");

			// 2) Character casing
			// Match the firstname field ($1) and the lastname field ($2) for character casing
			resultText = Regex.Replace(resultText, @"^(""[^""]*"", )(""[^""]*"")", (Match match) => {

				string result = match.ToString();

				// Uppercase first character
				result = Regex.Replace(result, @"\b\w", (Match innerMatch) => innerMatch.ToString().ToUpper());

				// Lowercase remaining characters
				result = Regex.Replace(result, @"\B\w", (Match innerMatch) => innerMatch.ToString().ToLower());

				return result;

			}, RegexOptions.Multiline);

			// 3) Remove double quotes
			// resultText = resultText.Replace("\"", "");

			return resultText;
		}
	}
}
