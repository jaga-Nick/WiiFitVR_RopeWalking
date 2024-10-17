using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackForce = 10f;  // ������΂���

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[�Ƀ^�O "Player" �����Ă���O��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �v���C���[�� Rigidbody ���擾
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // �Փ˂̐ڐG�_�̖@�����擾�i�Փ˕����j
                Vector3 knockbackDirection = collision.contacts[0].normal;

                // �v���C���[�𔽑Ε����ɐ�����΂�
                playerRigidbody.AddForce(-knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
