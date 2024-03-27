using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int age;
        age = 30;
        Debug.Log(age);

        float height1 = 160.5f; //f를 쓰지 않으면 double형으로 인식 됨
        float height2;
        height2 = height1;
        Debug.Log(height2);

        double height3 = 170.5;
        double height4;
        height4 = height3;
        Debug.Log(height4);

        string name;
        name = "전제민";
        Debug.Log(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
