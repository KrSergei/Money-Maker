using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform  shootPos;      //����� ��������� ����
    public GameObject bullet;        //������ ����

    public float delayTimeShooting;     //�������� ����� ����������
    public float offset;
    private float startTimeShooting;    //����� �� ������ ��������

    void Update()
    {
        //�������� ������ �� ������, ���� ������� 0, �� �������� ������ �� ������ ��������, ����� ������ ������� 
        if (startTimeShooting <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //����� �������� ��������
                StartCoroutine(Shooting());
                //����� �����������
                startTimeShooting = delayTimeShooting;
            }
        }  else startTimeShooting -= Time.deltaTime;  //�������� ����� ����������
    }

    /// <summary>
    /// ���������� �������� 
    /// </summary>
    public IEnumerator Shooting()
    {
        //�������� ������� ���� � ����� ��������� ���� � �������� �� ����������� ��������
        Instantiate(bullet, shootPos.position, transform.rotation).GetComponent<Bullet>();
        yield return null;
    }
}
