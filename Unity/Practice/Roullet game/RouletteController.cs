using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0;  // ȸ�� �ӵ�   

    void Start()
    {
        // �����ӷ���Ʈ�� 60���� ����
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // Ŭ���ϸ� ȸ�� �ӵ��� �����Ѵ�. 0�� ��Ŭ��, 1�� ��Ŭ��, 2�� ��Ŭ��
        if (Input.GetMouseButtonDown(0)) //��Ŭ�� Ŭ���� ���� true ��ȯ
        {
            this.rotSpeed = -30; // -�� ������ �ݴ� �������� ���ư�.
            //����� �ݽð� ����, ������ �ð� �������� ����.
        }
        if (Input.GetMouseButtonUp(1))//��Ŭ��, ���� ���� true ��ȯ
        {
            this.rotSpeed = 30; // -�� ������ �ݴ� �������� ���ư�.
            //����� �ݽð� ����, ������ �ð� �������� ����.
        }
        if (Input.GetMouseButton(2)) //��Ŭ��, ������ �ִ� ���� true ��ȯ
        {
            this.rotSpeed = 10; 
        }
        // ȸ�� �ӵ���ŭ �귿�� ȸ����Ų��
        transform.Rotate(0, 0, this.rotSpeed);

        // �귿�� ���ӽ�Ų��
        this.rotSpeed *= 0.97f;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}