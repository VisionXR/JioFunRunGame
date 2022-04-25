using UnityEngine;

public class SkyBoxChanger : MonoBehaviour
{

    public GameObject Head, Left, Right;
    void Start()
    {
        SetSkyBox();
    }
    
    public void SetSkyBox()
    {
        Head.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        Left.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        Right.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
    }
}
