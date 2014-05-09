﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame
{
  /// 战斗界面英雄信息面板
  public class LevelHeroPanel : MonoBehaviour
  {
    public UISprite background;
    public GameObject itemGroup;
    public LevelHeroItem heroItem;
    private object mParam;
    private bool started;
    /// 对象列表
    public List<LevelHeroItem> itemList = new List<LevelHeroItem> ();

    void Start ()
    {
      heroItem.gameObject.SetActive (false);
      started = true;
      UpdateData ();
    }

    /// 面板的参数对象
    public object param {
      get{ return this.mParam; }
      set {
        this.mParam = value;
        this.UpdateData ();
      }
    }

    private void UpdateData ()
    {
      if (!started) {
        return;
      }
      if (this.mParam == null) {
        return;
      }
      LevelHeroPanelData data = this.mParam as LevelHeroPanelData;
      foreach (LevelHeroItem item in itemList) {
        GameObject.Destroy (item.gameObject);
      }
      itemList.Clear ();

      float gap = 140;
      this.background.width = (int)(40 + gap * data.heroCount);
      float startX = -(data.heroCount - 1) / 2 * gap;
      for (int i = 0; i < data.heroCount; i++) {
        GameObject go = GameObject.Instantiate (heroItem.gameObject) as GameObject;
        go.name = "HeroItem_" + i;
        go.SetActive (true);
        go.transform.parent = itemGroup.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = new Vector3 (startX, 0, 0);
        startX += gap;
        LevelHeroItem item = go.GetComponent<LevelHeroItem> ();
        itemList.Add (item);
      }
    }
  }
}