using UnityEngine;

public class ShopAmmo : MonoBehaviour
{
    public GameObject uiManager;
    private UIPlaying uiPlaying;            //������� ��������

    private CalculateValues calculateValue;
    public float periodBetweenBuying;       //����� ����� ���������� �������� ��� ������� ��������
    public float timeAccessToShop;          //����� ��������� �������� ��� ������� ��������

    public float CurrentLetfTime { get; set; }
    public bool IsActiveShop { get; set; } = false;

    private void Start()
    {
        uiPlaying = uiManager.GetComponentInChildren<UIPlaying>();
        //�����������, ����� ����� �������� ��� ��������� �������
        DecideTimeRemain(IsActiveShop);
    }
    private void Update()
    {
        //������ ��������� �������
        CurrentLetfTime -= Time.deltaTime;
        //��� ���������� ������� ������� ������ ���� ������ 0, ����� ����� isActiveShop �� ��������������� � ����� ������� ��� ��������� �������
        if (CurrentLetfTime <= 0)
        {
            //�������� �������� IsActiveShop � ������� ��������� ��� ������ ������� ��������
            uiPlaying.SetTextForRemainingTime(IsActiveShop);
            //����� ����� ��� ��������� ������� ��������(������/������)
            SetValueForShop(IsActiveShop);
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
       return (shopStatus) ? periodBetweenBuying : timeAccessToShop;
    }

    /// <summary>
    /// ���� ����� �� ���������������
    /// </summary>
    /// <param name="value">�������� �����</param>
    private void SetValueForShop(bool value)
    {
        IsActiveShop = !value;
    }
}
