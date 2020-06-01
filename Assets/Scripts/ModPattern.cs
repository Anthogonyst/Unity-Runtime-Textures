using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The data structure that holds the information for third party user patterns
**/
namespace XML_Worker {
public class ModPattern : Pattern
{
	[Tooltip("Defines the name of the mod, serialized for convenience.")]
	[SerializeField] string modName = "Something interesting";
	[Tooltip("Defines the author of the mod, serialized for convenience.")]
	[SerializeField] string author = "Made by your community";
	
	public override string Name
	{
		get { return modName; }
		set { modName = value; }
	}
	
	public override string Author
	{
		get { return author; }
		set { author = value; }
	}
	
	public override bool Equals(object other) {
		if (other == null)
			return false;

		ModPattern tex = (ModPattern) other;
		bool equal = this.Main_Texture.imageContentsHash.Equals(tex.Main_Texture.imageContentsHash);
		
		if (equal) {
			Debug.Log("Two matching patterns were found by " + this.Author + " and " + tex.Author);
		}
		return equal;
	}
	
	public override int GetHashCode() {
		return material.GetHashCode();
	}
}
}