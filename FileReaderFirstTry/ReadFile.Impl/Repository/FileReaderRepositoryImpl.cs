using ReadFile.Domain.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReadFile.Impl.Repository
{
	public class FileReaderRepositoryImpl : IFileReaderRepository
	{
		public void GetOccurrences(string filePath, out string outputFilePath, out bool done)
		{
			SortedDictionary<string, int> occurences = ReadFile(filePath);
			outputFilePath = WriteOutputFile(occurences);

			done = true;
		}

		private static string WriteOutputFile(SortedDictionary<string, int> occurences)
		{
			string outputFilePath = Directory.GetCurrentDirectory() + "/output.txt";

			if (File.Exists(outputFilePath))
				File.Delete(outputFilePath);

			using (TextWriter writer = new StreamWriter(outputFilePath, true))
			{
				writer.WriteLine("Word			occurrence");
				foreach (KeyValuePair<string, int> occurrence in occurences.OrderByDescending(p => p.Value))
					writer.WriteLine(String.Format("{0}			{1}", occurrence.Key, occurrence.Value));
			}

			return outputFilePath;
		}

		private static SortedDictionary<string, int> ReadFile(string filePath)
		{
			string line;
			var file = new StreamReader(filePath);
			var occurences = new SortedDictionary<string, int>();

			while ((line = file.ReadLine()) != null)
			{
				string[] words = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

				for (int i = 0; i < words.Length; i++)
				{
					string wordToAppend = Regex.Replace(words[i], "[^0-9a-zA-Z]+", "").Trim();

					if (string.IsNullOrEmpty(wordToAppend))
						continue;

					if (occurences.ContainsKey(wordToAppend))
					{
						int old = occurences[wordToAppend];
						occurences[wordToAppend] = ++old;
					}
					else
					{
						occurences.Add(wordToAppend, 1);
					}
				}
			}

			return occurences;
		}
	}
}
