using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int hp = 100;    //private은 클래스의 멤버변수이기에 아래의 메인클래스에서 못가져옴.
    private int power = 50;

    public void Attack()
    {
        //int power = 9999; 만약 이 줄이 있다면 this를 꼭 붙여야 함,
        Debug.Log(this.power + " 대미지를 입혔다");
    }
    public void Damage(int damage)
    {
        this.hp -= damage;
        Debug.Log(damage + " 대미지를 입었다");
    }
    public int GetHP()
    { 
        return hp; 
    }
}

public class Test8class : MonoBehaviour// : MonoBehaviour = 상속
{
    void Start()
    {
        Player myPlayer = new Player();
        myPlayer.Attack();
        myPlayer.Damage(30);
        Debug.Log("현재 HP = " + myPlayer.GetHP());
    }
}
