using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ChapterItemObject : MonoBehaviour {

  public BetterList<StageItemObject> stages;
  /// <summary>
  /// 标题
  /// </summary>
  public UISprite title;
  /// <summary>
  /// 路径
  /// </summary>
  public UISprite path;
  /// <summary>
  /// 背景
  /// </summary>
  public UISprite background;

  private ChapterItemData data;

  // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
