using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objToFollow;       //������� �������� �������
    private Vector3 deltaPosition;      //���������� ����� ���������

    void Start()
    {
        //������ ���������� ����� ��������� - ������� ������� ������� ����� ������� �������� �������
        deltaPosition = transform.position - objToFollow.position;
    }

    //��������� ������� ������� - ������� ������� ������� ���� ���������� ����� ���������
    void Update()
    {
        transform.position = objToFollow.position + deltaPosition;
    }
}
