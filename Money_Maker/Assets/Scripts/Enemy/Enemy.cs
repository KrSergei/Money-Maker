using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float offset;            //угол поворота
    public float speed;             //скорость передвижени€
    public int health;              //текущее здоровье
    public float distanceAttack;    //ƒистанци€ на длину происходит атака
    public float speedAttack;       //—корость атаки
    public float timeRepeatAttack;  //¬рем€, через которое повтор€етс€ атака
    public Transform[] damageArea;  //ћассив позиций точек атаки
    public Animator anim;           //јниматор

    [SerializeField]
    private float maxSizeAttackArea;//ћаксимальный радиус атаки
    private Transform target;       //цель к которой должен двигатьс€

    private IEnumerator doAttack;   // орутина атаки

    private void Start()
    {
        //поиск цели - игрока
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //сброс области атаки
        SetAttackArea();
        //¬ызов метода дл€ проверки и запуска корутины doAttack
        CheckAndStartCoroutine();
    }

    void Update()
    {
        //¬ызов метода дл€ поворота в сторону цели
        RotationToTarget();

        //ƒвижение к цели, пока цель не умрет.
        StartCoroutine(MovingToTarget());
    }

    /// <summary>
    /// ѕередвижение к цели
    /// </summary>
    /// <returns></returns>
    IEnumerator MovingToTarget()
    {
        //ƒвижение к цели, пока рассто€ние больше, чем половина рассто€ни€ атаки 
        if (Vector2.Distance(transform.position, target.position) > distanceAttack * 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //¬ключение анимации передвижени€
            anim.SetBool("Walk", true);

        } else anim.SetBool("Walk", false);
        yield return null;
    }

    /// <summary>
    /// ћетод дл€ поворота в сторону цели
    /// </summary>
    private void RotationToTarget()
    {
        //ѕолучение значени€ положени€ цели
        Vector2 currentTargetPos = target.position - transform.position;
        //¬ычисление угла поворота по оси Z
        float rotateZ = Mathf.Atan2(currentTargetPos.y, currentTargetPos.x) * Mathf.Rad2Deg;
        //ѕоворот к цели на заданный угол по оси Z с добавлением угла поворота
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }

    /// <summary>
    /// вычисление оставшегос€ количеств жизни после полученного урона
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //¬ычитание из текущего количества жизней показател€ damage
        health -= damage;
        //¬ызвов метода проверки количетсва жизней
        CheckCurrentHealth();
    }

    /// <summary>
    /// ѕроверка текущего количества жизней, если меньше либо равно 0, то объект уничтожаетс€
    /// </summary>
    private void CheckCurrentHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CheckAndStartCoroutine()
    {
        //ѕроверка корутины doAttack на значени€, если она null,
        if (doAttack == null)
        {
            //»нициализаци€ корутины doAttack
            doAttack = DoRadiusAttack();
            //запуск корутины doAttack
            StartCoroutine(doAttack);
        }
    }

    /// <summary>
    /// —овершение атаки
    /// </summary>
    /// <returns></returns>
    IEnumerator DoRadiusAttack()
    {
        //сброс области атаки в 0
        SetAttackArea();
        //врем€ с начала аткаи
        float startTime = 0;
        //скорость изменени€ размеров области атаки
        float speedChangeArea = maxSizeAttackArea / speedAttack;

        //ѕока текущее врем€ атаки меньше, чем скорость атаки
        while (startTime <= speedAttack)
        {
            //измение текущего времени атаки
            startTime += Time.deltaTime;
            //изменение облаcти атаки
            for (int i = 0; i < damageArea.Length; i++)
            {
                damageArea[i].localScale = new Vector2(damageArea[i].localScale.x + Time.deltaTime * speedChangeArea,
                                            damageArea[i].localScale.y + Time.deltaTime * speedChangeArea);
            }
            yield return null;
        }

        //≈сли врем€, прошедшее с начала атаки больше либо расно времени атаки, то перезапуск корутины
        if (startTime >= speedAttack)
        {
            //сброс области атаки в 0
            SetAttackArea();
            //задержка перед вызовом корутины атаки
            yield return new WaitForSeconds(timeRepeatAttack);
            //презапуск
            StartCoroutine(DoRadiusAttack());
        }
    }

    /// <summary>
    /// утсановка области атаки в 0 дл€ всех точек атаки
    /// </summary>
    private void SetAttackArea()
    {
        for (int i = 0; i < damageArea.Length; i++)
        {
            damageArea[i].localScale = new Vector2(0f, 0f);
        }
    }
}
