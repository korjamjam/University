using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int hp = 100;    //private�� Ŭ������ ��������̱⿡ �Ʒ��� ����Ŭ�������� ��������.
    private int power = 50;

    public void Attack()
    {
        //int power = 9999; ���� �� ���� �ִٸ� this�� �� �ٿ��� ��,
        Debug.Log(this.power + " ������� ������");
    }
    public void Damage(int damage)
    {
        this.hp -= damage;
        Debug.Log(damage + " ������� �Ծ���");
    }
    public int GetHP()
    { 
        return hp; 
    }
}

public class Test8class : MonoBehaviour// : MonoBehaviour = ���
{
    void Start()
    {
        Player myPlayer = new Player();
        myPlayer.Attack();
        myPlayer.Damage(30);
        Debug.Log("���� HP = " + myPlayer.GetHP());
    }
}
