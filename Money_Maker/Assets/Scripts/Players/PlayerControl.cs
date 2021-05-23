using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;    //скорость игрока
    public Animator anim;   //компонент аниматор игрока

    void Update()
    {
        //определение направления движения по горизонтали 
        float directionSide = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        //определение направления движения по вертикали
        float directionForward = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        //передвижение игрока в зависимости от значений directionSide, directionForward
        transform.Translate(new Vector2(directionSide, directionForward));

        //Включение анимации при начале передвижения и отключение после остановки
        if (directionSide != 0 || directionForward != 0)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
    }
}
