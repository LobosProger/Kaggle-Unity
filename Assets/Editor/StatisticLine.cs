using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class StatisticLine
{
	[MenuItem("Output total code lines/output")]
	private static void PrintTotalLine()
	{
		string[] fileName = Directory.GetFiles("Assets/Scripts", "*.cs", SearchOption.AllDirectories);

		int totalLine = 0;
		foreach (var temp in fileName)
		{
			int nowLine = 0;
			StreamReader sr = new StreamReader(temp);
			while (sr.ReadLine() != null)
			{
				nowLine++;
			}

			//File name + number of file lines
			//Debug.Log(String.Format("{0}——{1}", temp, nowLine));

			totalLine += nowLine;
		}

		Debug.Log(String.Format("Total code lines: {0}", totalLine));
	}
}