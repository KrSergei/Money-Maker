using UnityEngine;

public class LookAtMouseAndRotate : MonoBehaviour
{
    public float offset = 5f;   //���� �������� ������

    void Update()
    {
        //��������� �������� ������� ������ � ������� �����������
        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //���������� ���� �������� �� ��� Z
        float rotateZ = Mathf.Atan2(currentMousePos.y, currentMousePos.x) * Mathf.Rad2Deg;
        //������� �� �������� ���� �� ��� Z � ����������� ���� ��������
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }
}
