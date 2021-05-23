using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform  shootPos;      //Место появления пули
    public GameObject bullet;        //Объект пули

    public float delayTimeShooting;     //интервал между выстрелами
    public float offset;
    private float startTimeShooting;    //время до начала выстрела

    void Update()
    {
        //Проверка времни до старта, если блольше 0, то обратный отсчет до начала стрельбы, иначе делаем выстрел 
        if (startTimeShooting <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //страт корутины стрельбы
                StartCoroutine(Shooting());
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
        //создание объекта пули в точке появления пули и указание ей направления движения
        Instantiate(bullet, shootPos.position, transform.rotation).GetComponent<Bullet>();
        yield return null;
    }
}
