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
    // 初始深度设置为 -2 ，0 保留给场景对象使用
    public int depth = -2;
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