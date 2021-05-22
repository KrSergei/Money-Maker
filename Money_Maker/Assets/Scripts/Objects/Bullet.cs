using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;             //скорость полета пули
    public float lifeTime;          //время существования пули
    public float distance;          //дистанция на которой наносится урон твердому телу
    public int damage;              //урон от пули
    public LayerMask whatIsSolid;   //маска определения жесткого тела
    private Vector3 direction;      //Направление полета пули, инициализируется из скрипта Shoot, при создании объект Bullet

    void Update()
    {
        //Определение столкновения с жестким телом
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        //Если есть столкновение с коллайдером
        if(hitInfo.collider != null)
        {
            //Проверка коллайдера по тегу enemy
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                //Получение компонента Enemy и вызво метода TakeDamage с передачей в метод параметра damage
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            //Уничтожение пули
            Destroy(gameObject);
        }
        //Задание движение пуле
        transform.Translate(-Vector2.up * speed * Time.deltaTime);

        //Вызов метода уничтожения пули
        DestroyBullet();
    }

    /// <summary>
    /// Установка вектора движения, задается при создании объекта из скрипта Shoot
    /// </summary>
    /// <param name="direction">Вектор направления</param>
    public void SetDirection(Vector3 direction)
    {  
        this.direction = direction;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject, lifeTime);
    }
}
