using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Holds the list of Patterns for usage as needed
**/
namespace XML_Worker {
public class PatternController : ScriptableObject
{
	[SerializeField] public static List<Pattern> patterns;
	[SerializeField] private static bool loadModsOnStartup = false;
	
	private static void Initialize() {
		patterns = new List<Pattern>();
		
		Pattern[] patternsArray = Resources.LoadAll<Pattern>("PatternAssets");
		foreach (Pattern p in patternsArray)
			patterns.Add(p);
		
		if (loadModsOnStartup)
			LoadMods();
	}
	
	public static void LoadMods() {
		if (patterns == null)
			Initialize();
		
		List<ModPattern> importedPatterns = XMLSerialization.RetrieveAllPatterns();
		
		foreach (ModPattern p in importedPatterns) {
			if (!patterns.Contains(p))
				patterns.Add(p);
		}
	}
	
	public static List<Pattern> RetrievePatterns() {
		if (patterns == null)
			Initialize();
		
		return patterns;
	}
}
}