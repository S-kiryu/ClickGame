// CameraSetup.cs
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(1f, 0f, 1f, 0f); // ƒ}ƒ[ƒ“ƒ^
    }
}