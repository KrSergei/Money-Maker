using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;             //�������� ������ ����
    public float lifeTime;          //����� ������������� ����
    public float distance;          //��������� �� ������� ��������� ���� �������� ����
    public int damage;              //���� �� ����
    public LayerMask whatIsSolid;   //����� ����������� �������� ����
    private Vector3 direction;      //����������� ������ ����, ���������������� �� ������� Shoot, ��� �������� ������ Bullet

    void Update()
    {
        //����������� ������������ � ������� �����
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        //���� ���� ������������ � �����������
        if(hitInfo.collider != null)
        {
            //�������� ���������� �� ���� enemy
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                //��������� ���������� Enemy � ����� ������ TakeDamage � ��������� � ����� ��������� damage
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            //����������� ����
            Destroy(gameObject);
        }
        //������� �������� ����
        StartCoroutine(Shoot());

        //����� ������ ����������� ���� � ����� ������
        DestroyBullet();
    }

    /// <summary>
    /// ������� ����������� � ������������ ����
    /// </summary>
    /// <returns></returns>
    IEnumerator Shoot()
    {
        transform.Translate(-Vector2.up * speed * Time.deltaTime);
        yield return null;
    }

    /// <summary>
    /// ����������� ���� ����� ��������� ������� �����
    /// </summary>
    private void DestroyBullet()
    {

        Destroy(gameObject, lifeTime);
    }
}
