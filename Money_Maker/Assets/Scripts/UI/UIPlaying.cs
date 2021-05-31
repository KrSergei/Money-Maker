using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public GameObject gameManager,              //объкт GameManager
                      player;                   //объкт Player

    private ShopAmmo shopAmmo;                  //компонет ShopAmmo GameManager
    private CalculateValues calculateValues;    //компонет CalculateValues GameManager

    private Shoot shoot;                        //компонет Shoot Player;

    public Text announcementShop,               //Объявления магазина
                remainigTimeToShopText,         //Оставшееся время до открытия магазина
                currentPoints,                  //Текущие количество очков
                currentAmmo,                    //Текущее количество патронов в запасе
                currentAmmoInMagazine;          //Текущее количество патронов в мгазине


    //Текст во время обрратного отсчета до окрытия магазина
    private string textForRemaningTime = "TIME TO OPEN SHOP: ";
    //Текст во время открытия магазина 
    private string textForShopping = "SHOP IS OPEN";

    private bool isActiveShop; //Флаг, указывающий какое сообщение выводить в текст

    void Start()
    {
        //Получение компонента
        shopAmmo = gameManager.GetComponent<ShopAmmo>();
        //Установка начального значения очков в 0
        calculateValues = gameManager.GetComponent<CalculateValues>();
        //Получение компонента Shoot
        shoot = player.GetComponentInChildren<Shoot>();
        //Стартовая установка строки для объявления
        announcementShop.text = textForRemaningTime;
    }


    void Update()
    {
        //Показ оставшегося времени до закртыия/открытия магазина
        ShowCurrentValueForText(remainigTimeToShopText, Mathf.Round(shopAmmo.CurrentLetfTime));
        //Вызов метода показа текущего количество очков
        ShowCurrentValueForText(currentPoints, calculateValues.PointForKilledEnemy);
        //Обновление значений количества патронов в магазине и количества запасных патронов
        ShowCurrentValueForText(currentAmmoInMagazine, shoot.CurrentAmmoInMagazine);
        ShowCurrentValueForText(currentAmmo, shoot.CurrentCountAmmo);

    }

    /// <summary>
    /// Установка текста для обратного отчета
    /// </summary>
    public void SetTextForRemainingTime(bool value = false)
    {
        //Смена текста в зависимости отзначения value
        announcementShop.text = (!value) ? textForRemaningTime : textForShopping;
    }

    /// <summary>
    /// Вывод обновляемых данных на экран
    /// Выводтся следующие данные:
    /// время до открытия/закрытия магазина
    /// количество очков, 
    /// количество патровнов в магазине,
    /// количество запасных патронов
    /// </summary>
    /// <param name="textValue">Строка на экране, в которую выводятся данные</param>
    /// <param name="value">значение, которое выводится</param>
    public void ShowCurrentValueForText(Text textValue, float value)
    {
        //Получение текущего количества очков
        textValue.text = value.ToString();
    }

}
