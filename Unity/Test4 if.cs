using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4if : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int hp = 100;
        if(hp <= 50)
        {
            Debug.Log("도망!");
        }
        else
        {
            Debug.Log("전투!");
        }

        int x = 1; //전역변수
        if(x == 1)
        {
            int y = 2; //지역변수
            Debug.Log(x);
            Debug.Log(y);
        }
        //Debug.Log(y); 블럭안에 없기 때문에 오류가 발생
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
