using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform bullet,        //��������� ����
                     shootPos;      //����� ��������� ����
    public float delayTimeShooting;     //�������� ����� ����������
    private float startTimeShooting;    //����� �� ������ ��������
    private LookAtMouseAndRotate currentMousePosition; //������� ������� ������� �����

    private void Start()
    {
        //��������� ���������� LookAtMouseAndRotate
        currentMousePosition = GetComponentInChildren<LookAtMouseAndRotate>();
    }

    void Update()
    {
        //�������� ������ �� ������, ���� ������� 0, �� �������� ������ �� ������ ��������, ����� ������ ������� 
        if(startTimeShooting <= 0)
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
        //����������� ����������� ��������  �� �������� �������������� ������� �������� ����� ��������� ����� ��������
        Vector3 directionBullet = GetDirectionShoot();

        //�������� ������� ���� � ����� ��������� ���� � �������� �� ����������� ��������
        Instantiate(bullet, shootPos.position, transform.rotation).GetComponent<Bullet>().SetDirection(GetDirectionShoot());

        yield return null;
    }

    /// <summary>
    /// ��������� ������� ������� ����� �� ������� LookAtMouseAndRotate
    /// </summary>
    /// <returns></returns>
    public Vector3 GetDirectionShoot()
    {
        return currentMousePosition.GetMousePos() - shootPos.position;
    }
}
