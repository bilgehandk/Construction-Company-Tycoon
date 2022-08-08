using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleStore : MonoBehaviour
{
    public static VehicleStore instance;
    public int vehicleIndex;

    //public RectTransform MainPageRect;
    public RectTransform VehicleStoreRect;
    public GameObject blocker;
    public GameObject mainPage;
    public GameObject IsometricCamera;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void OpenVehicleStore()
    {
        Utils.MoveYIn(VehicleStoreRect, 1280);
        mainPage.gameObject.SetActive(false);
        blocker.gameObject.SetActive(true);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = false;
    }

    public void Exit()
    {
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        blocker.gameObject.SetActive(false);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void BullDozer1Button()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 0;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void BullDozer2Button()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 1;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void CementMixerButton()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 2;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void CraneButton()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 3;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void DumpingTrackButton()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 4;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void LifterButton()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 5;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void RoadRollerButton()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 6;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
        
    }

    public void TruckButton()
    {
        blocker.gameObject.SetActive(false);
        vehicleIndex = 7;
        Utils.MoveYOut(VehicleStoreRect, -2000);
        mainPage.gameObject.SetActive(true);
        Placing.instance.placeVehicle();
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }
}
