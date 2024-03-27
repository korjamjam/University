using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test6arr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] points = { 83, 99, 52, 93, 15 };
        for (int i = 0; i < points.Length; i++) //Length는 배열 크기를 자동으로 조정
        {
            if (points[i] >= 90)
            {
                Debug.Log(points[i]);
            }
        }

        int sum = 0;
        for (int i = 0; i<points.Length; i++)
        {
            sum+= points[i];
        }

        int average = sum / points.Length;
        Debug.Log(average);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
