using UnityEngine;

public class JumpSetteing : MonoBehaviour
{
    public GameObject centerEyeAnchor;
    private float initialYPosition;
    private bool isCrouched;
    private bool isGrounded = true;  // �n�ʂɂ��邩�ǂ���
    private bool isRope = true; //���[�v�ɂ��邩�ǂ���
    private Rigidbody rb;
    private bool hasInitialized = false;
    private int framesToWait = 10; // 10�t���[���ҋ@

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
                return; // �t���[���ҋ@
            }
            initialYPosition = centerEyeAnchor.transform.localPosition.y;
            hasInitialized = true;
            Debug.Log("����Y�ʒu�ݒ芮��: " + initialYPosition);
        }

        float currentYPosition = centerEyeAnchor.transform.localPosition.y;

        // ���Ⴊ�݌��o�i�n�ʂɂ���Ƃ��̂݁j
        if ((isRope || isGrounded) && initialYPosition > currentYPosition + 0.2f)
        {
            isCrouched = true;
        }
        else if ((isRope || isGrounded) && isCrouched && currentYPosition + 0.05f >= initialYPosition)
        {
            Jump();
            isCrouched = false;
        }
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * 80.0f, ForceMode.Impulse);
        isGrounded = false;  // �W�����v������n�ʂ𗣂��
        isRope = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // �n�ʂɐڐG������isGrounded��true�ɂ���
        if (collision.gameObject.CompareTag("Ground"))  // �n�ʂ̃^�O��"Ground"�ł���Ɖ���
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Rope"))  // �n�ʂ̃^�O��"Ground"�ł���Ɖ���
        {
            isRope = true;
        }
    }
}
