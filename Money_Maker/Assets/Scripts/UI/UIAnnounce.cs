using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnnounce : MonoBehaviour
{
    public Text announceText;       //Текс выводимого сообщения

    // Start is called before the first frame update
    void Start()
    {
        ShowingCurrentText();
    }

    public void ShowingCurrentText(string textAnnounce = "")
    {
        announceText.text = textAnnounce;
    }
}
