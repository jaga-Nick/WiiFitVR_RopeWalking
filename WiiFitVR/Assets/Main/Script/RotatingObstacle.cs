using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    // ‰ñ“]‘¬“x‚ğ’²®‚Å‚«‚é•Ï”
    public float rotationSpeed = 100f;

    // ‰ñ“]•ûŒü‚ğ”½“]‚Å‚«‚ébool•Ï”
    public bool reverseRotation = false;

    void Update()
    {
        // ‰ñ“]•ûŒü‚ğŒˆ’è
        float direction = reverseRotation ? -1f : 1f;

        // ‰ñ“]‚ğ“K—p
        transform.Rotate(Vector3.up * rotationSpeed * direction * Time.deltaTime);
    }
}
