using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // UI 이벤트를 관리 하기 위해

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6;
    public GameObject PlayerPivot; // 플레이어가 바라보는 방향을 알기 위해
    public Camera viewCamera; // 메인 카메라가 받아오는 Camera 오브젝트
    public Vector3 velocity; // 이동 속도 값
    public ProjectileController projectileController; // ProjectileController 클래스를 가져온다.

    // Start is called before the first frame update
    void Start()
    {
        viewCamera = Camera.main; // 스크립트가 시작될 때 카메라를 받아온다.   
    }

    // Update is called once per frame
    void Update()
    {
        // 화면에서 -> 게임 3D 공간 좌표를 뽑아낸다.
        Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));

        // 공간 좌표가 캐릭터보다 위에 있을 경우에는 위를 쳐다보기 때문에 같은 y축 값을 맞춰준다.
        Vector3 targetPosition = new Vector3(mousePos.x, transform.position.y, mousePos.z);

        // 피봇이 해당 타겟을 보게 한다.
        PlayerPivot.transform.LookAt(targetPosition, Vector3.up);

        // 방향키를 통해서 이동
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * moveSpeed;
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10.0f, ForceMode.Impulse);
        }
        if(!EventSystem.current.IsPointerOverGameObject())
        {   // 게임 UI와 동시에 동작하지 않게 하기 위해 설정 <- 클릭하는 것
            if (Input.GetMouseButtonDown(0))
            {
                projectileController.FireProjectile();
            }
        }
    }

    private void FixedUpdate()
    {
        // 이동 백터 값을 Rigidbody 물리값에 적용하여 캐릭터를 이동시킨다.
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
