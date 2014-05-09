using UnityEngine;
using System.Collections;
using TG = TangGame;
using TUI = TangUI;

namespace TangLevel
{
  public class LevelUIRoot : MonoBehaviour
  {
    public UIAnchor bottomAnchor;
    private TUI.UIPanelNodeManager bottomPanelMgr;
    public TG.LevelHeroPanel levelHeroPanel;
    // Use this for initialization
    void Start ()
    {

      if (bottomAnchor != null) {
        bottomPanelMgr = new TUI.UIPanelNodeManager (bottomAnchor, OnBottomPanelEvent);
        bottomPanelMgr.LazyOpen (UIContext.HERO_OP_PANEL, TUI.UIPanelNode.OpenMode.ADDITIVE, "test param");
      }
    }

    void OnBottomPanelEvent (object sender, TUI.PanelEventArgs args)
    {

      switch (args.EventType) {
      case TUI.EventType.OnLoad:
        // 面板加载成功

        break;
      }

    }
  }
}
