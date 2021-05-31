using UnityEngine;
using UnityEngine.UI;

public class UIAnnounce : MonoBehaviour
{
    public Text announceText;       //����� ���������� ���������

    void Start()
    {
        ShowingCurrentText();
    }

    /// <summary>
    /// ����� ��������� �� �����
    /// </summary>
    /// <param name="textAnnounce"></param>
    public void ShowingCurrentText(string textAnnounce = "")
    {
        announceText.text = textAnnounce;
    }
}
