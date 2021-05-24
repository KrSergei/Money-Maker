using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;     //�������� ������
    public int health;
    public Animator anim;   //��������� �������� ������

    Rigidbody2D rb;         //�������� rigidbody ������
    public void Start()
    {
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
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
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
            Debug.Log("health - 1");
            health -= 1;
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
        if(health <= 0)
        {
            //Destroy(gameObject);
            Debug.Log("Game Over");
        }
    }
}
