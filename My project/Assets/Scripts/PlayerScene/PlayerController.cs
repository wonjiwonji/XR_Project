using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // UI �̺�Ʈ�� ���� �ϱ� ����

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    public GameObject PlayerPivot; // �÷��̾ �ٶ󺸴� ������ �˱� ����
    public Camera viewCamera; // ���� ī�޶� �޾ƿ��� Camera ������Ʈ
    public Vector3 velocity; // �̵� �ӵ� ��
    public ProjectileController projectileController; // ProjectileController Ŭ������ �����´�.

    // Start is called before the first frame update
    void Start()
    {
        viewCamera = Camera.main; // ��ũ��Ʈ�� ���۵� �� ī�޶� �޾ƿ´�.   
    }

    // Update is called once per frame
    void Update()
    {
        // ȭ�鿡�� -> ���� 3D ���� ��ǥ�� �̾Ƴ���.
        Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));

        // ���� ��ǥ�� ĳ���ͺ��� ���� ���� ��쿡�� ���� �Ĵٺ��� ������ ���� y�� ���� �����ش�.
        Vector3 targetPosition = new Vector3(mousePos.x, transform.position.y, mousePos.z);

        // �Ǻ��� �ش� Ÿ���� ���� �Ѵ�.
        PlayerPivot.transform.LookAt(targetPosition, Vector3.up);

        // ����Ű�� ���ؼ� �̵�
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * moveSpeed;
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10.0f, ForceMode.Impulse);
        }
        if(!EventSystem.current.IsPointerOverGameObject())
        {   // ���� UI�� ���ÿ� �������� �ʰ� �ϱ� ���� ���� <- Ŭ���ϴ� ��
            if (Input.GetMouseButtonDown(0))
            {
                projectileController.FireProjectile();
            }
        }
    }

    private void FixedUpdate()
    {
        // �̵� ���� ���� Rigidbody �������� �����Ͽ� ĳ���͸� �̵���Ų��.
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
