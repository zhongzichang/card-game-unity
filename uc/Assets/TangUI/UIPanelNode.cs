/**
 * UI Panel Manager
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
        gameObject.name = name;
        context.cache.Put (name, gameObject);

        DynamicBindUtil.BindScriptAndProperty (gameObject, name);

        Show ();    
      }

    }

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
        if (null != panel)
          panel.depth = context.depth;

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

    public void Remove ()
    {
      if (!(this is UIPanelRoot)) {
      
        SetActive (false);
        preNode.nextNode = null;

        context.currentNode = preNode;
        context.depth--;

        if (!(preNode is UIPanelRoot) && preNode.gameObject != null && !preNode.gameObject.activeSelf) {
          preNode.SetActive (true);
        }

      }
    }

    public void Hide ()
    {
      if (!(this is UIPanelRoot)) {
        SetActive (false);
      }
    }

  }
}