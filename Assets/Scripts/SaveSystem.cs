using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;
using UnityEngine;
using BayatGames.SaveGameFree;
using Lean.Common;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Rendering;


[Serializable]
public static class SaveSystem
{
    public static GameData PersistentGameData;
    public static int money;

    public static void SaveGameState(GameData gameData)
    {
        SaveGame.Encode = true;
        SaveGame.EncodePassword = "1";
        PersistentGameData = gameData;
        
        SaveGame.Save<GameData>("gd",gameData);
        Debug.Log("Kayıt başarılı oalrak tamamlandı.");
    }

    public static void LoadGameMoney()
    {
        SaveGame.Encode = true;
        SaveGame.EncodePassword = "1";

        if (SaveGame.Exists("gd"))
        {
            PersistentGameData = SaveGame.Load<GameData>("gd", new GameData(), "1");
            money = PersistentGameData.money;
        }
    }

    public static void LoadGameState()
    {
        SaveGame.Encode = true;
        SaveGame.EncodePassword = "1";

        if (SaveGame.Exists("gd"))
        {
            PersistentGameData = SaveGame.Load<GameData>("gd", new GameData(), "1");

            for (int i = 0; i < 72; i++)
            {
                var lockedArea = Placing.instance.SortLockedPlace[i];
                var carPlaces = Placing.instance.SortCarPlace[i];
                if (PersistentGameData.datas[i].isUnlocked == true)
                {
                    if (lockedArea != null)
                    {
                        if (lockedArea.transform.tag == "parkingAreas")
                        {
                            Placing.instance.DestroyLocked(lockedArea);
                        }
                    }



                    if (PersistentGameData.datas[i].isBought == true)
                    {
                        if (carPlaces != null)
                        {
                            var persistentPosition = carPlaces.transform.position;
                            persistentPosition.y = 1.3f;
                            var persistentRotation = carPlaces.transform.rotation.eulerAngles;
                            
                            Debug.Log("rot -y = " + persistentRotation.y);

                            if (persistentRotation.y >= 85f && persistentRotation.y < 95f)
                            {
                                persistentRotation.y = 0f;
                            }
                            else if (persistentRotation.y >= -5f && persistentRotation.y < 5f)
                            {
                                persistentRotation.y = 90f;
                            }
                            
                            
                            

                            if (PersistentGameData.datas[i].carIndex != null)
                            {
                                GameObject car = Placing.instance.Vehicle[PersistentGameData.datas[i].carIndex];

                                Placing.instance.MakeCarClone(car, persistentPosition, Quaternion.Euler(persistentRotation), i);
                                car.name = i.ToString();



                                
                                timer_vehicles.instance.Timer(i);
                                
                            }
                            
                            


                        }

                    }
                }

            }

        }
    }
    
    
}
