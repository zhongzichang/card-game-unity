using UnityEngine;
using System.Collections;

public class WorldTavernController : MonoBehaviour {
	public GameObject title;
	
	void OnTap()
	{
		Debug.Log("Tavern taped.");
		SpriteRenderer render = title.GetComponent<SpriteRenderer>();
		render.sprite.name = "world_title_25";
	}
}
