using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    
    public static void MoveYIn(RectTransform r, float y)
    {
        r.gameObject.SetActive(true);
        LeanTween.moveY(r, y, 0.5f).setEaseInOutBack();
    }
    public static void MoveYOut(RectTransform r, float y)
    {
        LeanTween.moveY(r, y, 0.5f).setEaseInOutBack().setOnComplete(delegate () {
            r.gameObject.SetActive(false);
        });
    }
}
