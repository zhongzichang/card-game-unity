/**
 * Created by emacs
 * Date: 2013/10/16
 * Author: zzc
 */
using UnityEngine;
using System.IO;
using TangUtils;
using TangGame.Xml;
using PureMVC.Patterns;
using TangPlace;
using System.Collections.Generic;

namespace TangGame
{
  public class PreloadBhvr : MonoBehaviour
  {
    private List<string> xmls = new List<string>();
    private int remainCounter = 0;

    void Start ()
    {
      xmls.Add("hero");
      xmls.Add("level_up");
      xmls.Add("skill");
      xmls.Add("props");
      xmls.Add("evolve");
      xmls.Add("enchants_consumed");
      xmls.Add("levels");
      xmls.Add("monster");
      xmls.Add("map");
      xmls.Add("name");


      TangScene.TS.EnsureTSGobj ();
      TangNet.TN.EnsureTNGobj ();

      remainCounter = xmls.Count;

      if (xmls.Count > 0) {

        foreach (string xml in xmls) {
          TangScene.TS.LoadXml (xml, LoadCompleted, LoadFailure, LoadBegan);
        }   
      } else {
        OnPreloadCompleted();
      }

      PlaceController.Place = Place.welcome;
    }

    private void LoadCompleted (WWW www)
    {

      Debug.Log (www.url + " load completed.");
      
      string name = UrlUtil.GetFileNameWithoutExt (www.url);
      object obj = XmlProcessor.Instance.Process (name, www.text);

      Config.xmlObjTable.Add (name, obj);

      //float percent = (float)1-remainCounter/xmls.Length;
      //Facade.Instance.SendNotification(NtftNames.TG_LOADING_PROGRESS, percent);
      //Facade.Instance.SendNotification(NtftNames.TG_LOADING_MESSAGE, LoadingLang.LOAING_XML);

      if (--remainCounter == 0)
        PreloadCompleted ();

    }

    private void LoadFailure (WWW www)
    {
      Debug.Log (www.url + " load failure.");
      if (--remainCounter == 0)
        PreloadCompleted ();

    }

    private void LoadBegan (WWW www)
    {
      Debug.Log (www.url + " load began.");
    }

    private void PreloadCompleted ()
    {
      /*
      TsEffectRoot tsEffectRoot = null;
      object[] attributes = typeof(TsEffectRoot).GetCustomAttributes (typeof(XmlLateAttribute), false);
      if (attributes != null && attributes.Length > 0) {
        XmlLateAttribute attr = attributes [0] as XmlLateAttribute;
        string name = attr.GetName ();
        if (Config.xmlObjTable.ContainsKey (name)) {
          tsEffectRoot = Config.xmlObjTable [name] as TsEffectRoot;
        }
      }
      TangEffect.TE.EnsureTEGobj (tsEffectRoot.items);
      */

      OnPreloadCompleted();
    }

    private void OnPreloadCompleted(){
      Facade.Instance.SendNotification(NtftNames.TG_PRELOAD_COMPLETED);
    }

  }
}