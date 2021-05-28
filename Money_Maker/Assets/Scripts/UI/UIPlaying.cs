using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public GameObject gameManager;              //объкт GameMAnager
    private ShopAmmo shopAmmo;                  //компонет ShopAmmo GameMAnager
    public Text announcementShop;               //Переменная объеявление магазина
    public Text remainigTimeToShopText;         //Оставшееся время до открытия магазина


    //Текст во время обрратного отсчета до окрытия магазина
    private string textForRemaninng = "TIME TO BUY AMMO - ";
    //Текст во время открытия магазина 
    private string textForShopping = "SHOP!! SHOP!! SHOP!!";

    private bool isActiveShop; //Флаг, указывающий какое сообщение выводить в текст
    // Start is called before the first frame update
    void Start()
    {
        //Получение компонента
        shopAmmo = gameManager.GetComponent<ShopAmmo>();

        announcementShop.text = textForRemaninng;
        //Получение времени до открытия магазина
        ShowTimeForShopAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        ShowTimeForShopAmmo();
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
   
}
