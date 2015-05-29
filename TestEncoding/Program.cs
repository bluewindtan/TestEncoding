using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestEncoding
{
	class Program
	{
		static void Main(string[] args)
		{
			System.Diagnostics.Process proUpdate = new System.Diagnostics.Process();
			proUpdate.StartInfo.FileName = "cmd.exe";
			proUpdate.StartInfo.UseShellExecute = false;
			proUpdate.StartInfo.RedirectStandardInput = true;
			proUpdate.Start();

			string strLogFile = "OutputEncodings.log";

			DateTime dtStart = DateTime.Now;
			string strOutput = "ECHO Print all the encodings: > " + strLogFile;
			proUpdate.StandardInput.WriteLine(strOutput);

			// Print the header.
			strOutput = "ECHO Name               "
				+ "CodePage  "
				+ "BodyName           " 
				+ "HeaderName         "
				+ "WebName            "
				+ "Encoding.EncodingName";
			strOutput += " >> " + strLogFile;
			proUpdate.StandardInput.WriteLine(strOutput);

			// For every encoding, compare the name properties with EncodingInfo.Name.
			// Display only the encodings that have one or more different names.
			foreach (EncodingInfo ei in Encoding.GetEncodings())
			{
				Encoding e = ei.GetEncoding();
				if (ei != null)
				{
					strOutput = "ECHO ";
					string strName = String.Format("{0,-18} ", ei.Name);
					strOutput += strName;
					string strCodePage = String.Format("{0,-9} ", e.CodePage);
					strOutput += strCodePage;
					string strBodyName = String.Format("{0,-18} ", e.BodyName);
					strOutput += strBodyName;
					string strHeaderName = String.Format("{0,-18} ", e.HeaderName);
					strOutput += strHeaderName;
					string strWebName = String.Format("{0,-18} ", e.WebName);
					strOutput += strWebName;
					string strEncodingName = String.Format("{0,-18} ", e.EncodingName);
					strOutput += strEncodingName;

					strOutput += " >> " + strLogFile;
					proUpdate.StandardInput.WriteLine(strOutput);
				}
			}

			proUpdate.StandardInput.WriteLine("exit");
			proUpdate.WaitForExit();
			proUpdate.Close();

		}
	}
}
