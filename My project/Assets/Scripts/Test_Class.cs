using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int hp = 100;
    private int Power = 50;

    public void Attack()
    {
        Debug.Log(this.Power + " �������� ������.");
        // �� ��� ���� �ٷ� ������ ���°Ŷ� ���� this�� ���� �ʾƵ� ��
    }

    public void Damage(int damage)
    {
        this.hp -= damage;
        Debug.Log(damage + " �������� �Ծ���.");

    }

}
public class Test_Class : MonoBehaviour // ������ ��� �����Ǿ� ��ӹ��� �ֶ� �̰� �����
{
    // Start is called before the first frame update
    void Start()
    {
        Player mPlayer = new Player();
        mPlayer.Attack();
        mPlayer.Damage(30);
    

    Vector2 startPos = new Vector2(2.0f, 1.0f); // ������, �Ǽ�
    Vector2 endPos = new Vector2(8.0f, 5.0f);
    Vector2 dir = endPos - startPos; // �� ���͸� ���� ���⼺�� �� �� �ִ�.
    Debug.Log(dir);

        float len = dir.magnitude; // ������ ���̸� ���ϴ� �ɹ�����
    Debug.Log(len);
    }
}
