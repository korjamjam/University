using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0; // 현재 회전 속도
    float acceleration = 0.5f; // 가속도
    float maxSpeed = 30.0f; // 최고 속도
    bool isAccelerating = false; // 가속 중인지 여부

    void Start()
    {
        // 프레임레이트를 60으로 고정
        Application.targetFrameRate = 60;
    }

    //누를 때 마다 방향이 바뀌고 속도도 리셋됨
    void Update()
    {
        // 좌클릭 시 가속 시작
        if (Input.GetMouseButtonDown(0))
        {
            isAccelerating = true;
        }

        // 가속 중이고 최고 속도에 도달하지 않았다면
        if (isAccelerating && Mathf.Abs(rotSpeed) < maxSpeed)
        {
            // 가속
            rotSpeed -= acceleration;
        }
        else if (Mathf.Abs(rotSpeed) >= maxSpeed)
        {
            // 최고 속도에 도달하면 가속 중지
            isAccelerating = false;
        }

        // 회전 속도만큼 룰렛을 회전시킨다
        transform.Rotate(0, 0, this.rotSpeed);

        // 가속 중이 아니라면 감속
        if (!isAccelerating)
        {
            this.rotSpeed *= 0.97f;
        }

        // ESC 키를 눌러 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
