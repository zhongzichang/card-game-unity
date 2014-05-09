using System;
using UnityEngine;
using TangUI;

namespace TangLevel
{
  public class HeroOpPanelBhvr : MonoBehaviour
  {


    UIAnchor anchor;
    UIPanelNodeManager mgr;

    // Use this for initialization
    void Start ()
    {

      anchor = GetComponent<UIAnchor> ();

      if (anchor != null) {
        mgr = new UIPanelNodeManager (anchor);
        mgr.LazyOpen (UIContext.HERO_OP_PANEL, UIPanelNode.OpenMode.ADDITIVE, "test param");
      }

    }
  }
}

