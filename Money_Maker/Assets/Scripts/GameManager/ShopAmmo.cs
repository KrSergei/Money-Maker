using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAmmo : MonoBehaviour
{
    private CalculateValues calculateValue;

    public float periodBetweenBuying;       //Время между активацией магазина для покупки патронов
    private float timeAccessToShop;         //Время активации магазина для покупки патронов
    private bool shopIsActive;              //Флаг активности магазина
    private bool isCountDownTime;           //Флаг обратного отсчета времени

    private void Start()
    {
        
    }

    public void StartTime(float time)
    {

    }

    public void CountDownTime(float time)
    {
        
    }

}
