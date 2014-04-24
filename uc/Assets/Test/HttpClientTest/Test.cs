using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using JsonFx.Json;

public class HeroResult
{
	public string name;
	public int id;
}

public class Test : MonoBehaviour
{
	private JsonReader reader = new JsonReader();

	void OnGUI(){
		if(GUILayout.Button("reqeust")){
			HTTP.Request http = new HTTP.Request( "get", "http://localhost:4004/" );
			http.AddHeader("Accept", "application/json");
			http.Send( HandleResponse );
		}
	}

	void HandleResponse(HTTP.Request request){
		Debug.Log( request.response.Text );
		HeroResult[] heroes = reader.Read<HeroResult[]>(request.response.Text );
		Debug.Log( heroes.Length );
		foreach(HeroResult hero in heroes){
			Debug.Log(hero.name);
		}
	}
}
