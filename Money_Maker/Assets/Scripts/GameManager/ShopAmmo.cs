using UnityEngine;

public class ShopAmmo : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject player;               //����� Player


    private UIPlaying uiPlaying;            //������� ��������

    private CalculateValues calculateValue;
    public float periodBetweenBuying;       //����� ����� ���������� �������� ��� ������� ��������
    public float timeAccessToShop;          //����� ��������� �������� ��� ������� ��������
    public int countBuyingAmmo;             //���������� ���������� ��������
    public int pricePointForSellingAmmo;    //���������� ����� �� ������� ���������� �������

    private Shoot shoot;                    //�������� Shoot Player;

    public float CurrentLetfTime { get; set; }
    

    [SerializeField]
    private bool isActiveShop = false;
    public bool IsActiveShop { get => isActiveShop; set => isActiveShop = value; }  

    private string addedAmmo = "ADDED AMMO"; //�����, ��������� �� ����� ��� ������� ���������
    private string notHavePoints = "DON'T ENOUGH POINTS"; //�����, ��������� �� ����� ��� ���������� ����� �� ������� ��������
    private string shopIsClose = "SHOP IS CLOSED!!"; //�����, ��������� �� ����� ��� ������� ������ �������, ����� ������� ������

    private void Start()
    {
        //��������� ���������� CalculateValues
        calculateValue = GetComponent<CalculateValues>();
        //��������� ���������� UIPlaying
        uiPlaying = uiManager.GetComponentInChildren<UIPlaying>();
        //��������� ���������� Shoot
        shoot = player.GetComponentInChildren<Shoot>();

        IsActiveShop = false;

        //�����������, ����� ����� �������� ��� ��������� �������
        CurrentLetfTime = DecideTimeRemain(IsActiveShop);
    }
    private void Update()
    {
        //������ ��������� �������
        CurrentLetfTime -= Time.deltaTime;
        //��� ���������� ������� ������� ������ ���� ������ 0, ����� ����� isActiveShop �� ��������������� � ����� ������� ��� ��������� �������
        if (CurrentLetfTime <= 0)
        {
            //����� ����� ��� ��������� ������� ��������(������/������)
            SetValueForShop(IsActiveShop);
            //�������� �������� IsActiveShop � ������� ��������� ��� ������ ������� ��������
            uiPlaying.SetTextForRemainingTime(IsActiveShop);
            //����������� ������ ������� ��� ������� � ����������� �� ��������� ����� isActiveShop
            CurrentLetfTime = DecideTimeRemain(IsActiveShop);
        }
    }

    /// <summary>
    /// ���� ������� ��� ��������� ������ � ����������� �� ����� isActiveShop
    /// </summary>
    /// <param name="shopStatus">�������� �����</param>
    /// <returns></returns>
    private float DecideTimeRemain(bool shopStatus)
    {
       return (!shopStatus) ? periodBetweenBuying : timeAccessToShop;
    }

    /// <summary>
    /// ���� ����� �� ���������������
    /// </summary>
    /// <param name="value">�������� �����</param>
    private void SetValueForShop(bool value)
    {
        IsActiveShop = !value;
    }

    /// <summary>
    /// ������� ������� �������� �� ����
    /// </summary>
    public void TryBuyAmmo()
    {
        //�������� �� �������� ��������, ���� ������ - ����� ��������� � ���, ��� ������� ������
        if (IsActiveShop)
        {
            //���� ���������� ����� >= pricePointForSellingAmmo, �� ������� ��������, ����� ����� ��������� �� ��������������� �����
            if(calculateValue.PointForKilledEnemy >= pricePointForSellingAmmo)
            {
                //��������� �� ������ ���������� ����� ���� �� ������� �����������
                calculateValue.PointForKilledEnemy -= pricePointForSellingAmmo;
                //���������� �� �������� ���������� ����������� ��������� �������
                shoot.CurrentCountAmmo += countBuyingAmmo;
                //����� ��������� �� ����� � ������� �����������
                uiManager.GetComponent<UIManager>().ShowAnnounceMenu(addedAmmo);

            } else uiManager.GetComponent<UIManager>().ShowAnnounceMenu(notHavePoints); //����� ��������� � ��������������� ����� ��� ������� ��������

        } else uiManager.GetComponent<UIManager>().ShowAnnounceMenu(shopIsClose); //����� ��������� � ���, ��� ������� ������
    }
}
