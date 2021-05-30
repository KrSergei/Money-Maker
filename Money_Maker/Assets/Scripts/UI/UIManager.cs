using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas[] canvasMenu; //Массив меню

    public float timeShowingAnnounceMenu = 5f; //Время показа меню объявления

    // Start is called before the first frame update
    void Start()
    {
        //Деактивация меню объявления
        canvasMenu[2].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAnnounce(string textAnnounce)
    {
        if (!canvasMenu[2].gameObject.activeInHierarchy)
        {
            //Активация меню объявления
            canvasMenu[2].gameObject.SetActive(true);
            //Вызвов метода в компоненте UIAnnounce для показа текста на экране, принимаемого в параметре textAnnounce
            GetComponentInChildren<UIAnnounce>().ShowingCurrentText(textAnnounce);
            //Отключение меню объявления через время timeShowingAnnounceMenu
            Invoke("SetInActiveAnnounceMenu", timeShowingAnnounceMenu);
        }
    }

    public void SetInActiveAnnounceMenu()
    {
        canvasMenu[2].gameObject.SetActive(false);
    }

}
