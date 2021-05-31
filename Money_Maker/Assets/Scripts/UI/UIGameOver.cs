using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public Text gameOverText;       //Текст выводимого сообщения

    /// <summary>
    /// Вывод сообщения на экран
    /// </summary>
    /// <param name="textGameOver"></param>
    public void ShowingCurrentText(string textGameOver = "")
    {
        gameOverText.text = textGameOver;
    }
}
