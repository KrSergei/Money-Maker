using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject gameObjectUIManager; //Объект UIManager
    public GameObject audioManager; //Объект AudioManager
    public GameObject gameManager; //Объект GameManager

    public AudioSource playerStep;
    private SoundsHandler soundHandler;

    public float speed;     //скорость игрока
    public int health;      //количество жизней
    public Animator anim;   //компонент аниматор игрока
    Rigidbody2D rb;         //Компонет rigidbody игрока

    private bool isWalking = false;    //Флаг, что персонаж двигается;

    private string messageForLose = "YOU LOSE"; //текстовое сообщение для проигрыша

    private void Awake()
    {
        soundHandler = GetComponent<SoundsHandler>();
    }

    public void Start()
    {
        //Получение компонента Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //определение направления движения по горизонтали 
        float directionSide = Input.GetAxisRaw("Horizontal");
        //определение направления движения по вертикали
        float directionForward = Input.GetAxisRaw("Vertical");

        //Запуск корутины передвижения
        StartCoroutine(Movement(directionSide, directionForward));

        //Включение анимации при начале передвижения и отключение после остановки
        if (directionSide != 0 || directionForward != 0)
        {
            anim.SetBool("Walking", true);
            //Проверка на передвижение игрока, если он не двигается, то включение звука передвижения
            if (!isWalking)
            {
                //Установка флага, учто игрок передвигается  = true
                isWalking = true;
                //Проигрывание звуков шагов
                soundHandler.PlaySound(playerStep);
            }
        }
        else
        {
            //Отключение триггера  "Walking" в Animator
            anim.SetBool("Walking", false);
            //Отключение проигрывания звуков шагов
            soundHandler.StopSound(playerStep);
            //Сброс флага
            isWalking = false;
        }
    }


    /// <summary>
    /// Передвижение персонажа
    /// </summary>
    /// <param name="directionSide">значение по оси Х</param>
    /// <param name="directionForward">>значение по оси Y</param>
    /// <returns></returns>
    IEnumerator Movement(float directionSide, float directionForward)
    {
        //Установка скорости движения игрока
        rb.velocity = new Vector2(directionSide, directionForward) * speed * Time.deltaTime;
        yield return null;
    }

    /// <summary>
    /// Проверка на столкновение
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Если есть столкновение с коллайдером "Enemy", то отнимается один пункт здоровья
        if (col.tag == "Enemy")
        {
            health--;
            //вызов метода проверки оставшегося здоровья
            CheckCurrenthealth();
        }
    }

    /// <summary>
    /// Проверка оставшегося здоровья
    /// </summary>
    private void CheckCurrenthealth()
    {
        //Если здоровье меньше либо = 0, то вызов меню окончания игры
        if (health <= 0)
        {
            //Отключение проигрывания звуков шагов
            soundHandler.StopSound(playerStep);
            //Выключение звуковых эффектов объекта Enemy(передача параметра что игрок проиграл)
            gameManager.GetComponent<SpawnEnemy>().DestoyAllEnemyAfterEnded();
            //Вызов метода для показа меню окончания игры с передачей ему сообщения о проигрыше
            gameObjectUIManager.GetComponent<UIManager>().ShowGameOverMenu(messageForLose);
            //Установка скорости игры в 0
            Time.timeScale = 0f;
        }
    }
}
