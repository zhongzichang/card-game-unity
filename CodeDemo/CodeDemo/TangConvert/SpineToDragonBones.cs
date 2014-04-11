using System.Collections.Generic;
using System;

namespace TangConvert
{
  public class SpineToDragonBones
  {
    /*
    const string BONE = "bones";
    const string SLOT = "slots";
    const string SKIN = "skins";
    const string ANIMATION = "animations";
    const string ROTATE = "rotate";
    const string TRANSLATE = "translate";
    const string SCALE = "scale";
    const string COMBINE = "combine";
    const string ATTACHMENT = "attachment";
    const string ADDITIVE = "additive";
    const string COLOR = "color";
    const string A_NAME = "name";
    const string A_PARENT = "parent";
    const string A_BONE = "bone";
    const string A_LENGTH = "length";
    const string A_TIME = "time";
    const string A_CURVE = "curve";
    const string A_DURATION = "duration";
    const string A_X = "x";
    const string A_Y = "y";
    const string A_ROTATION = "rotation";
    const string A_ANGLE = "angle";
    const string A_SCALE_X = "scaleX";
    const string A_SCALE_Y = "scaleY";
    const string A_WIDTH = "width";
    const string A_HEIGHT = "height";
    const string A_INHERIT_SCALE = "inheritScale";
    const string A_INHERIT_ROTATION = "inheritRotation";
    const string V_STEPPED = "stepped";
    */
    const double ANGLE_TO_RADIAN = Math.PI / 180;
    const int BEZIER_SEGMENTS = 10;
    /*
    function formatArmature(armatureObject:Object, armatureName:String, textureAtlasXML:XML, frameRate:uint):XML
    {
      var armatureXML:XML = 
        <{ConstValues.ARMATURE}
      {ConstValues.A_NAME}={armatureName}
      />;

      var boneList:Array = armatureObject[BONE];
      //对动画进行坐标变换时，需要保留bone的local坐标到boneListCopy中
      var boneListCopy:Array = tansformBoneList(boneList);

      for each(var boneObject:Object in boneList)
      {
        armatureXML.appendChild(formatBone(boneObject));
      }

      var slotList:Array = armatureObject[SLOT];

      var skins:Object = armatureObject[SKIN];
      var skinXML:XML;
      for(var skinName:String in skins)
      {
        skinXML = formatSkin(skins[skinName], skinName, slotList, textureAtlasXML);
        armatureXML.appendChild(skinXML);
      }

      var animations:Object = armatureObject[ANIMATION];
      var animationObject:Object;
      for(var animationName:String in animations)
      {
        animationObject = animations[animationName];
        //对动画进行坐标变化
        //boneListCopy提供遍历骨骼树由根到叶的顺序
        //slotList提供bone和slot的映射
        //skinXML提供slot和display的映射
        transformAnimation(animationObject, boneListCopy, slotList, frameRate, skinXML);
        armatureXML.appendChild(formatAnimation(animationObject, animationName));
      }

      return armatureXML;
    }*/
    public class TextureAtlas
    {
    }

