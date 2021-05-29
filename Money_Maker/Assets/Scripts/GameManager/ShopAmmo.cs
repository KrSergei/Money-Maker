using UnityEngine;

public class ShopAmmo : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject player;               //объкт Player


    private UIPlaying uiPlaying;            //Игровой интефейс

    private CalculateValues calculateValue;
    public float periodBetweenBuying;       //Время между активацией магазина для покупки патронов
    public float timeAccessToShop;          //Время активации магазина для покупки патронов
    public int countBuyingAmmo;             //Количество покупаемых патронов
    public int pricePointForSellingAmmo;    //Количество очков за которые покупаются патроны

    private Shoot shoot;                    //компонет Shoot Player;

    public float CurrentLetfTime { get; set; }
  
    [SerializeField]
    private bool isActiveShop = false;
    public bool IsActiveShop { get => isActiveShop; set => isActiveShop = value; }

    public bool GetIsActiveShop()
    {
        return IsActiveShop;
    }

    public void SetIsActiveShop(bool value)
    {
        IsActiveShop = value;
    }

    private void Start()
    {
        calculateValue = GetComponent<CalculateValues>();
        uiPlaying = uiManager.GetComponentInChildren<UIPlaying>();
        //Получение компонента Shoot
        shoot = player.GetComponentInChildren<Shoot>();

        IsActiveShop = false;

        //Определение, какое время включать для обратного отсчета
        CurrentLetfTime = DecideTimeRemain(GetIsActiveShop());
        Debug.Log(CurrentLetfTime);
    }
    private void Update()
    {
        //Отсчет обратного времени
        CurrentLetfTime -= Time.deltaTime;
        //При достижении времени отсчета равное либо меньше 0, смена флага isActiveShop на противоположный и смена времени для обратного отсчета
        if (CurrentLetfTime <= 0)
        {
            //Передача значения IsActiveShop в игровой интерфейс для показа статуса магазина
            uiPlaying.SetTextForRemainingTime(GetIsActiveShop());
            //Смена флага для изменения статуса магазина(открыт/закрыт)
            SetValueForShop(GetIsActiveShop());
            //Определение нового времени для отсчета в зависимости от состояния флага isActiveShop
            CurrentLetfTime = DecideTimeRemain(GetIsActiveShop());
        }
    }

    /// <summary>
    /// мена времени для обртаного отчета в зависимости от флага isActiveShop
    /// </summary>
    /// <param name="shopStatus">Значение флага</param>
    /// <returns></returns>
    private float DecideTimeRemain(bool shopStatus)
    {
       return (!shopStatus) ? periodBetweenBuying : timeAccessToShop;
    }

    /// <summary>
    /// Мена флага на противоположный
    /// </summary>
    /// <param name="value">Значение флага</param>
    private void SetValueForShop(bool value)
    {
        SetIsActiveShop(!value);
    }

    /// <summary>
    /// Попытка покупки патронов за очки
    /// </summary>
    public void TryBuyAmmo()
    {
        Debug.Log("GetIsActiveShop - " + GetIsActiveShop());
        //Проверка на открытие магазина и что
        if (GetIsActiveShop())
        {
            //Если количество очков >= pricePointForSellingAmmo, то покупка патронов, иначе вывод сообщения об недостаточности очков
            if(calculateValue.PointForKilledEnemy >= pricePointForSellingAmmo)
            {
                calculateValue.PointForKilledEnemy -= pricePointForSellingAmmo;
                shoot.CurrentCountAmmo += countBuyingAmmo;
                Debug.Log("BOUTH AMMO");

            } else  Debug.Log("YOU ARE BAD SHOOTER!!! DON'T ENOUGH POINTS");

        } else  Debug.Log("SHOP IS CLOSLED!! CLOSED!! CLOSED!!");
      
    }
}
