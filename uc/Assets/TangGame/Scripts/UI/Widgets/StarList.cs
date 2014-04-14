using UnityEngine;
using System.Collections;

public class StarList : UIGrid {
	public UISprite star;
	public int count;
	public int countMax;
	public bool showBackground = false;
	public string spriteN = "";
	public UISprite[] stars;

	protected override void Start ()
	{
		base.Start ();
		this.Flush ();
	}

	private void Flush(){
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
		for(int i = countMax; i > (countMax - count); i--){
			stars[i-1].GetComponent<UISprite>().spriteName = spriteName; 
		}
	}


}
