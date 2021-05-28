using UnityEngine;

public class ShopAmmo : MonoBehaviour
{
    public GameObject uiManager;
    private UIPlaying uiPlaying;            //Игровой интефейс

    private CalculateValues calculateValue;
    public float periodBetweenBuying;       //Время между активацией магазина для покупки патронов
    public float timeAccessToShop;          //Время активации магазина для покупки патронов

    public float CurrentLetfTime { get; set; }
    public bool IsActiveShop { get; set; } = false;

    private void Start()
    {
        uiPlaying = uiManager.GetComponentInChildren<UIPlaying>();
        //Определение, какое время включать для обратного отсчета
        DecideTimeRemain(IsActiveShop);
    }
    private void Update()
    {
        //Отсчет обратного времени
        CurrentLetfTime -= Time.deltaTime;
        //При достижении времени отсчета равное либо меньше 0, смена флага isActiveShop на противоположный и смена времени для обратного отсчета
        if (CurrentLetfTime <= 0)
        {
            //Передача значения IsActiveShop в игровой интерфейс для показа статуса магазина
            uiPlaying.SetTextForRemainingTime(IsActiveShop);
            //Смена флага для изменения статуса магазина(открыт/закрыт)
            SetValueForShop(IsActiveShop);
            //Определение нового времени для отсчета в зависимости от состояния флага isActiveShop
            CurrentLetfTime = DecideTimeRemain(IsActiveShop);
        }
    }

    /// <summary>
    /// мена времени для обртаного отчета в зависимости от флага isActiveShop
    /// </summary>
    /// <param name="shopStatus">Значение флага</param>
    /// <returns></returns>
    private float DecideTimeRemain(bool shopStatus)
    {
       return (shopStatus) ? periodBetweenBuying : timeAccessToShop;
    }

    /// <summary>
    /// Мена флага на противоположный
    /// </summary>
    /// <param name="value">Значение флага</param>
    private void SetValueForShop(bool value)
    {
        IsActiveShop = !value;
    }
}
