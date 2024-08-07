using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode driftKey = KeyCode.LeftShift;
    public KeyCode brakeKey = KeyCode.LeftControl;

    private float verticalInput;
    private float horizontalInput;
    private bool isDrifting;
    private bool isBraking;

    void Update()
    {
        verticalInput = (Input.GetKey(forwardKey) ? 1 : 0) - (Input.GetKey(backwardKey) ? 1 : 0);
        horizontalInput = (Input.GetKey(leftKey) ? 1 : 0) - (Input.GetKey(rightKey) ? 1 : 0);
        isDrifting = Input.GetKey(driftKey);
        isBraking = Input.GetKey(brakeKey);
    }

    public float GetVerticalInput()
    {
        return verticalInput;
    }

    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    public bool IsDrifting()
    {
        return isDrifting;
    }

    public bool IsBraking()
    {
        return isBraking;
    }
}