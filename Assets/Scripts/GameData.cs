using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Data")]
[System.Serializable]
public class GameData : ScriptableObject
{
    public DataSlot[] datas;
    public int money;
    public bool isTutorialShowed;



    [System.Serializable]
    public class DataSlot
    {
        public bool isBought;
        public bool isUnlocked;
        public int carIndex;
        public DateTime TimetoEnd;
        public DateTime TimetoStart;
        public string carName;
        public bool inProgress = false;
        public int notificationID;
    }
}


