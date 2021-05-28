using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public GameObject gameManager;              //����� GameManager
    private ShopAmmo shopAmmo;                  //�������� ShopAmmo GameManager
    private CalculateValues calculateValues;    //�������� CalculateValues GameManager
    public Text announcementShop;               //���������� ��������
    public Text remainigTimeToShopText;         //���������� ����� �� �������� ��������
    public Text currentPoints;                  //������� ���������� �����


    //����� �� ����� ���������� ������� �� ������� ��������
    private string textForRemaninng = "TIME TO BUY AMMO - ";
    //����� �� ����� �������� �������� 
    private string textForShopping = "SHOP!! SHOP!! SHOP!!";

    private bool isActiveShop; //����, ����������� ����� ��������� �������� � �����

    void Start()
    {
        //��������� ����������
        shopAmmo = gameManager.GetComponent<ShopAmmo>();
        //��������� ���������� �������� ����� � 0
        calculateValues = gameManager.GetComponent<CalculateValues>();

        announcementShop.text = textForRemaninng;
        //��������� ������� �� �������� ��������
        ShowTimeForShopAmmo();
    }


    void Update()
    {
        //����� ����������� ������� �� ��������/�������� ��������
        ShowTimeForShopAmmo();
        //����� ������ ������ �������� ���������� �����
        ShowCurrentPoints();
    }

    /// <summary>
    /// ��������� ������ ��� ��������� ������
    /// </summary>
    public void SetTextForRemainingTime(bool value = false)
    {
        //����� ������ � ����������� ���������� value
        announcementShop.text = (!value) ? textForRemaninng : textForShopping;
    }

    /// <summary>
    /// ��������� ������ 
    /// </summary>
    public void SetTextForShoppingTime()
    {
        announcementShop.text = textForShopping;
    }

    /// <summary>
    /// ����� ��������� ������� ������� �� �����
    /// </summary>
    public void ShowTimeForShopAmmo()
    {
        //�������� ������ �������
        remainigTimeToShopText.text = Mathf.Round(shopAmmo.CurrentLetfTime).ToString();
    }    

    public void ShowCurrentPoints()
    {
        //��������� �������� ���������� �����
        currentPoints.text = calculateValues.GetCountPoints().ToString();
    }
}
