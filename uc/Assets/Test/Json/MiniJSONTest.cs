using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using UnityEngine;
using Procurios.Public;
public class MiniJSONTest : MonoBehaviour
{
  void Start ()
  {
    var jsonString = "{ \"array\": [1.44,2,3], " +
                     "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
                     "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
                     "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
                     "\"int\": 65536, " +
                     "\"float\": 3.1415926, " +
                     "\"bool\": true, " +
                     "\"null\": null }";
   
    var dict = JSON.JsonDecode (jsonString) as Hashtable;

    Debug.Log ("deserialized: " + dict.GetType ());
    Debug.Log ("dict['array'][0]: " + ((ArrayList)dict ["array"]) [0]);
    Debug.Log ("dict['string']: " + (string)dict ["string"]);
    Debug.Log ("dict['float']: " + (double)dict ["float"]);
    Debug.Log ("dict['int']: " + (double)dict ["int"]); // ints come out as longs
    Debug.Log ("dict['unicode']: " + (string)dict ["unicode"]);

    var str = JSON.JsonEncode (dict);

    Debug.Log ("serialized: " + str);
  }
}

