using System;
using System.IO;
using UnityEngine;

/**
 * Helper to find the correct working directory
**/
namespace XML_Worker {
public class IOHelper
{
	public static DirectoryInfo CreateFolder(string path)
	{
		if(!Directory.Exists(path))
		{
			return Directory.CreateDirectory(path);
		}
		return null;
	}
	
	public static string GetOrCreateFolder() {
		return GetOrCreateFolder(null);
	}
	
	public static string GetOrCreateFolder(string folderName)
	{
		string path;
		if (folderName != null)
			path = Path.Combine(Application.persistentDataPath, folderName);
		else path = Application.persistentDataPath;
		CreateFolder(path);
		return path;
	}
	
	public static string[] SearchAll(string extension) {
		string[] files = Directory.GetFiles(GetOrCreateFolder(null), "*." + extension, SearchOption.AllDirectories);
		return files;
	}
	
	public static string ConvertFileToDir(string fileLocation) {
		return Path.GetDirectoryName(fileLocation);
	}
}
}