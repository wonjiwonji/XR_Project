using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected FxManager FxManager => FxManager.Instance;
    public int Hp = 5;
    
    public void Damaged(int Damage)
    {
        Hp -= Damage;
        if(Hp <= 0)
        {
            GameObject temp = this.gameObject;
            Destroy(temp);
            FxManager.PlayFx(this.gameObject.transform, FxType.Explosion, new Vector3(0.0f, 1.0f, 0.0f));
        }
    }
}
