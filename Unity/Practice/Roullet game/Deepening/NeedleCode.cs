using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleCode : MonoBehaviour
{
    float rotSpeed = 0;  // 회전 속도
    bool isStopping = false; // 룰렛이 멈추고 있는지 여부를 확인하는 플래그

    void Start()
    {
        // 프레임레이트를 60으로 고정
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // 클릭하면 회전 속도를 설정한다.
        if (Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 30;
            this.isStopping = false; // 회전이 시작되었으니, 멈추는 중이 아님
        }

        if (this.rotSpeed > 0.2 || this.rotSpeed < -0.2) // 회전 중
        {
            transform.Rotate(0, 0, this.rotSpeed);
            this.rotSpeed *= 0.97f; // 룰렛을 감속시킨다
        }
        else if (!this.isStopping) // 회전이 거의 멈춘 상태
        {
            this.rotSpeed = 0; // 회전 속도를 완전히 0으로 설정
            this.isStopping = true; // 멈추는 중으로 플래그 변경
            ShowStopMessage(); // 디버그 메시지 출력
        }

        // ESC 키를 눌러 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ShowStopMessage()
    {
        float angle = transform.eulerAngles.z;

        // 각도에 따라 다른 메시지 출력
        if (angle >= 330 || angle < 30)
        {
            Debug.Log("운수 보통");
        }
        else if (angle >= 30 && angle < 90)
        {
            Debug.Log("운수 매우 나쁨");
        }
        else if (angle >= 90 && angle < 150)
        {
            Debug.Log("운수 대통");
        }
        else if (angle >= 150 && angle < 210)
        {
            Debug.Log("운수 나쁨");
        }
        else if (angle >= 210 && angle < 270)
        {
            Debug.Log("운수 좋음");
        }
        else if (angle >= 270 && angle < 330)
        {
            Debug.Log("운수 조심");
        }
    }
}
