using UnityEngine;
using System.Collections;

public class StarList : UIGrid {
	public UISprite star;
	public int count;
	public int countMax;
	public bool showBackground = false;
	public bool descending = false;
	public string spriteN = "";
	public UISprite[] stars;

	protected override void Start ()
	{
		base.Start ();
		this.Flush ();
	}

	public void Flush(){
		stars = new UISprite[countMax];
		for (int i = 0; i < countMax; i++) {
			UISprite item = NGUITools.AddChild(this.gameObject, star.gameObject).GetComponent<UISprite>();
			item.name = star.name + "_" + i;
			stars[i] = item;
			item.gameObject.SetActive(showBackground);
		}
		this.SetStarSpirteName ();
		this.repositionNow = true;
	}
	public void SetStarSpirteName(){
		this.SetStarSpirteName (spriteN);
	}

	public void SetStarSpirteName(string spriteName){
		if (descending) {
			for (int i = countMax; i > (countMax - count); i--) {
				stars [i - 1].GetComponent<UISprite> ().spriteName = spriteName; 
				stars [i - 1].gameObject.SetActive (true);
			}
		} else {
			for (int i = 0; i <  count; i++) {
				stars [i].GetComponent<UISprite> ().spriteName = spriteName; 
				stars [i].gameObject.SetActive (true);
			}
		}
	}


}
