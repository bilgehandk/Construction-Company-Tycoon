using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class timer_vehicles : MonoBehaviour
{
    public static timer_vehicles instance;
    
    private DateTime TimerStart;
    public DateTime TimerEnd;

    private Coroutine lastTimer;
    private Coroutine lastDisplay;
    
    public GameData PersistentGameData;

    public int currentIndex;

    [Header("Production time")] 
    public int days;
    public int hours;
    public int minutes;
    public int seconds;

    [Header("UI")] 
    [SerializeField] private GameObject window;
    [SerializeField] private Text startTimeText;
    [SerializeField] private Text endTimeText;
    [SerializeField] private GameObject timeLeftObject;
    [SerializeField] private Text timeLeftText;
    [SerializeField] private Slider timeLeftSlider;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button startButton;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    
    #region UnityMehods

    private void Start()
    {
        startButton.onClick.AddListener(StartTimer);
        skipButton.onClick.AddListener(SkipTimer);
        
        
        
    }

    #endregion

    #region UI Methods

    private void InitializeWindow()
    {
        if (PersistentGameData.datas[currentIndex].inProgress)
        {
            startTimeText.text = "Start Time: \n" + TimerStart;
            endTimeText.text = "End Time: \n" + PersistentGameData.datas[currentIndex].TimetoEnd;
            Debug.Log("başlangıç index=" + currentIndex);
        
            timeLeftObject.SetActive(true);
            lastDisplay = StartCoroutine(DisplayTime());
        
            startButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(true);
        }
        else
        {
            startTimeText.text = "Start Time:";
            endTimeText.text = "End Time:";

            timeLeftObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
        
    }

    private IEnumerator DisplayTime()
    {
        DateTime start = WorldTimeAPI.Instance.GetCurrentDateTime();
        TimeSpan timeLeft = PersistentGameData.datas[currentIndex].TimetoEnd - start;
        double totalSecondLeft = timeLeft.TotalSeconds;
        double totalSeconds = (PersistentGameData.datas[currentIndex].TimetoEnd - TimerStart).TotalSeconds;
        string text;

        while (window.activeSelf && timeLeftObject.activeSelf)
        {
            text = "";
            timeLeftSlider.value = 1 - Convert.ToSingle((PersistentGameData.datas[currentIndex].TimetoEnd - WorldTimeAPI.Instance.GetCurrentDateTime()).TotalSeconds / totalSeconds);
            if (totalSecondLeft > 1)
            {
                if (timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h";
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if (timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m";
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Seconds + "s";
                }
                else
                {
                    text += Mathf.FloorToInt((float) totalSecondLeft) + "s";
                }

                timeLeftText.text = text;

                totalSecondLeft -= Time.deltaTime;
                yield return null;
            }
            else
            {
                timeLeftText.text = "Finished";
                skipButton.gameObject.SetActive(false);
                PersistentGameData.datas[currentIndex].inProgress = false;
                startButton.gameObject.SetActive(true);
                break;
            }
        }

        yield return null;
    }

    public void OpenWindow()
    {
        window.SetActive(true);
        currentIndex = Placing.instance.index;
        Debug.Log("şuanki index = "+currentIndex);
        if (currentIndex == null)
        {
            currentIndex = 0;
            PersistentGameData.datas[currentIndex].inProgress = false;
        }
        InitializeWindow();
    }

    public void CloseWindow()
    {
        window.SetActive(false);
    }

    #endregion

    #region Timed Event

    public void StartTimer()
    {
        TimerStart = WorldTimeAPI.Instance.GetCurrentDateTime();
        TimeSpan time = new TimeSpan(days, hours, minutes, seconds);
        Debug.Log("kayıtlı index= "+ currentIndex);
        PersistentGameData.datas[currentIndex].TimetoEnd = TimerStart.Add(time);
        PersistentGameData.datas[currentIndex].inProgress = true;
        
        SaveVehiclesTimer();

        lastTimer = StartCoroutine(Timer(Placing.instance.index));
        
        
        
        InitializeWindow();
    }
    
    private void SaveVehiclesTimer()
    {
        
        Debug.Log("İİNDEX= "+ currentIndex);
        
            
        SaveSystem.SaveGameState(PersistentGameData);
    }

    //public IEnumerator ContinueTimer(int index)
    //{
    //    DateTime start = WorldTimeAPI.Instance.GetCurrentDateTime();
    //    if (PersistentGameData.datas[index].Timer != null)
    //    {
    //        double secondsToFinished = (PersistentGameData.datas[index].Timer - start).TotalSeconds;
    //        Debug.Log("index=" + index +" time for vehilces  = " + secondsToFinished);
    //        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));
//
    //        inProgress = false;
    //        Debug.Log("Finished");
    //        startButton.gameObject.SetActive(true);
    //    }
    //    
    //}

    public IEnumerator Timer(int index)
    {
        DateTime start = WorldTimeAPI.Instance.GetCurrentDateTime();
        double secondsToFinished = (PersistentGameData.datas[index].TimetoEnd - start).TotalSeconds;
        
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));

        PersistentGameData.datas[currentIndex].inProgress = false;
        Debug.Log("Finished");
        PersistentGameData.money += 250000;
        
    }

    //Reklam ekle
    public void SkipTimer()
    {
        TimerEnd = WorldTimeAPI.Instance.GetCurrentDateTime();
        PersistentGameData.datas[currentIndex].inProgress = false;
        StopCoroutine(lastTimer);

        timeLeftText.text = "Finished";
        timeLeftSlider.value = 1;
        
        StopCoroutine(lastDisplay);
        skipButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);

    }

    #endregion



}
