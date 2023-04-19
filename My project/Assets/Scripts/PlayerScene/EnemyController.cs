using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 5.0f;  // �̵� �ӵ�
    public float rotationSpeed = 1.0f;  // ��ž ȸ�� �ӵ�
    public GameObject bulletPrefab; // �Ѿ� ������
    public GameObject EnemyPivot;   // �� ��ž �Ǻ�
    public Transform firePoint; // �߻� ��ġ
    public float fireRate = 1f; // �߻� �ӵ�

    private Rigidbody rb;
    private Transform player;

    private float NextFireTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // rb�� ���� RigidBody �Է�

        // Player Tag�� ������ �ִ� ������Ʈ transform�� �Է�
        player = GameObject.FindGameObjectWithTag("Player").transform;  // FindGameObjectWithTag ; unity���� Tag�� ã���ִ� �Լ�

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {

        
        if(Vector3.Distance(player.position, transform.position) > 5.0f) // �÷��̾� �����ǰ� ���� �������� ����ؼ� 5 �̻��� ���� (�ʹ� ������ ��� �̻���)
        {
            Vector3 direction = (player.position - transform.position).normalized;  // �̵� ���⼺(�÷��̾�� ��)
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);   // ���⼺ ���� �� -> Ridgidbody�� �ݿ�
        }

        // ��ž ȸ��
        Vector3 targetDirection = (player.position - EnemyPivot.transform.position).normalized; // ��ž�� ���⼺�� nomalized(���)
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // ȸ������� = ���� ��������
        EnemyPivot.transform.rotation = Quaternion.Lerp(EnemyPivot.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);     // ���� ȸ�� ���� �ݿ�

        if (Time.time > NextFireTime)
        {
            NextFireTime = Time.time + 1f / fireRate;   // �ð� ��� ��� Ƚ��
            GameObject temp = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            temp.GetComponent<ProjectileMove>().launchDirection = firePoint.localRotation * Vector3.forward;    // �߻����� �������ִ� ����
            temp.GetComponent<ProjectileMove>().projectileType = ProjectileMove.PROJECTILETYPE.MONSTER;
            Destroy(temp, 10.0f);
        }

        }
    }
}
