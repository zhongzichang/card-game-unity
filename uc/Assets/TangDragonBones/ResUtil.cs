using System;

namespace TangDragonBones
{
  public class ResUtil
  {
    public static string AtlasPath(string name){
      return Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_atlas";
    }

    public static string TexturePath(string name){
      return Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_texture";
    }

    public static string SkeletonPath(string name){
      return Config.DATA_PATH + Tang.Config.DIR_SEP + name + "_skeleton";
    }

    public static string AtlasName(string name){
      return name + "_atlas";
    }

    public static string TextureName(string name){
      return name + "_texture";
    }

    public static string SkeletonName(string name){
      return name + "_skeleton";
    }

  }
}

