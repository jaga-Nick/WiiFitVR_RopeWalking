using UnityEngine;

public class JumpSetteing : MonoBehaviour
{
    public GameObject centerEyeAnchor;
    private float initialYPosition;
    private bool isCrouched;
    private bool isGrounded = true;  // 地面にいるかどうか
    private bool isRope = true; //ロープにいるかどうか
    private Rigidbody rb;
    private bool hasInitialized = false;
    private int framesToWait = 10; // 10フレーム待機

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isCrouched = false;
    }

    void Update()
    {
        if (!hasInitialized)
        {
            if (framesToWait > 0)
            {
                framesToWait--;
                return; // フレーム待機
            }
            initialYPosition = centerEyeAnchor.transform.localPosition.y;
            hasInitialized = true;
            Debug.Log("初期Y位置設定完了: " + initialYPosition);
        }

        float currentYPosition = centerEyeAnchor.transform.localPosition.y;

        // しゃがみ検出（地面にいるときのみ）
        if ((isRope || isGrounded) && initialYPosition > currentYPosition + 0.2f)
        {
            isCrouched = true;
        }
        else if ((isRope || isGrounded) && isCrouched && currentYPosition + 0.1f >= initialYPosition)
        {
            Jump();
            isCrouched = false;
        }
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * 8.0f, ForceMode.Impulse);
        isGrounded = false;  // ジャンプしたら地面を離れる
        isRope = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 地面に接触したらisGroundedをtrueにする
        if (collision.gameObject.CompareTag("Ground"))  // 地面のタグが"Ground"であると仮定
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Rope"))  // 地面のタグが"Ground"であると仮定
        {
            isRope = true;
        }
    }
}
