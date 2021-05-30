using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas[] canvasMenu; //������ ����

    public float timeShowingAnnounceMenu = 5f; //����� ������ ���� ����������

    // Start is called before the first frame update
    void Start()
    {
        //����������� ���� ����������
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
            //��������� ���� ����������
            canvasMenu[2].gameObject.SetActive(true);
            //������ ������ � ���������� UIAnnounce ��� ������ ������ �� ������, ������������ � ��������� textAnnounce
            GetComponentInChildren<UIAnnounce>().ShowingCurrentText(textAnnounce);
            //���������� ���� ���������� ����� ����� timeShowingAnnounceMenu
            Invoke("SetInActiveAnnounceMenu", timeShowingAnnounceMenu);
        }
    }

    public void SetInActiveAnnounceMenu()
    {
        canvasMenu[2].gameObject.SetActive(false);
    }

}
