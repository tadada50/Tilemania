using Unity.Cinemachine;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    //[SerializeField] Camera cam;
    Camera cam;
   // CinemachineCamera cam;
    [SerializeField] float backGroundMoveFactorX;
    [SerializeField] float backGroundMoveFactorY;
    private Vector3 lastCamPosition;
    void Start()
    {
        cam = FindFirstObjectByType<Camera>();
        Debug.Log(this.gameObject.ToString());
        // cam = GetComponent<Camera>();
        Debug.Log(cam.ToString());
    }

    void Update()
    {
        MoveBackground();
        TrackCameraValues();
    }

    private void MoveBackground(){
        if(IsIdle()){
            return;
        }
        // Debug.Log("Camera fieldOfView:"+ cam.fieldOfView);
        Vector3 deltaVector = cam.transform.position - lastCamPosition;
        // Debug.Log("Not idle:" + deltaVector);
        // this.transform.Translate(deltaVector.x* Time.deltaTime * backGroundMoveFactorX, deltaVector.y* Time.deltaTime *backGroundMoveFactorY,0);
        
        this.transform.Translate(deltaVector.x * backGroundMoveFactorX, deltaVector.y* backGroundMoveFactorY,0);
    }
    private bool IsIdle()
    {
        // if(cam.transform.position.x - lastCamPosition.x>0.01)
        return Vector3.Equals(cam.transform.position, lastCamPosition);
    }
    private void TrackCameraValues()
    {
        lastCamPosition = cam.transform.position;
    }

}
