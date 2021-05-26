using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    [SerializeField]
    private int maxCountAmmo;            //Общее максимальное количество патронов
    [SerializeField]
    private int maxCountAmmoInMagazine;  //Максимальное количество патронов в магазине
    [SerializeField]
    private float timeBetweenBuyAmmo;    //Пауза между предложением для покупки патронов
    [SerializeField]
    private float timeForBuyingAmmo;     //Период времени, в который можно купить патроны
    [SerializeField]
    private int countBoughtAmmo;         //Количество патронов при разовой покупки

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
