using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;     //скорость игрока
    public int health;
    public Animator anim;   //компонент аниматор игрока

    Rigidbody2D rb;         // омпонет rigidbody игрока
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //определение направлени€ движени€ по горизонтали 
        float directionSide = Input.GetAxisRaw("Horizontal");
        //определение направлени€ движени€ по вертикали
        float directionForward = Input.GetAxisRaw("Vertical");

        //«апуск корутины передвижени€
        StartCoroutine(Movement(directionSide, directionForward));

        //¬ключение анимации при начале передвижени€ и отключение после остановки
        if (directionSide != 0 || directionForward != 0)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
    }

    /// <summary>
    /// ѕередвижение персонажа
    /// </summary>
    /// <param name="directionSide">значение по оси ’</param>
    /// <param name="directionForward">>значение по оси Y</param>
    /// <returns></returns>
    IEnumerator Movement(float directionSide, float directionForward)
    {
        //”становка скорости движени€ игрока
        rb.velocity = new Vector2(directionSide, directionForward) * speed * Time.deltaTime;
        yield return null;
    }

    /// <summary>
    /// ѕроверка на столкновение
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        //≈сли есть столкновение с коллайдером "Enemy", то отнимаетс€ один пункт здоровь€
        if (col.tag == "Enemy")
        {
            Debug.Log("health - 1");
            health -= 1;
            //вызов метода проверки оставшегос€ здоровь€
            CheckCurrenthealth();
        }
    }
    /// <summary>
    /// ѕроверка оставшегос€ здоровь€
    /// </summary>
    private void CheckCurrenthealth()
    {
        //≈сли здоровье меньше либо = 0, то вызов меню окончани€ игры
        if(health <= 0)
        {
            //Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }
}
