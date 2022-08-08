using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoneyInfo : MonoBehaviour
{
    public static ShowMoneyInfo instance;
    public Text moneyInfo;
    public GameData _gameData;

    // Start is called before the first frame update
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    void Start()
    {
        SaveSystem.LoadGameMoney();
        RefreshMoneyText();
    }

   public void RefreshMoneyText()
   {
       moneyInfo.text = GetComponent<Text>().text;
       moneyInfo.text =string.Format("{0:n0}", _gameData.money.ToString());
   }
}
