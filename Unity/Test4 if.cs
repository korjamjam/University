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
            Debug.Log("����!");
        }
        else
        {
            Debug.Log("����!");
        }

        int x = 1; //��������
        if(x == 1)
        {
            int y = 2; //��������
            Debug.Log(x);
            Debug.Log(y);
        }
        //Debug.Log(y); ���ȿ� ���� ������ ������ �߻�
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
