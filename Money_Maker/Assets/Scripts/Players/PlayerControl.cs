using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public GameObject gameObjectUIManager; //Объект gameManager

    private UIManager uiManager;

    public float speed;     //скорость игрока
    public int health;      //количество жизней
    public Animator anim;   //компонент аниматор игрока
    Rigidbody2D rb;         //Компонет rigidbody игрока

    private string messageForLose = "YOU LOSE"; //текстовое сообщение для проигрыша


    public void Start()
    {
        //Получение компонента Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //Получение компонента UIManager 
        uiManager = gameObjectUIManager.GetComponent<UIManager>();
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
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
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
            health --;
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
        if(health <= 0)
        {
            //Вызов метода для показа меню окончания игры с передачей ему сообщения о проигрыше
            uiManager.ShowGameOverMenu(messageForLose);
            //Установка скорости игры в 0
            Time.timeScale = 0f;
        }
    }
}
