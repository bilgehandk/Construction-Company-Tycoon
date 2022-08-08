using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera _camera;
    public GameData persitentGameData;
    public Text goldDisplay;
    public GameObject store;
    public GameObject blocker;
    public GameObject mainPage;
    public GameObject IsometricCamera;
    private bool control = false;
    public RectTransform unclockedPopUp;
    public RectTransform tutorialRect;
    //public GameObject Area1;

    public int costOfPlace = 250000;
    public int[] costofVehicles = {250000};

    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        SaveSystem.LoadGameState();
    }


    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.LoadGameMoney();
        
        if (persitentGameData.isTutorialShowed == false)
        {
            OpenTutorial();
        }
    }

    // Update is called once per frame
    void Update()
    {
        goldDisplay.text =string.Format("{0:n0}", persitentGameData.money.ToString());
    }

    public void OpenStore()
    {
        store.SetActive(true);
        mainPage.gameObject.SetActive(false);
        blocker.gameObject.SetActive(true);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = false;
    }

    public void ExitStore()
    {
        store.SetActive(false);
        mainPage.gameObject.SetActive(true);
        blocker.gameObject.SetActive(false);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenUnlockedPopup()
    {
        Utils.MoveYIn(unclockedPopUp, 0);
        mainPage.gameObject.SetActive(false);
        blocker.gameObject.SetActive(true);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = false;
        
    }

    public void CloseUnlockedPopup()
    {
        Utils.MoveYOut(unclockedPopUp, -1600);
        mainPage.gameObject.SetActive(true);
        blocker.gameObject.SetActive(false);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
    }

    public void OpenTutorial()
    {
        Utils.MoveYIn(tutorialRect, 0);
        mainPage.gameObject.SetActive(false);
        blocker.gameObject.SetActive(true);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = false;
        
    }

    public void CloseTutorial()
    {
        Utils.MoveYOut(tutorialRect, -1600);
        mainPage.gameObject.SetActive(true);
        blocker.gameObject.SetActive(false);
        IsometricCamera.GetComponent<IsometricCameraControl>().enabled = true;
        persitentGameData.isTutorialShowed = true;
        SaveSystem.SaveGameState(persitentGameData);
    }
    
}
