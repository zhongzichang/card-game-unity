/**
 * UI Panel Node Context
 * Author: zzc
 * Date: 2014/4/3
 */

namespace TangUI
{
  public class UIPanelNodeContext
  {
    public UIPanelCache cache;
    public int depth = 0;
    public UIAnchor anchor;
    private UIPanelNode m_currentNode;

    public UIPanelNode currentNode {
      get {
        return m_currentNode;
      }

      set {
        m_currentNode = value;
      }
    }
  }
}