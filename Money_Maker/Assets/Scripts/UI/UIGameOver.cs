using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public Text gameOverText;       //����� ���������� ���������

    /// <summary>
    /// ����� ��������� �� �����
    /// </summary>
    /// <param name="textGameOver"></param>
    public void ShowingCurrentText(string textGameOver = "")
    {
        gameOverText.text = textGameOver;
    }
}
