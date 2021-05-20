using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed;    //�������� ������

    [SerializeField]
    private float offset;   //������� ������

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        //����������� ����������� �������� �� ����������� 
        float directionSide = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //����������� ����������� �������� �� ���������
        float directionForward = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        //������������ ������ � ����������� �� �������� directionSide, directionForward
        transform.Translate(new Vector2(directionSide, directionForward));

        //
        if (directionSide != 0 || directionForward != 0)
            anim.SetTrigger("Walking");
    }
}
