using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int Hp = 5;

    public void Damaged(int Damage)
    {
        Hp -= Damage;
        if(Hp <= 0)
        {
            GameObject temp = this.gameObject;
            Destroy(temp);
        }
    }
}
