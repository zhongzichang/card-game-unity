/**
 * UI Panel Node
 * Author: zzc
 * Date: 2014/4/3
 */
using UnityEngine;
using System;
using System.Reflection;
using TS = TangScene;

namespace TangUI
{
  public class UIPanelNode
  {
    public enum OpenMode
    {
      ADDITIVE,
      OVERRIDE,
      REPLACE
    }

    private UIPanelNodeContext m_context;
    /// <summary>
    ///   Node Name
    /// </summary>
    public string name;
    /// <summary>
    ///   Previous Node
    /// </summary>
    public UIPanelNode preNode;
    /// <summary>
    ///   Next Node
    /// </summary>
    public UIPanelNode nextNode;
    /// <summary>
    ///   GameObject
    /// </summary>
    public GameObject gameObject;
    /// <summary>
    ///   Replace
    /// </summary>
    //public bool replace = false;

    /// <summary>
    ///   Open Mode
    /// </summary>
    public OpenMode openMode;
    /// <summary>
    ///   Body
    /// </summary>
    public object param;

    /// <summary>
    ///   Context
    /// </summary>
    public UIPanelNodeContext context {
      get {
        return m_context;
      }
      set {
        this.m_context = value;
        this.preNode = m_context.currentNode;
      }
    }

    public UIPanelNode ()
    {
      
    }

    public UIPanelNode (string name)
    {
      this.name = name;
    }

    /// <summary>
    ///   Launch
    /// </summary>
    public void Launch (OpenMode openMode, object param)
    {

      this.openMode = openMode;
      this.param = param;

      gameObject = context.cache.GetInactiveGobj (name);
      if (null == gameObject)
        StartLoadRes ();
      else
        Show ();

    }

    public void SetActive (bool active)
    {
      NGUITools.SetActive (gameObject, active);
    }

    private void StartLoadRes ()
    {
      if (!Config.use_packed_res)
        LoadResComplete (null);
      else {
        string path = Tang.ResourceUtils.GetAbFilePath (name);
        TS.TS.LoadAssetBundle (path, LoadResComplete);      
      }
    }

    private void LoadResComplete (WWW www)
    { 
      UnityEngine.Object assets = null;
      if (www == null)
        assets = Resources.Load (Config.PANEL_PATH + Tang.Config.DIR_SEP + name);
      else
        assets = www.assetBundle.Load (name);
      gameObject = GameObject.Instantiate (assets) as GameObject;

      if (gameObject != null) {
        gameObject.SetActive (false);
        gameObject.name = name;
        context.cache.Put (name, gameObject);

        DynamicBindUtil.BindScriptAndProperty (gameObject, name);

        Show ();    
      }

    }

    /// <summary>
    /// 显示 Panel
    /// </summary>
    private void Show ()
    {

      // hide previous node
      if (openMode == OpenMode.REPLACE && !(preNode is UIPanelRoot)) {
        preNode.Remove ();
      } else if (openMode == OpenMode.OVERRIDE) {
        preNode.Hide ();
      }

      // init current node
      preNode = context.currentNode;
      if (preNode.nextNode == null) {

        preNode.nextNode = this;
        context.currentNode = this;
        context.depth++;

        Transform transform = gameObject.transform;
        transform.parent = context.anchor.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        UIPanel panel = gameObject.GetComponent<UIPanel> ();
        if (null != panel) {

          // 调整基本面板
          int baseDepth = context.depth * 1000;
          FixPanelRenderQ (panel, baseDepth);
          panel.depth = baseDepth;

          // 调整子面板
          UIPanel[] childPanels = gameObject.GetComponentsInChildren<UIPanel> (true);
          foreach (UIPanel cp in childPanels) {
            if (cp != panel) {
              int cdepth = baseDepth + cp.depth;
              FixPanelRenderQ (cp, cdepth);
              NGUITools.AdjustDepth (cp.gameObject, baseDepth);
            }
          }
        }

        // assign param
        MonoBehaviour script = gameObject.GetComponent (name) as MonoBehaviour;
        if (script != null) {
          FieldInfo fieldInfo = script.GetType ().GetField ("param");
          if (fieldInfo != null) {
            fieldInfo.SetValue (script, param);
          }
        }
    

        if (!gameObject.activeSelf)
          SetActive (true);

      } else {
        throw new Exception ("Can not attach to previous node.");
      }
    }

    private void FixPanelRenderQ (UIPanel panel, int renderQueue)
    {

      if (panel.renderQueue == UIPanel.RenderQueue.Automatic) {
        panel.renderQueue = UIPanel.RenderQueue.StartAt;
      }
      panel.startingRenderQueue = renderQueue;
    }

    /// <summary>
    /// 删除该 Panel
    /// </summary>
    public void Remove ()
    {
      if (!(this is UIPanelRoot)) {

        UIPanel panel = gameObject.GetComponent<UIPanel> ();
        if (null != panel) {
          int baseDepth = context.depth * 1000;

          // 恢复子面板的 depth
          UIPanel[] childPanels = gameObject.GetComponentsInChildren<UIPanel> (true);
          foreach (UIPanel cp in childPanels) {
            if (cp != panel) {
              NGUITools.AdjustDepth (cp.gameObject, -baseDepth);
            }
          }
        }
        //NGUITools.AdjustDepth (gameObject, -context.depth * 1000);
        SetActive (false);

        preNode.nextNode = null;
        context.currentNode = preNode;
        context.depth--;

        if (!(preNode is UIPanelRoot) && preNode.gameObject != null && !preNode.gameObject.activeSelf) {
          preNode.SetActive (true);
        }

      }
    }

    /// <summary>
    /// 隐藏该 Panel
    /// </summary>
    public void Hide ()
    {
      if (!(this is UIPanelRoot)) {
        SetActive (false);
      }
    }
  }
}