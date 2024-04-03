using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0; // ���� ȸ�� �ӵ�
    float acceleration = 0.5f; // ���ӵ�
    float maxSpeed = 30.0f; // �ְ� �ӵ�
    bool isAccelerating = false; // ���� ������ ����

    void Start()
    {
        // �����ӷ���Ʈ�� 60���� ����
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // ��Ŭ�� �� ���� ����
        if (Input.GetMouseButtonDown(0))
        {
            isAccelerating = true;
        }

        // ���� ���̰� �ְ� �ӵ��� �������� �ʾҴٸ�
        if (isAccelerating && Mathf.Abs(rotSpeed) < maxSpeed)
        {
            // ����
            rotSpeed -= acceleration;
        }
        else if (Mathf.Abs(rotSpeed) >= maxSpeed)
        {
            // �ְ� �ӵ��� �����ϸ� ���� ����
            isAccelerating = false;
        }

        // ȸ�� �ӵ���ŭ �귿�� ȸ����Ų��
        transform.Rotate(0, 0, this.rotSpeed);

        // ���� ���� �ƴ϶�� ����
        if (!isAccelerating)
        {
            this.rotSpeed *= 0.97f;
        }

        // ESC Ű�� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
