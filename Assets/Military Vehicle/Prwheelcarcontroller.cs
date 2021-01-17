using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Prwheelcarcontroller : MonoBehaviour
{

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider FL_Wheel, FR_Wheel;
    public WheelCollider BL_Wheel, BR_Wheel;
    public Transform FL_WheelT, FR_WheelT;
    public Transform BL_WheelT, BR_WheelT;
    public float maxSteeringAngle = 30;
    public float motorForce = 50; 

    private void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteeringAngle * m_horizontalInput;
        FL_Wheel.steerAngle = m_steeringAngle;
        FR_Wheel.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        BR_Wheel.motorTorque = m_verticalInput * motorForce;
        BL_Wheel.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(FL_Wheel, FL_WheelT);
        UpdateWheelPose(FR_Wheel, FR_WheelT);
        UpdateWheelPose(BL_Wheel, BL_WheelT);
        UpdateWheelPose(BR_Wheel, BR_WheelT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
