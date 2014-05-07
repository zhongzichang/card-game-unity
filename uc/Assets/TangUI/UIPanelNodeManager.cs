/**
 * UI Panel Node Manager
 * Author: zzc
 * Date: 2014/4/3
 */
using UnityEngine;

namespace TangUI
{
  public class UIPanelNodeManager
  {
    private UIPanelNodeContext context;

    public UIPanelNodeManager (UIAnchor anchor)
    {

      UIPanelNode root = new UIPanelRoot ();
      root.gameObject = anchor.gameObject;

      this.context = new UIPanelNodeContext ();
      this.context.currentNode = root;
      this.context.cache = new UIPanelCache ();
      this.context.anchor = anchor;
    }

    public void LazyOpen (string name)
    {
      LazyOpen (name, UIPanelNode.OpenMode.ADDITIVE, UIPanelNode.BlockMode.SPRITE, null);
    }

    public void LazyOpen (string name, UIPanelNode.OpenMode openMode)
    {
      LazyOpen (name, openMode, UIPanelNode.BlockMode.NONE, null);
    }

    public void LazyOpen (string name, UIPanelNode.OpenMode openMode, UIPanelNode.BlockMode blockMode)
    {
      LazyOpen (name, openMode, blockMode, null);
    }

    public void LazyOpen (string name, UIPanelNode.OpenMode openMode, object param)
    {
      LazyOpen (name, openMode, UIPanelNode.BlockMode.SPRITE, param);
    }

    public void LazyOpen (string name, UIPanelNode.OpenMode openMode, UIPanelNode.BlockMode blockMode,
                          object param, bool isBaseTemplate = false)
    {
      if (!name.Equals (context.currentNode.name)) {
        UIPanelNode node = new UIPanelNode (name);
        node.context = context;
        UIPanelNodeContext.mgrUseStack.Push (this);
        node.Launch (openMode, blockMode, param, isBaseTemplate);
      }
      
    }

    public void Back ()
    {
      if (!(context.currentNode is UIPanelRoot)) {
        context.currentNode.Remove ();

    
      }
    }
  }
}
