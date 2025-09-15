using UnityEditor.Rendering;
using UnityEngine;

public class SkyBoxRotation : MonoBehaviour
{

    [Header("Skybox Settings")]

    [Space(16)]

    [SerializeField] float rotateSpeed = 0.2f;

    public float RotateSpeed                                                    // for later use !
    {
        get => rotateSpeed;
        set => rotateSpeed = value;
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
    }
}
