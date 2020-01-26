using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;

    public float minimumX = -360.0f;
    public float maximumX = 360.0f;
    public float minimumY = -60.0f;
    public float maximumY = 60.0f;

    float rotationX = 0.0f;
    float rotationY = 0.0f;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0.0f;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0.0f;

    public float frameCounter = 20;
    Quaternion originalRotation;
    Quaternion originalCamRotation;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
        originalCamRotation = transform.GetChild(0).transform.localRotation;
    }

    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            //Resets the average rotation
            rotAverageY = 0.0f;
            rotAverageX = 0.0f;

            //Gets rotational input from the mouse
            rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            //Adds the rotation values to their relative array
            rotArrayY.Add(rotationY);
            rotArrayX.Add(rotationX);

            //If the arrays length is bigger or equal to the value of frameCounter remove the first value in the array
            if (rotArrayY.Count >= frameCounter)
            {
                rotArrayY.RemoveAt(0);
            }
            if (rotArrayX.Count >= frameCounter)
            {
                rotArrayX.RemoveAt(0);
            }

            //Adding up all the rotational input values from each array
            for (int j = 0; j < rotArrayY.Count; j++)
            {
                rotAverageY += rotArrayY[j];
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }

            //Standard maths to find the average
            rotAverageY /= rotArrayY.Count;
            rotAverageX /= rotArrayX.Count;

            //Clamp the rotation average to be within a specific value range
            rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            //Get the rotation you will be at next as a Quaternion
            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

            //Rotate player
            transform.localRotation = originalRotation * xQuaternion;

            //Rotate camera
            transform.GetChild(0).transform.localRotation = originalCamRotation * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotAverageX = 0.0f;
            rotationX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
            rotArrayX.Add(rotationX);
            if (rotArrayX.Count >= frameCounter)
            {
                rotArrayX.RemoveAt(0);
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }
            rotAverageX /= rotArrayX.Count;
            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360.0f) && (angle <= 360.0f))
        {
            if (angle < -360.0f)
            {
                angle += 360.0f;
            }
            if (angle > 360.0f)
            {
                angle -= 360.0f;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
