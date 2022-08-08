using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Touch;

public class Placing : MonoBehaviour
{
    public static Placing instance;
    public Vector3 place;
    public Camera camera;

    public GameObject[] Vehicle;
    public GameObject Timer;
    private Vector3 _gridObjectPosition;
    private Vector3 _gridRotation;
    

    private int carAmount = 0;
    private RaycastHit _Hit;
    public bool placeNow;
    public bool control = false;
    public bool isTimerOpen = false;

    public GameData _GameData;
    public int index = 0;
    public GameObject[] SortCarPlace;
    public GameObject[] SortLockedPlace;
    private int tapCount;
    
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    } 

    private void Start()
    {
        
        SaveSystem.LoadGameState();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOpen == false)
        {
            SendOnDuty();
        }

        if (control == true)
        {
            BuyCarParkingAreas();
        }

        //if (isTimerOpen == false )
        //{
        //    SendOnDuty();
        //}
//
        if (Input.GetTouch(0).tapCount > 0 && placeNow == true)
        {
            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.tag == "grid")
                {
                    if (_GameData.money >= GameManager.instance.costofVehicles[0])
                    {
                        _GameData.money -= GameManager.instance.costofVehicles[0];
                        //Grid objenin rotation ve positionu al variablelara ata.
                        _gridObjectPosition = hitInfo.collider.gameObject.transform.position;
                        _gridRotation = hitInfo.collider.gameObject.transform.rotation.eulerAngles;
                        _gridObjectPosition.y = 1.3f;
                        hitInfo.collider.gameObject.GetComponent<BoxCollider>().enabled = false;

                        //Vertical ve horizantal park alanları için y rotationu için ayrı değer ver.
                        if (_gridRotation.y >= 85f && _gridRotation.y < 95f)
                        {
                            _gridRotation.y = 0f;
                        }
                        else if (_gridRotation.y >= -5f && _gridRotation.y < 5f)
                        {
                            _gridRotation.y = 90f;
                        }

                        place = _gridObjectPosition;




                        //Araç clonenu oluştur.
                        index = int.Parse(hitInfo.collider.name);
                        GameObject araba = Instantiate(Vehicle[VehicleStore.instance.vehicleIndex], place,
                            Quaternion.Euler(_gridRotation));
                        araba.name = index.ToString();
                        carAmount++;

                        Debug.Log("Araba sayısı= " + carAmount);

                        Debug.Log("Grid index : " + index);
                        //Playerprefs ve oyun içine kaydet.
                        _GameData.datas[index].isBought = true;
                        _GameData.datas[index].carIndex = VehicleStore.instance.vehicleIndex;
                        _GameData.datas[index].carName = araba.name;
                        SaveGame();
                        //Kontrol statement
                        placeNow = false;
                    }
                }
            }
        }
        
    }

    public void SendOnDuty()
    {
        if (Input.GetTouch(0).tapCount >= 2 && isTimerOpen == false)
        {
            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.tag == "Vehicles")
                {
                    //Araç objenin rotation ve positionu al variablelara ata.
                    _gridObjectPosition = hitInfo.collider.gameObject.transform.position;
                    _gridRotation = hitInfo.collider.gameObject.transform.rotation.eulerAngles;
                    _gridObjectPosition.y = 1.3f;
                    //hitInfo.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                    
                    //Vertical ve horizantal park alanları için y rotationu için ayrı değer ver.
                   //if (_gridRotation.y >= 85f && _gridRotation.y <95f)
                   //{
                   //    _gridRotation.y = 0f;
                   //}
                   //else if (_gridRotation.y >= -5f && _gridRotation.y < 5f)
                   //{
                   //    _gridRotation.y = 90f;
                   //}
                   
                    place = _gridObjectPosition;
                    //Timerı aç
                    
                    index = int.Parse(hitInfo.collider.name);
                    
                    Debug.Log("car name = " + index);
                    
                    timer_vehicles.instance.OpenWindow();
                    SaveGame();
                    //Kontrol statement
                    placeNow = true;
                    
                }
            }
        }
    }
    
    public void BuyCarParkingAreas()
    {
       
        if (Input.GetTouch(0).tapCount > 0 && control == true)
        {
            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out _Hit))
            {
                BoxCollider bc = _Hit.collider as BoxCollider;
                if (_GameData.money >= GameManager.instance.costOfPlace)
                {
                    if (bc != null && _Hit.transform.tag == "parkingAreas")
                    {
                        if (_GameData.money >= GameManager.instance.costOfPlace)
                        {
                            _GameData.money -= GameManager.instance.costOfPlace;
                            Destroy(bc.gameObject);

                            index = int.Parse(_Hit.collider.name);
                            Debug.Log(index);
                            _GameData.datas[index].isUnlocked = true;
                            //lockedPlace[index] = ;

                            SaveGame();

                            control = false;
                        }
                    }
                }
            }
        }
    }
    
    public void UnlockCarParkingPlace()
    {
        control = true;
    }


    public void placeVehicle()
    {
        placeNow = true;
    }

    

    public void FinishDuty()
    {
        isTimerOpen = false;

    }

    public void SaveGameTimer(int index)
    {
        
        SaveSystem.SaveGameState(_GameData);
        
    }
    
    public void SaveGame()
    {
        SaveSystem.SaveGameState(_GameData);
        
        Debug.Log("Game Saved!");
        
    }

    public void MakeCarClone(GameObject car, Vector3 clonePos, Quaternion cloneRot, int index)
    {
        GameObject araba = Instantiate(car, clonePos, cloneRot);
        araba.name = araba.name.Replace("(Clone)", "");
        araba.name = index.ToString();
        
    }

    public void DestroyLocked(GameObject lockerGrid)
    {
        Destroy(lockerGrid);
    }

    
    //public void OnLeanFingerDown()
    //{
    //    LeanFinger leannfinger = new LeanFinger();
    //    tapCount = leannfinger.TapCount;
    //    
    //    if (isTimerOpen == false && tapCount >= 2)
    //    {
    //        SendOnDuty();
    //    }
    //}


}
