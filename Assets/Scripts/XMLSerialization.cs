using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

/**
 * Serializes external mod files into workable game files
**/
namespace XML_Worker {

	[XmlRoot("mod")]
	public class XMLExternalMod {
		[XmlAttribute] public string name;
		[XmlAttribute] public string creator;
		public Texture[] pattern;
	}

	public class Texture {
		[XmlAttribute] public string name;
		[XmlAttribute] public string pattern;
	}

	public static class XMLSerialization
	{
		private static bool debugPrint = false;
		private static List<ModPattern> mods;
		
		public static List<ModPattern> RetrieveAllPatterns()
		{
			// TODO: Put a lock here so that it can't be called while it's working. Preferably make it more asynchronous too.
			mods = new List<ModPattern>();
			string[] filesFound = IOHelper.SearchAll("xml");
			
			foreach (string file in filesFound) {
				ReadPO(file);
			}
			// Validate art file
			// Make new ModPattern
			return mods;
		}

		private static void CreatePO(string filename)
		{
			// Creates an instance of the XmlSerializer class and specifies the type of object to serialize.
			XmlSerializer serializer = new XmlSerializer(typeof(XMLExternalMod));
			TextWriter writer = new StreamWriter(filename);
			XMLExternalMod mod = new XMLExternalMod();

			// Creates a test file and additional components for verifying file integrity.
			mod.name = "Test mod structure";
			mod.creator = "Some programmer probably";

			Texture image1 = new Texture();
			image1.name = "Tex name 1";
			image1.pattern = "Tex1";

			Texture image2 = new Texture();
			image2.name = "Tex name 2";
			image2.pattern = "Tex2";

			// Inserts the item into the array.
			Texture[] images = { image1, image2 };
			mod.pattern = images;
				
			// Serializes the pattern, and closes the TextWriter.
			serializer.Serialize(writer, mod);
			writer.Close();
		}

		static void ReadPO(string filename)
		{
			// Creates an instance of the XmlSerializer class and specifies the type of object to be deserialized.
			XmlSerializer serializer = new XmlSerializer(typeof(XMLExternalMod));
			// If the XML document has been altered with unknown nodes or attributes, handles them with the
			// UnknownNode and UnknownAttribute events.
			serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
			serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

			// A FileStream is needed to read the XML document.
			FileStream fs = new FileStream(filename, FileMode.Open);
			// Declares an object variable of the type to be deserialized.
			XMLExternalMod mod;
			// Uses the Deserialize method to restore the object's state with data from the XML document.
			mod = (XMLExternalMod) serializer.Deserialize(fs);
			// Sets any null fields to default except for file
			FillBlanks(mod);
			
			// Sends for last data sweep before making class, and closes the FileStream.
			fs.Close();
			MigratePO(filename, mod);
			
			if (debugPrint) {
				Debug.Log("Called: " + mod.name);
				Debug.Log("Written by: " + mod.creator);
				
				// Reads the list of ordered items
				Texture[] images = mod.pattern;
				Debug.Log("Patterns included:");
				foreach(Texture image in images)
				{
					Debug.Log("\t" + image.name + "\t" + image.pattern);
				}
			}
		}
		
		private static void MigratePO(string filename, XMLExternalMod mod) {
			foreach (Texture image in mod.pattern) {
				string picture = SearchMultipleExtensions(IOHelper.ConvertFileToDir(filename) + "\\" + image.pattern);
				if (File.Exists(picture)) {
					ModPattern newCandidate = ScriptableObject.CreateInstance<ModPattern>();
					newCandidate.Name = mod.name;
					newCandidate.Author = mod.creator;
					newCandidate.Description = filename;
					
					if (picture.EndsWith(".tga")) {
						newCandidate.Main_Texture = TGALoader.LoadTGA(picture);
						mods.Add(newCandidate);
					} else try {
						byte[] fileData = File.ReadAllBytes(picture);
						Texture2D tex = new Texture2D(2, 2);
						tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
						newCandidate.Main_Texture = tex;
						mods.Add(newCandidate);
					} catch {
						Debug.LogError("Could not open the picture: " + picture);
					}
				}
			}
		}
		
		private static string SearchMultipleExtensions(string path) {
			string[] pictureExtensions = { ".tga", ".png", ".psd", ".tiff", ".jpg" };
			foreach (string ext in pictureExtensions) {
				if (File.Exists(path + ext))
					return (path + ext);
			}
			
			return null;
		}
		
		private static void FillBlanks(XMLExternalMod mod)
		{
			if (mod.name == null)
				mod.name = "No mod title";
			if (mod.creator == null)
				mod.creator = "No author";
				
			foreach (Texture image in mod.pattern.ToList())
			{
				if (image.name == null)
					image.name = "Unnamed Texture";
			}
		}

		private static void serializer_UnknownNode (object sender, XmlNodeEventArgs e)
		{
			Debug.LogWarning("Unknown Node:" + e.Name + "\t" + e.Text);
		}

		private static void serializer_UnknownAttribute (object sender, XmlAttributeEventArgs e)
		{
			System.Xml.XmlAttribute attr = e.Attr;
			Debug.LogWarning("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
		}
	}
}