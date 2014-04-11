using System;
using System.IO;
using LitJson;
using TangConvert;

namespace CodeDemo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");


			string path = "/Users/zhongzichang/Downloads/spine/skeleton.json";
			string jsonText = File.ReadAllText (path);


			//PrintJson (jsonText);

			DragonBones dragonBones = JsonMapper.ToObject<DragonBones> (jsonText);

			//string filterdJsonText = JsonMapper.ToJson (dragonBones);

      JsonWriter writer = new JsonWriter(Console.Out);
      writer.PrettyPrint = true;
      JsonMapper.ToJson (dragonBones, writer);
      Console.WriteLine ("");


		}

	}
}
