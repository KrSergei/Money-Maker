using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public GameObject gameManager;              //объкт GameManager
    private ShopAmmo shopAmmo;                  //компонет ShopAmmo GameManager
    private CalculateValues calculateValues;    //компонет CalculateValues GameManager
    public Text announcementShop;               //Объявления магазина
    public Text remainigTimeToShopText;         //Оставшееся время до открытия магазина
    public Text currentPoints;                  //Текущие количество очков


    //Текст во время обрратного отсчета до окрытия магазина
    private string textForRemaninng = "TIME TO BUY AMMO - ";
    //Текст во время открытия магазина 
    private string textForShopping = "SHOP!! SHOP!! SHOP!!";

    private bool isActiveShop; //Флаг, указывающий какое сообщение выводить в текст

    void Start()
    {
        //Получение компонента
        shopAmmo = gameManager.GetComponent<ShopAmmo>();
        //Установка начального значения очков в 0
        calculateValues = gameManager.GetComponent<CalculateValues>();

        announcementShop.text = textForRemaninng;
        //Получение времени до открытия магазина
        ShowTimeForShopAmmo();
    }


    void Update()
    {
        //Показ оставшегося времени до закртыия/открытия магазина
        ShowTimeForShopAmmo();
        //Вызов метода показа текущего количество очков
        ShowCurrentPoints();
    }

    /// <summary>
    /// Установка текста для обратного отчета
    /// </summary>
    public void SetTextForRemainingTime(bool value = false)
    {
        //Смена текста в зависимости отзначения value
        announcementShop.text = (!value) ? textForRemaninng : textForShopping;
    }

    /// <summary>
    /// Установка текста 
    /// </summary>
    public void SetTextForShoppingTime()
    {
        announcementShop.text = textForShopping;
    }

    /// <summary>
    /// Вывод обратного отсчета времени на экран
    /// </summary>
    public void ShowTimeForShopAmmo()
    {
        //Обратный отсчет времени
        remainigTimeToShopText.text = Mathf.Round(shopAmmo.CurrentLetfTime).ToString();
    }    

    public void ShowCurrentPoints()
    {
        //Получение текущего количества очков
        currentPoints.text = calculateValues.GetCountPoints().ToString();
    }
}
