using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;    //�������� ������
    public Animator anim;   //��������� �������� ������

    void Update()
    {
        //����������� ����������� �������� �� ����������� 
        float directionSide = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //����������� ����������� �������� �� ���������
        float directionForward = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        //������������ ������ � ����������� �� �������� directionSide, directionForward
        transform.Translate(new Vector2(directionSide, directionForward));

        //��������� �������� ��� ������ ������������ � ���������� ����� ���������
        if (directionSide != 0 || directionForward != 0)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
    }
}
