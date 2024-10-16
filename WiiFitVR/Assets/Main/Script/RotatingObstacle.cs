using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    // 回転速度を調整できる変数
    public float rotationSpeed = 100f;

    // 回転方向を反転できるbool変数
    public bool reverseRotation = false;

    void Update()
    {
        // 回転方向を決定
        float direction = reverseRotation ? -1f : 1f;

        // 回転を適用
        transform.Rotate(Vector3.up * rotationSpeed * direction * Time.deltaTime);
    }
}
