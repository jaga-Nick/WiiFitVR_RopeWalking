using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    // ��]���x�𒲐��ł���ϐ�
    public float rotationSpeed = 100f;

    // ��]�����𔽓]�ł���bool�ϐ�
    public bool reverseRotation = false;

    void Update()
    {
        // ��]����������
        float direction = reverseRotation ? -1f : 1f;

        // ��]��K�p
        transform.Rotate(Vector3.up * rotationSpeed * direction * Time.deltaTime);
    }
}
