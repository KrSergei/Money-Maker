using UnityEngine;

public class LookAtMouseAndRotate : MonoBehaviour
{
    public float offset = 5f;   //”гол поворота игрока

    private Vector3 currentMousePos;    //“екущее положение курсора мышки в мировых кординатах

    void Update()
    {
        //ѕолучение значени€ курсора экрана в мировых координатах
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //¬ычисление угла поворота по оси Z
        float rotateZ = Mathf.Atan2(currentMousePos.y, currentMousePos.x) * Mathf.Rad2Deg;
        //ѕоворот на заданный угол по оси Z с добавлением укла поворота
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
    }

    /// <summary>
    /// возврат текущего место положени€ курсора мышки
    /// </summary>
    /// <returns></returns>
    public  Vector3 GetMousePos()
    {
        return currentMousePos;
    }                                                                                                                    
}
