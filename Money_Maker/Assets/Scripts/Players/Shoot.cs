using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject gameManager;      //Объект управления игрой
    public GameObject UIManager;        //Объект управления меню
    public GameObject bullet;           //Объект пули
    public Transform  shootPos;         //Место появления пули



    public float delayTimeShooting;     //интервал между выстрелами

    private ShopAmmo shopAmmo;

    private float startTimeShooting;    //время до начала выстрела

    private int countAmmo;              //Запас патронов 
    private int ammoInMagazine;         //Количество патронов в магазине

    [SerializeField]
    private int currentCountAmmo;       //Запас патронов
    [SerializeField]
    private int currentAmmoInMagazine;  //Текущее количество патровов в магазине

    private string messageReload = "RELOAD";                //Сообщение выводимое на экран при необходимости перезарядки
    private string messageBuyAmmo = "NEED BUY THE AMMO!!!"; //Сообщение выводимое на экран при необходимости покупки боеприпасов


    public int CurrentCountAmmo { get => currentCountAmmo; set => currentCountAmmo = value; }
    public int CurrentAmmoInMagazine { get => currentAmmoInMagazine; set => currentAmmoInMagazine = value; }

    private void Start()
    {
        //Максимальный запас патронов
        countAmmo = gameManager.GetComponent<ShootControl>().MaxCountAmmo;
        //Установка количество патронов в магазине из настроек
        ammoInMagazine = gameManager.GetComponent<ShootControl>().MaxCountAmmoInMagazine;

        shopAmmo = gameManager.GetComponent<ShopAmmo>();

        CurrentCountAmmo = countAmmo;
        CurrentAmmoInMagazine = ammoInMagazine;
    }

    void Update()
    {
        //Проверка времени до старта стрельбы, если блольше 0, то обратный отсчет до начала стрельбы, иначе делаем выстрел 
        if (startTimeShooting <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //Проверка текущего количетва патронов в магазине, если больше 0, то делаем выстрел, иначе ввод сообщения об перезагрузке
                if(CurrentAmmoInMagazine > 0)
                {
                    //страт корутины стрельбы
                    StartCoroutine(DoShoot());
                    //старт перезарядки
                    startTimeShooting = delayTimeShooting;
                    //Декремент текущего количества патронов в магазине
                    CurrentAmmoInMagazine--;
                }
                else
                {
                    //Вызвов метода для показа объявления об перезарядке с передачей текста сообщения
                    UIManager.GetComponent<UIManager>().ShowAnnounce(messageReload);
                }
            }

            //Если нажата клавиша R и текущее количество патронов не равно максимальному количеству патронов в магазине, вызов метода перезарядки патронов
            if (Input.GetKeyDown(KeyCode.R) && CurrentAmmoInMagazine != ammoInMagazine)
            {
                ReloadAmmo(CurrentAmmoInMagazine);
            }

        }  else startTimeShooting -= Time.deltaTime;  //Интервал между выстрелами

        if (Input.GetMouseButtonDown(1))
        {
            shopAmmo.TryBuyAmmo();
        }
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
        if (CurrentCountAmmo > 0)
        {
            if (CurrentCountAmmo >= ammoInMagazine)
            {
                //установка текущего значения патронов в магазине равное количеству патронов, которое должно быть в в магазине
                CurrentAmmoInMagazine = ammoInMagazine;
                //вычитание из общего количества патронов количества, которое должно быть в в магазине
                CurrentCountAmmo -= CurrentAmmoInMagazine;
                //добавление в оставшемуся количесту патронов, количество патронов при перезарядке
                CurrentCountAmmo += valueAmmoInMagazine;
            }
            else
            {
                //установка текущего значения патронов в магазине равное общему количеству оставшихся патронов
                CurrentAmmoInMagazine += CurrentCountAmmo;
                //Сброс в 0 оставшегося количества патронов
                CurrentCountAmmo = 0;
                //Проверка на первышение количества патронов магазине;
                if (CurrentAmmoInMagazine > ammoInMagazine)
                {
                    //Перенос лишних патронов  из магазина в общее количество патронов
                    CurrentCountAmmo = CurrentAmmoInMagazine - ammoInMagazine;
                    //установка текущего значения патронов в магазине равное количеству патронов, которое должно быть в в магазине
                    CurrentAmmoInMagazine = ammoInMagazine;
                }
            }
        }
        else
        {
            //Установка текущего значения currentCountAmmo в 0
            CurrentCountAmmo = 0;
            //Вызвов метода для показа объявления об необходимости покупки беоприпасов с передачей текста сообщения
            UIManager.GetComponent<UIManager>().ShowAnnounce(messageBuyAmmo);
        }
    }
}
