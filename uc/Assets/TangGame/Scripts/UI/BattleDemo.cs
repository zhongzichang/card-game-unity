using UnityEngine;
using System.Collections;

public class BattleDemo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TangGame.UIContext.manger.LazyOpen("LevelControllPanel", TangUI.UIPanelNode.OpenMode.ADDITIVE, TangUI.UIPanelNode.BlockMode.NONE, null);
	}
	

}
