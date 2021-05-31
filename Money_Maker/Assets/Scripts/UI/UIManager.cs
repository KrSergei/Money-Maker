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

    // Start is called before the first frame update
    void Start()
    {
        //Активация стртового меню 
        canvasMenu[0].gameObject.SetActive(true);
        //Деактивация игрового меню
        canvasMenu[1].gameObject.SetActive(false);
        //Деактивация меню объявления
        canvasMenu[2].gameObject.SetActive(false);
        //Деактивация меню объявления
        canvasMenu[3].gameObject.SetActive(false);
        //Установка скорости игры равная 0
        Time.timeScale = 0f;
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

    public void BAStartButton()
    {
        //Деактивация стртового меню 
        canvasMenu[0].gameObject.SetActive(false);
        //Активация игрового меню
        canvasMenu[1].gameObject.SetActive(true);
        //Установка скорости игры равная 0
        Time.timeScale = 1f;
    }

    public void BAExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void BARestart()
    {
        SceneManager.LoadScene(0);
    }
}
