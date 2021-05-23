using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float offset;            //���� ��������
    public float speed;             //�������� ������������
    public int health;              //������� ��������
    public float distanceAttack;    //��������� �� ����� ���������� �����
    public float speedAttack;       //�������� �����
    public float timeRepeatAttack;  //�����, ����� ������� ����������� �����
    public Transform[] damageArea;  //������ ������� ����� �����
    public Animator anim;           //��������

    [SerializeField]
    private float maxSizeAttackArea;//������������ ������ �����
    private Transform target;       //���� � ������� ������ ���������

    private IEnumerator doAttack;   //�������� �����

    private void Start()
    {
        //����� ���� - ������
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //����� ������� �����
        SetAttackArea();
        //����� ������ ��� �������� � ������� �������� doAttack
        CheckAndStartCoroutine();
    }

    void Update()
    {
        //����� ������ ��� �������� � ������� ����
        RotationToTarget();

        //�������� � ����, ���� ���� �� �����.
        StartCoroutine(MovingToTarget());
    }

    /// <summary>
    /// ������������ � ����
    /// </summary>
    /// <returns></returns>
    IEnumerator MovingToTarget()
    {
        //�������� � ����, ���� ���������� ������, ��� �������� ���������� ����� 
        if (Vector2.Distance(transform.position, target.position) > distanceAttack * 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //��������� �������� ������������
            anim.SetBool("Walk", true);

        } else anim.SetBool("Walk", false);
        yield return null;
    }

    /// <summary>
    /// ����� ��� �������� � ������� ����
    /// </summary>
    private void RotationToTarget()
    {
        //��������� �������� ��������� ����
        Vector2 currentTargetPos = target.position - transform.position;
        //���������� ���� �������� �� ��� Z
        float rotateZ = Mathf.Atan2(currentTargetPos.y, currentTargetPos.x) * Mathf.Rad2Deg;
        //������� � ���� �� �������� ���� �� ��� Z � ����������� ���� ��������
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }

    /// <summary>
    /// ���������� ����������� ��������� ����� ����� ����������� �����
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        //��������� �� �������� ���������� ������ ���������� damage
        health -= damage;
        //������ ������ �������� ���������� ������
        CheckCurrentHealth();
    }

    /// <summary>
    /// �������� �������� ���������� ������, ���� ������ ���� ����� 0, �� ������ ������������
    /// </summary>
    private void CheckCurrentHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void CheckAndStartCoroutine()
    {
        //�������� �������� doAttack �� ��������, ���� ��� null,
        if (doAttack == null)
        {
            //������������� �������� doAttack
            doAttack = DoRadiusAttack();
            //������ �������� doAttack
            StartCoroutine(doAttack);
        }
    }

    /// <summary>
    /// ���������� �����
    /// </summary>
    /// <returns></returns>
    IEnumerator DoRadiusAttack()
    {
        //����� ������� ����� � 0
        SetAttackArea();
        //����� � ������ �����
        float startTime = 0;
        //�������� ��������� �������� ������� �����
        float speedChangeArea = maxSizeAttackArea / speedAttack;

        //���� ������� ����� ����� ������, ��� �������� �����
        while (startTime <= speedAttack)
        {
            //������� �������� ������� �����
            startTime += Time.deltaTime;
            //��������� ����c�� �����
            for (int i = 0; i < damageArea.Length; i++)
            {
                damageArea[i].localScale = new Vector2(damageArea[i].localScale.x + Time.deltaTime * speedChangeArea,
                                            damageArea[i].localScale.y + Time.deltaTime * speedChangeArea);
            }
            yield return null;
        }

        //���� �����, ��������� � ������ ����� ������ ���� ����� ������� �����, �� ���������� ��������
        if (startTime >= speedAttack)
        {
            //����� ������� ����� � 0
            SetAttackArea();
            //�������� ����� ������� �������� �����
            yield return new WaitForSeconds(timeRepeatAttack);
            //���������
            StartCoroutine(DoRadiusAttack());
        }
    }

    /// <summary>
    /// ��������� ������� ����� � 0 ��� ���� ����� �����
    /// </summary>
    private void SetAttackArea()
    {
        for (int i = 0; i < damageArea.Length; i++)
        {
            damageArea[i].localScale = new Vector2(0f, 0f);
        }
    }
}
