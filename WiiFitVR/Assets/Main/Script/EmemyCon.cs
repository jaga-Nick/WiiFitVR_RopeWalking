using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyCon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 1000 * Time.deltaTime);
    }

    public float knockbackForce = 10f;  // 吹っ飛ばす力

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーにタグ "Player" がついている前提
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーの Rigidbody を取得
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // 衝突の接触点の法線を取得（衝突方向）
                Vector3 knockbackDirection = collision.contacts[0].normal;

                // プレイヤーを反対方向に吹っ飛ばす
                playerRigidbody.AddForce(-knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
