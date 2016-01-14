using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour
{
    void Update()
    {
        float angle = Time.deltaTime * 10;
        transform.Rotate(Vector3.up, angle);
    }
}
