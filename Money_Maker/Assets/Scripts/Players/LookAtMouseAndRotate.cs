using UnityEngine;

public class LookAtMouseAndRotate : MonoBehaviour
{
    public float offset = 5f;   //���� �������� ������

    private Vector3 currentMousePos;    //������� ������� ������� �����

    void Update()
    {
        //��������� �������� ������� ������ � ������� �����������
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //���������� ���� �������� �� ��� Z
        float rotateZ = Mathf.Atan2(currentMousePos.y, currentMousePos.x) * Mathf.Rad2Deg;
        //������� �� �������� ���� �� ��� Z � ����������� ���� ��������
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }

    /// <summary>
    /// ������� ������� ������� �����
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMousePos()
    {
        return currentMousePos;
    }
}
