using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string str1 = "Happy ";
        string str2 = "Birthday";
        str1 += str2; //오른쪽 값을 더함.
        Debug.Log(str1);
        Debug.Log(str2);

        string str3 = "Happy";
        int num1 = 123;
        string message = str3 + num1;
        Debug.Log(message);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
