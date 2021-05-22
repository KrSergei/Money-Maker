using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform  shootPos;      //Место появления пули
    public GameObject bullet;        //Объект пули

    public float delayTimeShooting;     //интервал между выстрелами
    public float offset;

    private float startTimeShooting;    //время до начала выстрела
    private LookAtMouseAndRotate currentMousePosition; //Текущая позиция курсора мышки

    private void Start()
    {
        //Получение компонента LookAtMouseAndRotate
        currentMousePosition = GetComponentInChildren<LookAtMouseAndRotate>();
    }

    void Update()
    {
        //Проверка времни до старта, если блольше 0, то обратный отсчет до начала стрельбы, иначе делаем выстрел 
        if (startTimeShooting <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //страт корутины стрельбы
                //StartCoroutine(Shooting());
                ////определение направления стрельбы  из текущего местоположения курсора вычитаем место положения точки стрельбы
                Vector3 directionBullet = GetDirectionShoot() - shootPos.position;
                //создание объекта пули в точке появления пули и указание ей направления движения
                Instantiate(bullet, shootPos.position, transform.rotation).GetComponent<Bullet>().SetDirection(directionBullet);
                //Instantiate(bullet, shootPos.position, transform.rotation);
                //старт перезарядки
                startTimeShooting = delayTimeShooting;
            }
        }  else startTimeShooting -= Time.deltaTime;  //Интервал между выстрелами
    }

    /// <summary>
    /// Реализация стрельбы 
    /// </summary>
    public IEnumerator Shooting()
    {
        //определение направления стрельбы  из текущего местоположения курсора вычитаем место положения точки стрельбы
        Vector3 directionBullet = GetDirectionShoot() - shootPos.position;

        //создание объекта пули в точке появления пули и указание ей направления движения
        Instantiate(bullet, shootPos.position, transform.rotation).GetComponent<Bullet>().SetDirection(directionBullet);

        yield return null;
    }

    /// <summary>
    /// Получение текущей позиции мышки из скрипта LookAtMouseAndRotate
    /// </summary>
    /// <returns></returns>
    public Vector3 GetDirectionShoot()
    {
        //return currentMousePosition.GetMousePos() - shootPos.position;
        return currentMousePosition.GetMousePos();
    }
}
