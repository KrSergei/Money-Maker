using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /*
     * BA - ������� ����� ��������� ������ - ����� ������������ ������� �� ������� ������
     * */

    /// <summary>
    /// ������ 0 - ��������� ���� (Start Menu)
    /// ������ 1 - ������� ���� (Play Menu)
    /// ������ 2 - ���� ��������� (Announce Menu)
    /// ������ 3 - ���� ����� ����  (Game Over)
    /// </summary>
    public Canvas[] canvasMenu; //������ ����

    public float timeShowingAnnounceMenu = 1f; //����� ������ ���� ����������

    // Start is called before the first frame update
    void Start()
    {
        //��������� ��������� ���� 
        canvasMenu[0].gameObject.SetActive(true);
        //����������� �������� ����
        canvasMenu[1].gameObject.SetActive(false);
        //����������� ���� ����������
        canvasMenu[2].gameObject.SetActive(false);
        //����������� ���� ����������
        canvasMenu[3].gameObject.SetActive(false);
        //��������� �������� ���� ������ 0
        Time.timeScale = 0f;
    }

    public void ShowAnnounceMenu(string messageAnnounce)
    {
        if (!canvasMenu[2].gameObject.activeInHierarchy)
        {
            //��������� ���� ����������
            canvasMenu[2].gameObject.SetActive(true);
            //������ ������ � ���������� UIAnnounce ��� ������ ������ �� ������, ������������ � ��������� textAnnounce
            GetComponentInChildren<UIAnnounce>().ShowingCurrentText(messageAnnounce);
            //���������� ���� ���������� ����� ����� timeShowingAnnounceMenu
            Invoke("SetInActiveAnnounceMenu", timeShowingAnnounceMenu);
        }
    }

    public void SetInActiveAnnounceMenu()
    {
        canvasMenu[2].gameObject.SetActive(false);
    }

    public void ShowGameOverMenu(string messageGameOver)
    {
        //����������� �������� ����
        canvasMenu[2].gameObject.SetActive(true);
        //��������� ���� Game Over
        canvasMenu[3].gameObject.SetActive(true);
        //����� ��������� �� ��������� ����(��������� ��� ������)
        GetComponentInChildren<UIGameOver>().ShowingCurrentText(messageGameOver);
        
    }

    public void BAStartButton()
    {
        //����������� ��������� ���� 
        canvasMenu[0].gameObject.SetActive(false);
        //��������� �������� ����
        canvasMenu[1].gameObject.SetActive(true);
        //��������� �������� ���� ������ 0
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
