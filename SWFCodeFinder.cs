/*
 * Created by SharpDevelop.
 * User: Andrey
 * Date: 24.07.2013
 * Time: 10:49
 * 
 * Find swf with actionscript in all files in dir and subdirs. Need SwiX to be installed
 */
using System;
using System.IO;

namespace FilesHelper
{
	class SWFCodeFinder
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
				string outFile = file.Replace(".swf", ".xml");
				//Create process
				System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
				
				//strCommand is path and file name of command to run
				pProcess.StartInfo.FileName = Constants.SWIX_PATH;
				
				//strCommandParameters are parameters to pass to program
				pProcess.StartInfo.Arguments = "swf2xml \""+file+"\" \""+outFile;
				
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
				
				// Read the file and display it line by line.
				System.IO.StreamReader fileXML = new System.IO.StreamReader(outFile);
				int counter = 0;
				string line;
				while((line = fileXML.ReadLine()) != null)
				{
				    if ( line.Contains(Constants.CODE_SIGNATURE) )
				    {
				        Console.WriteLine (outFile + " !!! FIND CODE !!! \n");
				        break;
				    }
				
				   counter++;
				}
				
				fileXML.Close();
				
				//Remove
				System.IO.File.Delete(outFile);
				
//				Console.WriteLine(file);
//				return;
			}
			
		}
		
	}
}

static class Constants
{
    public const string SWIX_PATH = @"c:\Program Files (x86)\SWiX\SwiXConsole.exe";
    public const string CONVERT_EXT = "*.swf";
    public const string CODE_SIGNATURE = "<DoABC";
}