using UnityEngine;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public GameObject gameManager;              //����� GameMAnager
    private ShopAmmo shopAmmo;                  //�������� ShopAmmo GameMAnager
    public Text announcementShop;               //���������� ����������� ��������
    public Text remainigTimeToShopText;         //���������� ����� �� �������� ��������


    //����� �� ����� ���������� ������� �� ������� ��������
    private string textForRemaninng = "TIME TO BUY AMMO - ";
    //����� �� ����� �������� �������� 
    private string textForShopping = "SHOP!! SHOP!! SHOP!!";

    private bool isActiveShop; //����, ����������� ����� ��������� �������� � �����
    // Start is called before the first frame update
    void Start()
    {
        //��������� ����������
        shopAmmo = gameManager.GetComponent<ShopAmmo>();

        announcementShop.text = textForRemaninng;
        //��������� ������� �� �������� ��������
        ShowTimeForShopAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        ShowTimeForShopAmmo();
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
   
}
