using ReadFile.Domain.Repository;
using ReadFile.Impl.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace ReadFileFirstTry
{
	class Program
	{
		static void Main(string[] args)
		{
			bool done = false;
			string filePath = @"C:\Users\PAULO\source\repos\ReadFileFirstTry\ReadFileFirstTry\bigSample.txt";
			string outputFilePath = string.Empty;
			IFileReaderRepository fileReader = new FileReaderRepositoryImpl();

			var read = new Thread(() => fileReader.GetOccurrences(filePath, out outputFilePath, out done));
			read.Start();

			int i = 0;
			do
			{
				i++;
				Thread.Sleep(1000);
				Console.WriteLine(string.Format("Loading time: {0} seconds", i));
			} while (!done); ;

			Console.WriteLine(outputFilePath);
			Console.Read();
		}

		//If you have an UI you can use this to kill loading process
		private static void KillThread(Thread read, out bool done)
		{
			read.Abort();
			done = true;
		}
	}
}
