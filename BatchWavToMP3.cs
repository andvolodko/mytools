/*
 * Created by SharpDevelop.
 * User: Andrey
 * Date: 24.07.2013
 * Time: 10:49
 * 
 * Convert all wav files in dir and subdirs to mp3. Need lame to be installed
 */
using System;
using System.IO;

namespace FilesHelper
{
	class BatchWavToMP3
	{
			
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			if(args.Length > 0) {
				ConvertFiles(args[0]);
			} else {
				Console.WriteLine("No input dir specified");
			}
				
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		private static void ConvertFiles(string path) {
			string[] filePaths = Directory.GetFiles(path, Constants.CONVERT_EXT, SearchOption.AllDirectories);
			Console.WriteLine("Files: "+filePaths.Length);
			foreach(var file in filePaths) {
				string outFile = file.Replace(".wav", ".mp3");
				//Create process
				System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
				
				//strCommand is path and file name of command to run
				pProcess.StartInfo.FileName = Constants.LAME_PATH;
				
				//strCommandParameters are parameters to pass to program
				pProcess.StartInfo.Arguments = "\""+file+"\" \""+outFile+"\" "+Constants.LAME_PARAMS;
				
				pProcess.StartInfo.UseShellExecute = false;
//				pProcess.StartInfo.CreateNoWindow = true;
				
				//Set output of program to be written to process output stream
				pProcess.StartInfo.RedirectStandardOutput = true;   
				
				//Optional
//				pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
				
				//Start the process
				pProcess.Start();
				
				//Get program output
				string strOutput = pProcess.StandardOutput.ReadToEnd();
				
				//Wait for process to finish
				pProcess.WaitForExit();
				Console.WriteLine(strOutput);
//				Console.WriteLine(file);
//				return;
			}
			
		}
		
	}
}

static class Constants
{
    public const string LAME_PATH = @"E:\_Andrey\downloads\lame3.99.5\lame.exe";
    public const string CONVERT_EXT = "*.wav";
    public const string LAME_PARAMS = "-b 80 --resample 44 -a -S";
}