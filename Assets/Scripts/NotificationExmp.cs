using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using UnityEngine;

public class NotificationExmp : MonoBehaviour
{
    
    public GameData _GameData;
    
    public void ScheduleCustom()
    {
        var notificationParams = new NotificationParams
        {
            Id = UnityEngine.Random.Range(0, int.MaxValue),
            Delay = TimeSpan.FromHours(8),
            Title = "Your vehicle is returned from the work",
            Message = "Check your car and resend to work",
            Ticker = "Ticker",
            Sound = true,
            Vibrate = true,
            Light = true,
            SmallIcon = NotificationIcon.Heart,
            SmallIconColor = new Color(0, 0.5f, 0),
            LargeIcon = "icon_1"
        };

        NotificationManager.SendCustom(notificationParams);
        _GameData.datas[Placing.instance.index].notificationID = notificationParams.Id;
        Debug.Log("notificationParams = " + notificationParams.Id);
    }
    

    public void CancelCustom()
    {
        NotificationManager.Cancel(_GameData.datas[Placing.instance.index].notificationID);
    }
    
}


