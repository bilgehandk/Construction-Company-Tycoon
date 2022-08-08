using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataTimer
{
   public string timeStart;
   public string timeEnd;

   public DataTimer()
   {
      timeStart = "";
      timeEnd = "";
   }

   public DataTimer(DateTime start, DateTime end)
   {
      timeStart = start.ToString();
      timeEnd = end.ToString();

      var tempArray = GameObject.FindGameObjectsWithTag("grid");
      for (int i = 0; i < tempArray.Length; i++)
      {
         Debug.Log("sda: "+ tempArray[i].name);
      }
   }
   
   

}
