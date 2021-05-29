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
        //��������� ���������� Shoot
        shoot = player.GetComponentInChildren<Shoot>();

        IsActiveShop = false;

        //�����������, ����� ����� �������� ��� ��������� �������
        CurrentLetfTime = DecideTimeRemain(GetIsActiveShop());
        Debug.Log(CurrentLetfTime);
    }
    private void Update()
    {
        //������ ��������� �������
        CurrentLetfTime -= Time.deltaTime;
        //��� ���������� ������� ������� ������ ���� ������ 0, ����� ����� isActiveShop �� ��������������� � ����� ������� ��� ��������� �������
        if (CurrentLetfTime <= 0)
        {
            //�������� �������� IsActiveShop � ������� ��������� ��� ������ ������� ��������
            uiPlaying.SetTextForRemainingTime(GetIsActiveShop());
            //����� ����� ��� ��������� ������� ��������(������/������)
            SetValueForShop(GetIsActiveShop());
            //����������� ������ ������� ��� ������� � ����������� �� ��������� ����� isActiveShop
            CurrentLetfTime = DecideTimeRemain(GetIsActiveShop());
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
        SetIsActiveShop(!value);
    }

    /// <summary>
    /// ������� ������� �������� �� ����
    /// </summary>
    public void TryBuyAmmo()
    {
        Debug.Log("GetIsActiveShop - " + GetIsActiveShop());
        //�������� �� �������� �������� � ���
        if (GetIsActiveShop())
        {
            //���� ���������� ����� >= pricePointForSellingAmmo, �� ������� ��������, ����� ����� ��������� �� ��������������� �����
            if(calculateValue.PointForKilledEnemy >= pricePointForSellingAmmo)
            {
                calculateValue.PointForKilledEnemy -= pricePointForSellingAmmo;
                shoot.CurrentCountAmmo += countBuyingAmmo;
                Debug.Log("BOUTH AMMO");

            } else  Debug.Log("YOU ARE BAD SHOOTER!!! DON'T ENOUGH POINTS");

        } else  Debug.Log("SHOP IS CLOSLED!! CLOSED!! CLOSED!!");
      
    }
}
