using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
            transform.Rotate(0, 10, 0);

        else if (Input.GetMouseButton(1))
            transform.Rotate(0, -10, 0);
    }
}
