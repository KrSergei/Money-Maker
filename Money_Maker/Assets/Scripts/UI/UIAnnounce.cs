using UnityEngine;
using UnityEngine.UI;

public class UIAnnounce : MonoBehaviour
{
    public Text announceText;       //Текст выводимого сообщения

    void Start()
    {
        ShowingCurrentText();
    }

    /// <summary>
    /// Вывод сообщения на экран
    /// </summary>
    /// <param name="textAnnounce"></param>
    public void ShowingCurrentText(string textAnnounce = "")
    {
        announceText.text = textAnnounce;
    }
}
