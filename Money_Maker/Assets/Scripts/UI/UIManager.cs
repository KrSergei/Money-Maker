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

    public AudioSource startMenuSounadTrack; //������� ������� ��� ���������� ����

    public GameObject player; //��������� Player


    void Start()
    {
        //��������� �������� ���� ������ 0
        Time.timeScale = 0f;
        //���������� �������� ���������� Shoot � ������, ��� ���������� �������� �������� � ������� ����.
        player.gameObject.GetComponentInChildren<Shoot>().enabled = false;
        //��������� ��������� ���� 
        canvasMenu[0].gameObject.SetActive(true);
        //����������� �������� ����
        canvasMenu[1].gameObject.SetActive(false);
        //����������� ���� ����������
        canvasMenu[2].gameObject.SetActive(false);
        //����������� ���� ����������
        canvasMenu[3].gameObject.SetActive(false);
        //��������� ��������� ������
        startMenuSounadTrack.Play();

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


    /// <summary>
    /// ��������� ������� �� ������ �����
    /// </summary>
    public void BAStartButton()
    {
        //���������� ������� ������ � ��������� ����
        startMenuSounadTrack.Stop();
        //����������� ��������� ���� 
        canvasMenu[0].gameObject.SetActive(false);
        //��������� �������� ����
        canvasMenu[1].gameObject.SetActive(true);
        //��������� �������� ���� ������ 0
        Time.timeScale = 1f;
        //��������� �������� ���������� ��� ��������� ��������
        player.gameObject.GetComponentInChildren<Shoot>().enabled = true;
    }

    /// <summary>
    /// ��������� ������� �� ������ �����
    /// </summary>
    public void BAExitButton()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    /// <summary>
    /// ��������� ������� �� ������ �������
    /// </summary>
    public void BARestart()
    {
        SceneManager.LoadScene(0);
    }
}
