using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed;    //скорость игрока

    [SerializeField]
    private float offset;   //поворот игрока

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

        //определение направления движения по горизонтали 
        float directionSide = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //определение направления движения по вертикали
        float directionForward = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        //передвижение игрока в зависимости от значений directionSide, directionForward
        transform.Translate(new Vector2(directionSide, directionForward));

        //
        if (directionSide != 0 || directionForward != 0)
            anim.SetTrigger("Walking");
    }
}
