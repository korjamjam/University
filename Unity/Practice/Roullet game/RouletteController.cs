using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0;  // 회전 속도   

    void Start()
    {
        // 프레임레이트를 60으로 고정
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // 클릭하면 회전 속도를 설정한다. 0은 좌클릭, 1은 우클릭, 2는 휠클릭
        if (Input.GetMouseButtonDown(0)) //좌클릭 클릭한 순간 true 반환
        {
            this.rotSpeed = -30; // -를 넣으면 반대 방향으로 돌아감.
            //양수는 반시계 방향, 음수는 시계 방향으로 돈다.
        }
        if (Input.GetMouseButtonUp(1))//우클릭, 떼는 순간 true 반환
        {
            this.rotSpeed = 30; // -를 넣으면 반대 방향으로 돌아감.
            //양수는 반시계 방향, 음수는 시계 방향으로 돈다.
        }
        if (Input.GetMouseButton(2)) //휠클릭, 누르고 있는 동안 true 반환
        {
            this.rotSpeed = 10; 
        }
        // 회전 속도만큼 룰렛을 회전시킨다
        transform.Rotate(0, 0, this.rotSpeed);

        // 룰렛을 감속시킨다
        this.rotSpeed *= 0.97f;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}