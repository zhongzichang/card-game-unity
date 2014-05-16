/**
 * UI Panel Node
 * Author: zzc
 * Date: 2014/4/3
 */
using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
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

    public enum BlockMode
    {
      /// <summary>
      /// The NON.
      /// 没有背景
      /// </summary>
      NONE,
      /// <summary>
      /// The SPRIT.
      /// 黑色背景
      /// </summary>
      SPRITE,
      /// <summary>
      /// The TEXTUR.
      /// 图片背景
      /// </summary>
      TEXTURE
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
    /// The block mode.
    /// </summary>
    public BlockMode blockMode;
    /// <summary>
    ///   Body
    /// </summary>
    public object param;
    /// <summary>
    /// The is base template.
    /// </summary>
    public bool isBaseTemplate;
    /// <summary>
    /// The panel event handler.
    /// </summary>
    public PanelEventHandler raisePanelEvent;

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
    public void Launch (OpenMode openMode, BlockMode blockMode, object param, 
                        bool isBaseTemplate = false, PanelEventHandler handler = null)
    {
      this.blockMode = blockMode;
      this.openMode = openMode;
      this.param = param;
      this.isBaseTemplate = isBaseTemplate;
      this.raisePanelEvent = handler;

      if (isBaseTemplate) { // 使用模版

        if (context.cache.assetTable.ContainsKey (name)) {
          UnityEngine.Object asset = context.cache.assetTable [name];
          GameObject g = Create (asset);
          if (g != null) {
            Show ();
          }
        } else {
          StartLoadRes ();
        }

      } else {

        gameObject = context.cache.GetInactiveGobj (name);
        if (null == gameObject) {
          if (context.cache.assetTable.ContainsKey (name)) {
            GameObject g = Create (context.cache.assetTable [name]);
            if (g != null) {
              Show ();
            }
          } else {
            StartLoadRes ();
          }
        } else {
          Show ();
        }
      }

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
      UnityEngine.Object asset = null;
      if (www == null) {
        asset = Resources.Load (Config.PANEL_PATH + Tang.Config.DIR_SEP + name);
      } else {
        asset = www.assetBundle.Load (name);
      }

      if (asset != null) {
        context.cache.assetTable.Add (asset.name, asset);
        GameObject g = Create (asset);
        if (g != null) {
          Show ();
        }
      }
    }

    private GameObject Create (UnityEngine.Object asset)
    {

      gameObject = GameObject.Instantiate (asset) as GameObject;
      if (gameObject != null) {
        gameObject.SetActive (false);
        gameObject.name = name;
        context.cache.Put (name, gameObject);
        DynamicBindUtil.BindScriptAndProperty (gameObject, name);

        if (this.blockMode == BlockMode.SPRITE) {
          UnityEngine.Object obj = Resources.Load (
                                     TangGame.UIContext.getWidgetsPath (TangGame.UIContext.PANEL_BLOCK));
          NGUITools.AddChild (gameObject, obj as GameObject);
        }
        if (this.blockMode == BlockMode.TEXTURE) {
          GameObject obj = Resources.Load (
                             TangGame.UIContext.getWidgetsPath (TangGame.UIContext.PANEL_BLOCK)) as GameObject;
					Block block = NGUITools.AddChild (gameObject, obj as GameObject).GetComponent<Block> ();
					if (block != null) {
						block.texture.SetActive (true);
					}
        }
      }
      return gameObject;
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
          Hashtable ht = new Hashtable ();
          foreach (UIPanel cp in childPanels) {
            ht.Add (cp, cp.depth);
          }
          UIPanel[] keyArray = new UIPanel[ht.Count]; 
          int[] valueArray = new int[ht.Count]; 
          ht.Keys.CopyTo (keyArray, 0);  
          ht.Values.CopyTo (valueArray, 0);
          Array.Sort (valueArray, keyArray);
          int index = 1;
          foreach (UIPanel cp in keyArray) {
            if (cp != panel) {
              int cdepth = baseDepth + 100 * index++;
              FixPanelRenderQ (cp, cdepth);
              cp.depth = cdepth;
            }
          }
        }

        // assign param
        MonoBehaviour script = gameObject.GetComponent (name) as MonoBehaviour;
        if (script != null) {
          FieldInfo fieldInfo = script.GetType ().GetField ("param");
          if (fieldInfo != null) {
            fieldInfo.SetValue (script, param);
          } else {
            PropertyInfo propertyInfo = script.GetType ().GetProperty ("param");
            if (propertyInfo != null) {
              propertyInfo.SetValue (script, param, null);
            }
          }
        }
    

        if (!gameObject.activeSelf)
          SetActive (true);

        // 发出加载完成通知
        if (raisePanelEvent != null) {
          raisePanelEvent (this, new PanelEventArgs (EventType.OnLoad));
        }

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

        if (isBaseTemplate) {
          NGUITools.Destroy (gameObject);
        } else {
          SetActive (false);
        }

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