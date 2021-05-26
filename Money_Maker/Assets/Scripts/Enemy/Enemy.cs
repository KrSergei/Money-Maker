using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float offset;            //угол поворота
    public float speed;             //скорость передвижения
    public int health;              //текущее здоровье
    public float minDistanceToTarget;  //Минимальная дистанция до цели 
    public float speedAttack;       //Скорость атаки
    public float timeRepeatAttack;  //Время, через которое повторяется атака
    public int pointPrice;          //Стоимость за учничтожение объекта


    public Transform[] damageArea;  //Массив позиций точек атаки
    public Animator anim;           //Аниматор

    [SerializeField]
    private float maxSizeAttackArea;//Максимальный радиус атаки
    private Transform target;       //цель к которой должен двигаться

    private CalculateValues calculatePoint; //Объект, в который передается  pointPrice за уничтожение объекта Enemy
    private IEnumerator doAttack;   //Корутина атаки

    private void Start()
    {
        //поиск цели - игрока
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //получение компонента calculatePoint
        calculatePoint = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CalculateValues>();
        //сброс области атаки
        SetAttackArea();
        //Вызов метода для проверки и запуска корутины doAttack
        CheckAndStartCoroutine();
    }

    void Update()
    {
        //Вызов метода для поворота в сторону цели
        RotationToTarget();

        //Движение к цели, пока цель не умрет.
        StartCoroutine(MovingToTarget());
    }

    /// <summary>
    /// Передвижение к цели
    /// </summary>
    /// <returns></returns>
    IEnumerator MovingToTarget()
    {
        //Если расстояние до цели больше чем distanceAttack
        if (Vector2.Distance(transform.position, target.position) > minDistanceToTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //Включение анимации передвижения
            anim.SetBool("Walk", true);

        } else anim.SetBool("Walk", false);
        yield return null;
    }

    /// <summary>
    /// Метод для поворота в сторону цели
    /// </summary>
    private void RotationToTarget()
    {
        //Получение значения положения цели
        Vector2 currentTargetPos = target.position - transform.position;
        //Вычисление угла поворота по оси Z
        float rotateZ = Mathf.Atan2(currentTargetPos.y, currentTargetPos.x) * Mathf.Rad2Deg;
        //Поворот к цели на заданный угол по оси Z с добавлением угла поворота
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }

    /// <summary>
    /// вычисление оставшегося количеств жизни после полученного урона
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //Вычитание из текущего количества жизней показателя damage
        health -= damage;
        //Вызвов метода проверки количетсва жизней
        CheckCurrentHealth();
    }

    /// <summary>
    /// Проверка текущего количества жизней, если меньше либо равно 0, то объект уничтожается
    /// </summary>
    private void CheckCurrentHealth()
    {
        if (health <= 0)
        {
            //Инкремент количества убитых врагов
            calculatePoint.SetPoint(pointPrice);
            //уничтожение объекта
            Destroy(gameObject);
        }
    }

    private void CheckAndStartCoroutine()
    {
        //Проверка корутины doAttack на значения, если она null,
        if (doAttack == null)
        {
            //Инициализация корутины doAttack
            doAttack = DoAttack();
            //запуск корутины doAttack
            StartCoroutine(doAttack);
        }
    }

    /// <summary>
    /// Совершение атаки
    /// </summary>
    /// <returns></returns>
    IEnumerator DoAttack()
    {
        //сброс области атаки в 0
        SetAttackArea();
        //время с начала аткаи
        float startTime = 0;
        //скорость изменения размеров области атаки
        float speedChangeArea = maxSizeAttackArea / speedAttack;

        //Пока текущее время атаки меньше, чем скорость атаки
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

        //Если время, прошедшее с начала атаки больше либо расно времени атаки, то перезапуск корутины
        if (startTime >= speedAttack)
        {
            //сброс области атаки в 0
            SetAttackArea();
            //задержка перед вызовом корутины атаки
            yield return new WaitForSeconds(timeRepeatAttack);
            //презапуск
            StartCoroutine(DoAttack());
        }
    }

    /// <summary>
    /// утсановка области атаки в 0 для всех точек атаки
    /// </summary>
    private void SetAttackArea()
    {
        for (int i = 0; i < damageArea.Length; i++)
        {
            damageArea[i].localScale = new Vector2(0f, 0f);
        }
    }
}
