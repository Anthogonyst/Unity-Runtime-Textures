using System.Collections.Generic;
using UnityEngine;

/**
 * The data structure that holds the information for developer maintained patterns
**/
namespace XML_Worker {

[CreateAssetMenu(menuName = "ProgrammingTest/Pattern", fileName = "NewPattern")]
public class Pattern : ScriptableObject
{
	[TextArea(3, 6)]
	[SerializeField] string description = "Write comments for developers here: this is a convenient way to serialize textures.";
	[Tooltip("Defines the name of the texture.")]
	[SerializeField] string patternName = "New Pattern";
	[Tooltip("Defines the texture map for the albedo color.")]
	[SerializeField] public Texture2D material;
	// Optionally, we may serialize texture names if we desire to credit multiple artists or individual works

	public virtual string Description
	{
		get { return description; }
		set { description = value; }
	}

	public virtual Texture2D Main_Texture
	{
		get { return material; }
		set { material = value; }
	}
	
	public virtual string Name
	{
		get { return patternName; }
		set { patternName = value; }
	}
	
	public virtual string Author
	{
		get { return "Ruffleneck"; }
		set { ; }
	}
	
	public override bool Equals(object other) {
		if (other == null)
			return false;

		Pattern tex = (Pattern) other;
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
