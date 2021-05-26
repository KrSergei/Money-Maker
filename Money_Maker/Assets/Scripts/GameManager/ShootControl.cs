using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    [SerializeField]
    private int maxCountAmmo;            //����� ������������ ���������� ��������
    [SerializeField]
    private int maxCountAmmoInMagazine;  //������������ ���������� �������� � ��������
    [SerializeField]
    private float timeBetweenBuyAmmo;    //����� ����� ������������ ��� ������� ��������
    [SerializeField]
    private float timeForBuyingAmmo;     //������ �������, � ������� ����� ������ �������
    [SerializeField]
    private int countBoughtAmmo;         //���������� �������� ��� ������� �������

    public int MaxCountAmmo { get => maxCountAmmo; set => maxCountAmmo = value; }
    public int MaxCountAmmoInMagazine { get => maxCountAmmoInMagazine; set => maxCountAmmoInMagazine = value; }
    public float TimeBetweenBuyAmmo { get => timeBetweenBuyAmmo; set => timeBetweenBuyAmmo = value; }
    public float TimeForBuyingAmmo { get => timeForBuyingAmmo; set => timeForBuyingAmmo = value; }
    public int CountBoughtAmmo { get => countBoughtAmmo; set => countBoughtAmmo = value; }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
