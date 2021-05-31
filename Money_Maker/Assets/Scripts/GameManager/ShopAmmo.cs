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

    private string addedAmmo = "ADDED AMMO"; //текст, выводимый на экран при покупке патровнов
    private string notHavePoints = "DON'T ENOUGH POINTS"; //текст, выводимый на экран при отсутствии очков на покупке патронов
    private string shopIsClose = "SHOP IS CLOSED!!"; //текст, выводимый на экран при попытке купить патроны, когда магазин закрыт

    private void Start()
    {
        //Получение компонента CalculateValues
        calculateValue = GetComponent<CalculateValues>();
        //Получение компонента UIPlaying
        uiPlaying = uiManager.GetComponentInChildren<UIPlaying>();
        //Получение компонента Shoot
        shoot = player.GetComponentInChildren<Shoot>();

        IsActiveShop = false;

        //Определение, какое время включать для обратного отсчета
        CurrentLetfTime = DecideTimeRemain(IsActiveShop);
    }
    private void Update()
    {
        //Отсчет обратного времени
        CurrentLetfTime -= Time.deltaTime;
        //При достижении времени отсчета равное либо меньше 0, смена флага isActiveShop на противоположный и смена времени для обратного отсчета
        if (CurrentLetfTime <= 0)
        {
            //Смена флага для изменения статуса магазина(открыт/закрыт)
            SetValueForShop(IsActiveShop);
            //Передача значения IsActiveShop в игровой интерфейс для показа статуса магазина
            uiPlaying.SetTextForRemainingTime(IsActiveShop);
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
       return (!shopStatus) ? periodBetweenBuying : timeAccessToShop;
    }

    /// <summary>
    /// Мена флага на противоположный
    /// </summary>
    /// <param name="value">Значение флага</param>
    private void SetValueForShop(bool value)
    {
        IsActiveShop = !value;
    }

    /// <summary>
    /// Попытка покупки патронов за очки
    /// </summary>
    public void TryBuyAmmo()
    {
        //Проверка на открытие магазина, если закрыт - вывод сообщения о том, что магазин закрыт
        if (IsActiveShop)
        {
            //Если количество очков >= pricePointForSellingAmmo, то покупка патронов, иначе вывод сообщения об недостаточности очков
            if(calculateValue.PointForKilledEnemy >= pricePointForSellingAmmo)
            {
                //вычитание из общего количества очков цену за покупку боеприпасов
                calculateValue.PointForKilledEnemy -= pricePointForSellingAmmo;
                //добавление ук текущему количеству боеприпасов купленные патроны
                shoot.CurrentCountAmmo += countBuyingAmmo;
                //вывод сообщения на экран о покупке боеприпасов
                uiManager.GetComponent<UIManager>().ShowAnnounceMenu(addedAmmo);

            } else uiManager.GetComponent<UIManager>().ShowAnnounceMenu(notHavePoints); //Вывод сообщения о недостаточности очков для покупки патронов

        } else uiManager.GetComponent<UIManager>().ShowAnnounceMenu(shopIsClose); //Вывод сообщения о том, что магазин закрыт
    }
}
