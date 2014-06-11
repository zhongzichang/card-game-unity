using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TangUtils;

namespace TangLevel
{
  public class EffectEjector
  {

    private static EffectEjector instance = null;

    private Dictionary<int, Type> typeMap = new Dictionary<int, Type> ();
    private Dictionary<int, MethodInfo> callbackMap = new Dictionary<int, MethodInfo> ();

    #region Properties

    public static EffectEjector Instance {
      get {
        if (instance == null) {
          instance = new EffectEjector ();
          instance.Init ();
        }
        return instance;
      }
    }

    #endregion

    #region Constructor

    private EffectEjector ()
    {
    }

    #endregion

    #region Public

    public Effect NewEffect (int id, ArrayList paramList)
    {

      if (typeMap.ContainsKey (id)) {
        Effect obj = Activator.CreateInstance (typeMap [id]) as Effect;
        obj.paramList = paramList;
        return obj;
      }
      return null;
    }

    public void Arise (Effect effect, EffectorWrapper w)
    {
      if (callbackMap.ContainsKey (effect.code)) {
        callbackMap [effect.code].Invoke (null, new object[]{ effect, w });
      }

    }

    #endregion

    #region Private

    private void Init ()
    {

      List<Type> typeList = AttributeUtils.GetTypesWith<EffectAttribute> (true);
      if (typeList != null) {
        foreach (Type type in typeList) {
          RegisterEffect (type);
        }
      }
    }

    private void RegisterEffect (Type type)
    {
      object[] objs = type.GetCustomAttributes (true);
      foreach (object obj in objs) {
        if (obj is EffectAttribute) {

          int code = ((EffectAttribute)obj).GetCode ();
          typeMap [code] = type;

          MethodInfo method = type.GetMethod ("Arise", new Type[] { typeof(Effect), typeof(EffectorWrapper) });

          if (method != null) {
            callbackMap [code] = method;
          }
        }
      }
    }

    #endregion
  }
}

