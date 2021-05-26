using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gameManager;      //Объект управления игрой

    public GameObject bullet;           //Объект пули
    public Transform  shootPos;         //Место появления пули

    public float delayTimeShooting;     //интервал между выстрелами
    public float offset;
    private float startTimeShooting;    //время до начала выстрела

    private int maxCountAmmo;           //Общее оставшееся количество патронов 
    private int ammoInMagazine;         //Количество патронов в магазине, которое должно быть в в магазине

    [SerializeField]
    private int currentCountAmmo;       //Общее количество патровов
    [SerializeField]
    private int currentAmmoInMagazine;  //Текущее количество патровов в магазине

    private void Start()
    {
        //Максимальное количество патронов
        maxCountAmmo = gameManager.GetComponent<ShootControl>().MaxCountAmmo;
        //Установка количество патронов в магазине из настроек
        ammoInMagazine = gameManager.GetComponent<ShootControl>().MaxCountAmmoInMagazine;

        currentCountAmmo = maxCountAmmo;
        currentAmmoInMagazine = ammoInMagazine;

        Debug.Log("currentCountAmmo : " + currentCountAmmo);
        Debug.Log("currentAmmoInMagazine : " + currentAmmoInMagazine);
    }

    void Update()
    {
        //Проверка времни до старта, если блольше 0, то обратный отсчет до начала стрельбы, иначе делаем выстрел 
        if (startTimeShooting <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //Проверка текущего количетва патронов в магазине, если больше 0, то делаем выстрел, иначе ввод сообщения об перезагрузке
                if(currentAmmoInMagazine > 0)
                {
                    //страт корутины стрельбы
                    StartCoroutine(DoShoot());
                    //старт перезарядки
                    startTimeShooting = delayTimeShooting;
                    //Декремент текущего количества патронов в магазине
                    currentAmmoInMagazine--;
                }
                else
                {
                    Debug.Log("NEED RELOAD");
                }
            }

            //Если нажата клавиша R и текущее количество патронов не равно максимальному количеству патронов в магазине, вызов метода перезарядки патронов
            if (Input.GetKeyDown(KeyCode.R) && currentAmmoInMagazine != ammoInMagazine)
            {
                Debug.Log("RELOAD");
                ReloadAmmo(currentAmmoInMagazine);
            }

        }  else startTimeShooting -= Time.deltaTime;  //Интервал между выстрелами
    }

    /// <summary>
    /// Реализация стрельбы 
    /// </summary>
    public IEnumerator DoShoot()
    {
        //создание объекта пули в точке появления пули и указание ей направления движения
        Instantiate(bullet, shootPos.position, transform.rotation).GetComponent<Bullet>();
        yield return null;
    }

    /// <summary>
    /// Перезарядка
    /// </summary>
    /// <param name="valueAmmoInMagazine">Оставшееся количество патронов в магазине</param>
    private void ReloadAmmo(int valueAmmoInMagazine)
    {
        //Проверка на наличие патронов, если больше 0, то перезарядка магазина
        if (currentCountAmmo > 0)
        {
            if (currentCountAmmo >= ammoInMagazine)
            {
                //установка текущего значения патронов в магазине равное количеству патронов, которое должно быть в в магазине
                currentAmmoInMagazine = ammoInMagazine;
                //вычитание из общего количества патронов количества, которое должно быть в в магазине
                currentCountAmmo -= currentAmmoInMagazine;
                //добавление в оставшемуся количесту патронов, количество патронов при перезарядке
                currentCountAmmo += valueAmmoInMagazine;
            }
            else
            {
                //установка текущего значения патронов в магазине равное общему количеству оставшихся патронов
                currentAmmoInMagazine += currentCountAmmo;
                //Сброс в 0 оставшегося количества патронов
                currentCountAmmo = 0;
                //Проверка на первышение количества патронов магазине;
                if (currentAmmoInMagazine > ammoInMagazine)
                {
                    //Перенос лишних патронов  из магазина в общее количество патронов
                    currentCountAmmo = currentAmmoInMagazine - ammoInMagazine;
                    //установка текущего значения патронов в магазине равное количеству патронов, которое должно быть в в магазине
                    currentAmmoInMagazine = ammoInMagazine;
                }
            }
        }
        else
        {
            //Установка текущего значения currentCountAmmo в 0
            currentCountAmmo = 0;
            Debug.Log("NEED BUY THE AMMO!!!");
        }
    }
}
