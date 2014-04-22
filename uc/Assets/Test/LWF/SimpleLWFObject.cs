using UnityEngine;

public class SimpleLWFObject : LWFObject {
	
	void Start()
	{
		lwfName = "animated_building.lwfdata/animated_building";
		Load(lwfName, "animated_building.lwfdata/");
	}
	
}