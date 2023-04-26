using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{

    public enum PROJECTILETYPE      // enum���� Ÿ�� ����
    {
        PLAYER,
        MONSTER
    }
    public Vector3 launchDirection; // �߻� ����
    public PROJECTILETYPE projectileType;

    private void FixedUpdate()
    {
        float moveAmount = 3 * Time.fixedDeltaTime; // �̵� �ӵ� ����
        transform.Translate(launchDirection * moveAmount); // Translate �� �̵�
    }

    // ctrl+k+o, ctrl+k+c // �浹�� �Ͼ�� ���..
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    if (collision.gameObject.tag == "Object") // Tag ���� ������Ʈ �� ���
    //    {
    //        Destroy(this.gameObject); // �� �߻�ü�� ��������.
    //    }
    //    if (collision.gameObject.tag == "Monster") // Tag ���� Monster �� ���
    //    {
    //        Destroy(this.gameObject); // �� �߻�ü�� ��������.
    //        collision.gameObject.GetComponent<Monster>().Damaged(1);
    //    }
    //}

    private void OnTriggerEnter(Collider other)     // Ʈ���� ���� �Լ�
    {
        if(other.CompareTag("Monster") && projectileType == PROJECTILETYPE.PLAYER)  // Tag�� �˻��Ѵ�.
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<Monster>().Damaged(1);
            // �Լ� ȣ���� ���� ã�ƾ��� (���� �Ǿ����� ����) -> FindGameObjectWithTag ; �±׷� ã�Ƽ� ����
            GameObject Temp = GameObject.FindGameObjectWithTag("GameManager");
            Temp.GetComponent<HUDTextManager>().UpdateHUDTextSet("1", other.gameObject, new Vector3(0.0f, 10.0f, 0.0f));
        }

        if(other.CompareTag("Player") && projectileType == PROJECTILETYPE.MONSTER)  // Tag�� �˻��Ѵ�.
        {
            Destroy(this.gameObject);
            other.gameObject.GetComponent<PlayerHp>().Damaged(1);
            GameObject Temp = GameObject.FindGameObjectWithTag("GameManager");
            Temp.GetComponent<HUDTextManager>().UpdateHUDTextSet("1", other.gameObject, new Vector3(0.0f, 10.0f, 0.0f));
        }
    }
}
