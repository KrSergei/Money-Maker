using System.Collections;
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
        StartCoroutine(Shoot());

        //Вызов метода уничтожения пули в любом случае
        DestroyBullet();
    }

    /// <summary>
    /// Задание направления и передвижение пули
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        transform.Translate(-Vector2.up * speed * Time.deltaTime);
        yield return null;
    }

    /// <summary>
    /// Уничтожение пули после окончания времени жизни
    /// </summary>
    private void DestroyBullet()
    {

        Destroy(gameObject, lifeTime);
    }
}
