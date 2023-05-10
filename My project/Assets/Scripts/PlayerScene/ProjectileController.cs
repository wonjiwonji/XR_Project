using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] protected SoundProfileData soundProfileData;

    public Vector3 launchDirection;
    public GameObject Projectile;

    protected AudioManager AudioManager => AudioManager.Instance;

    public void FireProjectile()
    {
        AudioManager.PlayOneShot(soundProfileData.GetRandomClip());
        GameObject temp = (GameObject)Instantiate(Projectile);

        temp.transform.position = this.gameObject.transform.position;
        temp.transform.localScale = Vector3.one * 0.3f;
        temp.GetComponent<ProjectileMove>().launchDirection = transform.forward;
        temp.GetComponent<ProjectileMove>().projectileType = ProjectileMove.PROJECTILETYPE.PLAYER;

        Destroy(temp, 10.0f); // 10초 후에 발사체 삭제
    }
    
}
