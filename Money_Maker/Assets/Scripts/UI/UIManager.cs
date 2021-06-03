using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /*
     * BA - префикс перед названием метода - метод обрабатывает реакцию на нажатие кнопки
     * */

    /// <summary>
    /// Индекс 0 - стартовое меню (Start Menu)
    /// Индекс 1 - игровое меню (Play Menu)
    /// Индекс 2 - меню сообщений (Announce Menu)
    /// Индекс 3 - меню конца игры  (Game Over)
    /// </summary>
    public Canvas[] canvasMenu; //Массив меню

    public float timeShowingAnnounceMenu = 1f; //Время показа меню объявления

    public AudioSource startMenuSounadTrack; //Фоновая дорожка для стартового меню

    public GameObject player; //Компонент Player


    void Start()
    {
        //Установка скорости игры равная 0
        Time.timeScale = 0f;
        //Отключение игрового компонента Shoot у игрока, для отключения эффектов стрельбы в главном меню.
        player.gameObject.GetComponentInChildren<Shoot>().enabled = false;
        //Активация стртового меню 
        canvasMenu[0].gameObject.SetActive(true);
        //Деактивация игрового меню
        canvasMenu[1].gameObject.SetActive(false);
        //Деактивация меню объявления
        canvasMenu[2].gameObject.SetActive(false);
        //Деактивация меню объявления
        canvasMenu[3].gameObject.SetActive(false);
        //Включение стартовой музыки
        startMenuSounadTrack.Play();

    }

    public void ShowAnnounceMenu(string messageAnnounce)
    {
        if (!canvasMenu[2].gameObject.activeInHierarchy)
        {
            //Активация меню объявления
            canvasMenu[2].gameObject.SetActive(true);
            //Вызвов метода в компоненте UIAnnounce для показа текста на экране, принимаемого в параметре textAnnounce
            GetComponentInChildren<UIAnnounce>().ShowingCurrentText(messageAnnounce);
            //Отключение меню объявления через время timeShowingAnnounceMenu
            Invoke("SetInActiveAnnounceMenu", timeShowingAnnounceMenu);
        }
    }

    public void SetInActiveAnnounceMenu()
    {
        canvasMenu[2].gameObject.SetActive(false);
    }

    public void ShowGameOverMenu(string messageGameOver)
    {
        //Деактивация игрового меню
        canvasMenu[2].gameObject.SetActive(true);
        //Активация меню Game Over
        canvasMenu[3].gameObject.SetActive(true);
        //Вывод сообщения об окончании игры(проигрыша или победы)
        GetComponentInChildren<UIGameOver>().ShowingCurrentText(messageGameOver);
        
    }


    /// <summary>
    /// Обработка нажатия на кнопку старт
    /// </summary>
    public void BAStartButton()
    {
        //Отключение фоновой музыки в стартовом меню
        startMenuSounadTrack.Stop();
        //Деактивация стртового меню 
        canvasMenu[0].gameObject.SetActive(false);
        //Активация игрового меню
        canvasMenu[1].gameObject.SetActive(true);
        //Установка скорости игры равная 0
        Time.timeScale = 1f;
        //Включение игрового компонента для активации стрельбы
        player.gameObject.GetComponentInChildren<Shoot>().enabled = true;
    }

    /// <summary>
    /// Обратотка нажатия на кнопку выход
    /// </summary>
    public void BAExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    /// <summary>
    /// Обработка нажатия на кнопку рестарт
    /// </summary>
    public void BARestart()
    {
        SceneManager.LoadScene(0);
    }
}
