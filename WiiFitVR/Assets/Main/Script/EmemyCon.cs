using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyCon : MonoBehaviour
{
    public Transform pointA;   // �ړ��J�n�n�_
    public Transform pointB;   // �ړ��I���n�_
    public float speed = 2f;   // �ړ����x

    private Vector3 target;    // ���݂̈ړ���

    void Start()
    {
        // �����̈ړ����ݒ�
        target = pointB.position;
    }

    void Update()
    {
        // ���݂̈ʒu����^�[�Q�b�g�ւ̈ړ�
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // �^�[�Q�b�g�ɓ��B�����ꍇ�A�ړ����؂�ւ��A�����𔽓]
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // �ړ���𔽓]
            target = (target == pointA.position) ? pointB.position : pointA.position;

            // �����̔��]
            Vector3 scale = transform.localScale;
            scale.x *= -1;  // x�������̃X�P�[���𔽓]
            transform.localScale = scale;
        }
    }
}
