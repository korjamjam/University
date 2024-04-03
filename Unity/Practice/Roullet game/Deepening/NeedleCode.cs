using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleCode : MonoBehaviour
{
    float rotSpeed = 0;  // ȸ�� �ӵ�
    bool isStopping = false; // �귿�� ���߰� �ִ��� ���θ� Ȯ���ϴ� �÷���

    void Start()
    {
        // �����ӷ���Ʈ�� 60���� ����
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Ŭ���ϸ� ȸ�� �ӵ��� �����Ѵ�.
        if (Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 30;
            this.isStopping = false; // ȸ���� ���۵Ǿ�����, ���ߴ� ���� �ƴ�
        }

        if (this.rotSpeed > 0.2 || this.rotSpeed < -0.2) // ȸ�� ��
        {
            transform.Rotate(0, 0, this.rotSpeed);
            this.rotSpeed *= 0.97f; // �귿�� ���ӽ�Ų��
        }
        else if (!this.isStopping) // ȸ���� ���� ���� ����
        {
            this.rotSpeed = 0; // ȸ�� �ӵ��� ������ 0���� ����
            this.isStopping = true; // ���ߴ� ������ �÷��� ����
            ShowStopMessage(); // ����� �޽��� ���
        }

        // ESC Ű�� ���� ���� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ShowStopMessage()
    {
        float angle = transform.eulerAngles.z;

        // ������ ���� �ٸ� �޽��� ���
        if (angle >= 330 || angle < 30)
        {
            Debug.Log("��� ����");
        }
        else if (angle >= 30 && angle < 90)
        {
            Debug.Log("��� �ſ� ����");
        }
        else if (angle >= 90 && angle < 150)
        {
            Debug.Log("��� ����");
        }
        else if (angle >= 150 && angle < 210)
        {
            Debug.Log("��� ����");
        }
        else if (angle >= 210 && angle < 270)
        {
            Debug.Log("��� ����");
        }
        else if (angle >= 270 && angle < 330)
        {
            Debug.Log("��� ����");
        }
    }
}