    public DbArmature FormatArmature (SpineSkeleton armatureObject, string armatureName, TextureAtlas atlas, uint frameRate)
    {
      DbArmature armature = new DbArmature ();
      List<SpineBone> boneList = armatureObject.bones;
      List<SpineBone> boneListCopy = TransformBoneList (boneList);
      foreach (SpineBone boneObject in boneList) {
        armature.bone.Add (FormatBone (boneObject));
      }

      return armature;
    }
    /*
     * function formatBone(boneObject:Object):XML
{
  var boneXML:XML = 
    <{ConstValues.BONE}
      {ConstValues.A_NAME}={boneObject[A_NAME]}
      {ConstValues.A_LENGTH}={boneObject[A_LENGTH]}
    >
      <{ConstValues.TRANSFORM}
        {ConstValues.A_X}={formatNumber(boneObject[A_X])}
        {ConstValues.A_Y}={formatNumber(boneObject[A_Y])}
        {ConstValues.A_SKEW_X}={formatNumber(boneObject[A_ROTATION])}
        {ConstValues.A_SKEW_Y}={formatNumber(boneObject[A_ROTATION])}
        {ConstValues.A_SCALE_X}={formatNumber(boneObject[A_SCALE_X])}
        {ConstValues.A_SCALE_Y}={formatNumber(boneObject[A_SCALE_Y])}
      />
    </{ConstValues.BONE}>;

  var inheritRatation:String = boneObject[A_INHERIT_ROTATION] as String;
  switch (inheritRatation)
  {
    case "0":
    case "false":
    case "no":
      boneXML.@[ConstValues.A_FIXED_ROTATION] = true;
      break;
    default:
      boneXML.@[ConstValues.A_FIXED_ROTATION] = false;
      break;
  }

  var inheritScale:String = boneObject[A_INHERIT_SCALE] as String;
  if (inheritScale)
  {
    switch (inheritScale)
    {
      case "1":
      case "true":
      case "yes":
        boneXML.@[ConstValues.A_SCALE_MODE] = 2;
        break;
      default:
        boneXML.@[ConstValues.A_SCALE_MODE] = 0;
        break;
    }
  }
  else
  {
    boneXML.@[ConstValues.A_SCALE_MODE] = 2;
  }

  var parent:String = boneObject[A_PARENT];
  if(parent)
  {
    boneXML.@[ConstValues.A_PARENT] = parent;
  }

  return boneXML;
}
    */
    public DbBone FormatBone (SpineBone spineBone)
    {
      DbBone dbBone = new DbBone ();

      dbBone.fixedRotation = !spineBone.inheritRotation;

      dbBone.scaleMode = spineBone.inheritScale ? 2 : 0;

      if (spineBone.parent != null) {
        dbBone.parent = spineBone.parent;
      }


      return dbBone;
    }
    /*
    function tansformBoneList(boneList:Array):Array
    {
      //sort
      if(boneList.length == 0)
      {
        return null;
      }

      var listCopy:Array = [];
      _helpArray.length = 0;
      var i:int = boneList.length;
      while(i --)
      {
        var boneObject:Object = boneList[i];
        var level:int = 0;
        var parentObject:Object = boneObject;
        while(parentObject && parentObject.parent)
        {
          level ++;
          parentObject = getBoneFromList(boneList, parentObject[A_PARENT]);
        }
        _helpArray[i] = {level:level, bone:boneObject};
      }

      _helpArray.sortOn("level", Array.NUMERIC);

      i = _helpArray.length;
      while(i --)
      {
        boneList[i] = _helpArray[i].bone;
      }
      _helpArray.length = 0;

      //transform
      var parentName;
      var parentRadian:Number;
      var boneCopy:Object;
      for each(boneObject in boneList)
      {
        formatTransform(boneObject);
        boneCopy = {};
        boneCopy[A_NAME] = boneObject[A_NAME];
        boneCopy[A_PARENT] = boneObject[A_PARENT];
        boneCopy[A_X] = boneObject[A_X];
        boneCopy[A_Y] = boneObject[A_Y];
        boneCopy[A_ROTATION] = boneObject[A_ROTATION];
        boneCopy[A_SCALE_X] = boneObject[A_SCALE_X];
        boneCopy[A_SCALE_Y] = boneObject[A_SCALE_Y];
        listCopy.push(boneCopy);
        parentName = boneObject[A_PARENT];
        if(parentName)
        {
          parentObject = getBoneFromList(boneList, parentName);
          transformToGlobal(boneObject, parentObject);
        }
      }

      return listCopy;
    } 
    */
    public List<SpineBone> TransformBoneList (List<SpineBone> boneList)
    {
      if (boneList.Count == 0) {
        return null;
      }

      List<SpineBone> listCopy = new List<SpineBone> ();
      SortedDictionary<int, SpineBone> helpTable = new SortedDictionary<int, SpineBone> ();

      int i = boneList.Count;
      while (i-- > 0) {
        SpineBone boneObject = boneList [i];
        int level = 0;
        SpineBone parentObject = boneObject;
        while (parentObject != null && parentObject.parent != null) {
          level++;
          parentObject = GetBoneFromList (boneList, parentObject.parent);
        }
        helpTable [level] = boneObject;
      }

      i = helpTable.Count;
      foreach (KeyValuePair<int, SpineBone> kvp in helpTable) {
        boneList [i--] = kvp.Value;
      }

      // transform
      string parentName;
      SpineBone boneCopy;
      foreach (SpineBone boneObject in boneList) {
        FormatTransform (boneObject);
        boneCopy = new SpineBone ();
        boneCopy.name = boneObject.name;
        boneCopy.parent = boneObject.parent;
        boneCopy.x = boneObject.x;
        boneCopy.y = boneObject.y;
        boneCopy.rotation = boneObject.rotation;
        boneCopy.scaleX = boneObject.scaleX;
        boneCopy.scaleY = boneObject.scaleY;
        listCopy.Add (boneCopy);
        parentName = boneObject.parent;
        if (parentName != null) {
          TransformToGlobal (boneObject, GetBoneFromList (boneList, parentName));
        }
      }

      return listCopy;

    }
    /*
    function getBoneFromList(boneList:Array, boneName):Object
    {
      var bone:Object;
      var i:int = boneList.length;
      while(i --)
      {
        bone = boneList[i];
        if(bone[A_NAME] == boneName)
        {
          return bone;
        }
      }

      return null;
    }*/
    public SpineBone GetBoneFromList (List<SpineBone> boneList, string boneName)
    {
      SpineBone bone;
      int i = boneList.Count;
      while (i-- > 0) {
        bone = boneList [i];
        if (bone.name == boneName)
          return bone;

      }

      return null;
    }
    /*
    function formatTransform(transformObject:Object, defaultScale:Number = 1):void
    {
      transformObject[A_X] = Number(transformObject[A_X]) || 0;
      transformObject[A_Y] = -Number(transformObject[A_Y]) || 0;
      transformObject[A_ROTATION] = formatRotation(-Number(transformObject[A_ROTATION]) || -Number(transformObject[A_ANGLE])) || 0;
      transformObject[A_SCALE_X] = Number(transformObject[A_SCALE_X]) || defaultScale;
      transformObject[A_SCALE_Y] = Number(transformObject[A_SCALE_Y]) || defaultScale;
    }*/
    public void FormatTransform (SpineBone bone, double scale = 1)
    {
      bone.y = -bone.y;
      bone.rotation = FormatRotation (-bone.rotation);
      bone.scaleX = bone.scaleX == 0 ? 1 : bone.scaleX;
      bone.scaleY = bone.scaleY == 0 ? 1 : bone.scaleY;
    }
    /*
    function getFrameCurvePercent(frameObject:Object, percent:Number):Number
    {
      var curve:* = frameObject[A_CURVE];
      if(!curve)  
      {
        return percent;
      }
      else if(curve == V_STEPPED)
      {
        return 0;
      }
      else if(curve is Array)
      {

      }
      else
      {
        return percent;
      }

      var dfx:Number = curve[0];
      var dfy:Number = curve[1];
      var ddfx:Number = curve[2];
      var ddfy:Number = curve[3];
      var dddfx:Number = curve[4];
      var dddfy:Number = curve[5];
      var x:Number = dfx;
      var y:Number = dfy;
      var lastX:Number;
      var lastY:Number;

      var i:int = BEZIER_SEGMENTS - 2;
      while(true) 
      {
        if(x >= percent) 
        {
          lastX = x - dfx;
          lastY = y - dfy;
          return lastY + (y - lastY) * (percent - lastX) / (x - lastX);
        }
        if (i == 0)
        {
          break;
        }
        i --;
        dfx += ddfx;
        dfy += ddfy;
        ddfx += dddfx;
        ddfy += dddfy;
        x += dfx;
        y += dfy;
      }

      return y + (1 - y) * (percent - x) / (1 - x);
    }*/
    public double GetFrameCurvePercent (DbFrame frame, double percent)
    {
      return 0;
    }
    /*
function transformToGlobal(boneObject:Object, parentObject:Object):void
{
  if(parentObject)
  {
    var x:Number = boneObject[A_X];
    var y:Number = boneObject[A_Y];
    var scaleX:Number = parentObject[A_SCALE_X];
    var scaleY:Number = parentObject[A_SCALE_Y];
    var parentRadian:Number = parentObject[A_ROTATION] * ANGLE_TO_RADIAN;
    boneObject[A_X] = x * Math.cos(parentRadian) * scaleX - y * Math.sin(parentRadian) * scaleY + parentObject[A_X];
    boneObject[A_Y] = x * Math.sin(parentRadian) * scaleX + y * Math.cos(parentRadian) * scaleY + parentObject[A_Y];
    boneObject[A_ROTATION] = formatRotation(boneObject[A_ROTATION] + parentObject[A_ROTATION]);
  }
}*/
    public void TransformToGlobal (SpineBone boneObject, SpineBone parentObject)
    {
      double x = boneObject.x;
      double y = boneObject.y;
      double scaleX = boneObject.scaleX;
      double scaleY = boneObject.scaleY;
      double parentRadian = parentObject.rotation * ANGLE_TO_RADIAN;
      boneObject.x = x * Math.Cos (parentRadian) * scaleX - y * Math.Sin (parentRadian) * scaleY + parentObject.x;
      boneObject.y = x * Math.Sin (parentRadian) * scaleX + y * Math.Cos (parentRadian) * scaleY + parentObject.y;
      boneObject.rotation = FormatRotation (boneObject.rotation + parentObject.rotation);
    }
    /*
    function formatRotation(rotation:Number):Number
    {
      rotation %= 360;
      if (rotation > 180)
      {
        rotation -= 360;
      }
      if (rotation < -180)
      {
        rotation += 360;
      }
      return rotation;
    }*/
    // 360度转换成 -180 至 180
    public double FormatRotation (double rotation)
    {

      rotation %= 360;
      if (rotation > 180) {
        rotation -= 360;
      }

      if (rotation < -180) {
        rotation += 360;
      }

      return rotation;
    }
    /*
    function formatNumber(num:Number, retain:uint = 100):Number
    {
      retain = retain || 100;
      return Math.round(num * retain) / retain;
    }*/
    // 保留几位小数
    public double FormatNumber (double num, uint retain = 100)
    {
      retain = retain > 0 ? retain : 100;
      return Math.Round (num * retain) / retain;
    }
  }
}