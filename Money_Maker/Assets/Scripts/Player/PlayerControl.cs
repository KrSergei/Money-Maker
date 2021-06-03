using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject gameObjectUIManager; //������ UIManager
    public GameObject audioManager; //������ AudioManager
    public GameObject gameManager; //������ GameManager

    public AudioSource playerStep;
    private SoundsHandler soundHandler;

    public float speed;     //�������� ������
    public int health;      //���������� ������
    public Animator anim;   //��������� �������� ������
    Rigidbody2D rb;         //�������� rigidbody ������

    private bool isWalking = false;    //����, ��� �������� ���������;

    private string messageForLose = "YOU LOSE"; //��������� ��������� ��� ���������

    private void Awake()
    {
        soundHandler = GetComponent<SoundsHandler>();
    }

    public void Start()
    {
        //��������� ���������� Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //����������� ����������� �������� �� ����������� 
        float directionSide = Input.GetAxisRaw("Horizontal");
        //����������� ����������� �������� �� ���������
        float directionForward = Input.GetAxisRaw("Vertical");

        //������ �������� ������������
        StartCoroutine(Movement(directionSide, directionForward));

        //��������� �������� ��� ������ ������������ � ���������� ����� ���������
        if (directionSide != 0 || directionForward != 0)
        {
            anim.SetBool("Walking", true);
            //�������� �� ������������ ������, ���� �� �� ���������, �� ��������� ����� ������������
            if (!isWalking)
            {
                //��������� �����, ���� ����� �������������  = true
                isWalking = true;
                //������������ ������ �����
                soundHandler.PlaySound(playerStep);
            }
        }
        else
        {
            //���������� ��������  "Walking" � Animator
            anim.SetBool("Walking", false);
            //���������� ������������ ������ �����
            soundHandler.StopSound(playerStep);
            //����� �����
            isWalking = false;
        }
    }


    /// <summary>
    /// ������������ ���������
    /// </summary>
    /// <param name="directionSide">�������� �� ��� �</param>
    /// <param name="directionForward">>�������� �� ��� Y</param>
    /// <returns></returns>
    IEnumerator Movement(float directionSide, float directionForward)
    {
        //��������� �������� �������� ������
        rb.velocity = new Vector2(directionSide, directionForward) * speed * Time.deltaTime;
        yield return null;
    }

    /// <summary>
    /// �������� �� ������������
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        //���� ���� ������������ � ����������� "Enemy", �� ���������� ���� ����� ��������
        if (col.tag == "Enemy")
        {
            health--;
            //����� ������ �������� ����������� ��������
            CheckCurrenthealth();
        }
    }

    /// <summary>
    /// �������� ����������� ��������
    /// </summary>
    private void CheckCurrenthealth()
    {
        //���� �������� ������ ���� = 0, �� ����� ���� ��������� ����
        if (health <= 0)
        {
            //���������� ������������ ������ �����
            soundHandler.StopSound(playerStep);
            //���������� �������� �������� ������� Enemy(�������� ��������� ��� ����� ��������)
            gameManager.GetComponent<SpawnEnemy>().DestoyAllEnemyAfterEnded();
            //����� ������ ��� ������ ���� ��������� ���� � ��������� ��� ��������� � ���������
            gameObjectUIManager.GetComponent<UIManager>().ShowGameOverMenu(messageForLose);
            //��������� �������� ���� � 0
            Time.timeScale = 0f;
        }
    }
}
