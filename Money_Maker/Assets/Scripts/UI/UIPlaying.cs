using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public GameObject gameManager,              //����� GameManager
                      player;                   //����� Player

    private ShopAmmo shopAmmo;                  //�������� ShopAmmo GameManager
    private CalculateValues calculateValues;    //�������� CalculateValues GameManager

    private Shoot shoot;                        //�������� Shoot Player;

    public Text announcementShop,               //���������� ��������
                remainigTimeToShopText,         //���������� ����� �� �������� ��������
                currentPoints,                  //������� ���������� �����
                currentAmmo,                    //������� ���������� �������� � ������
                currentAmmoInMagazine;          //������� ���������� �������� � �������


    //����� �� ����� ���������� ������� �� ������� ��������
    private string textForRemaningTime = "TIME TO OPEN SHOP: ";
    //����� �� ����� �������� �������� 
    private string textForShopping = "SHOP IS OPEN";

    private bool isActiveShop; //����, ����������� ����� ��������� �������� � �����

    void Start()
    {
        //��������� ����������
        shopAmmo = gameManager.GetComponent<ShopAmmo>();
        //��������� ���������� �������� ����� � 0
        calculateValues = gameManager.GetComponent<CalculateValues>();
        //��������� ���������� Shoot
        shoot = player.GetComponentInChildren<Shoot>();
        //��������� ��������� ������ ��� ����������
        announcementShop.text = textForRemaningTime;
    }


    void Update()
    {
        //����� ����������� ������� �� ��������/�������� ��������
        ShowCurrentValueForText(remainigTimeToShopText, Mathf.Round(shopAmmo.CurrentLetfTime));
        //����� ������ ������ �������� ���������� �����
        ShowCurrentValueForText(currentPoints, calculateValues.PointForKilledEnemy);
        //���������� �������� ���������� �������� � �������� � ���������� �������� ��������
        ShowCurrentValueForText(currentAmmoInMagazine, shoot.CurrentAmmoInMagazine);
        ShowCurrentValueForText(currentAmmo, shoot.CurrentCountAmmo);

    }

    /// <summary>
    /// ��������� ������ ��� ��������� ������
    /// </summary>
    public void SetTextForRemainingTime(bool value = false)
    {
        //����� ������ � ����������� ���������� value
        announcementShop.text = (!value) ? textForRemaningTime : textForShopping;
    }

    /// <summary>
    /// ����� ����������� ������ �� �����
    /// �������� ��������� ������:
    /// ����� �� ��������/�������� ��������
    /// ���������� �����, 
    /// ���������� ��������� � ��������,
    /// ���������� �������� ��������
    /// </summary>
    /// <param name="textValue">������ �� ������, � ������� ��������� ������</param>
    /// <param name="value">��������, ������� ���������</param>
    public void ShowCurrentValueForText(Text textValue, float value)
    {
        //��������� �������� ���������� �����
        textValue.text = value.ToString();
    }

}
