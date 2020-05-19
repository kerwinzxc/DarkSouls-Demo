﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Keys Setting")]
    public string keyUp;
    public string keyDown;
    public string keyLeft;
    public string keyRight;
    
    public string keyA;
    public string keyB;
    public string keyX;
    public string keyY;

    public string keyCameraUp;
    public string keyCameraRight;

    [Header("Direction Outputs")]
    public float directionUp;
    public float directionRight;

    public float cameraUp;
    public float cameraRight;

    public float directionMagnitude;
    public Vector3 directionVector;

    public bool run;
    public bool jump;
    public bool attack;

    [Header("Other Settings")]
    public bool inputEnabled = true;

    private float targetDirectionUp;
    private float targetDirectionRight;
    private float velocityDirectionUp;
    private float velocityDirectionRight;

    private bool lastJump;
    private bool lastAttack;

    // Update is called once per frame
    void Update()
    {
        targetDirectionUp = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDirectionRight = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        cameraUp = Input.GetAxis(keyCameraUp);
        cameraRight = Input.GetAxis(keyCameraRight);

        if (!inputEnabled)
        {
            targetDirectionUp = 0;
            targetDirectionRight = 0;
        }

        directionUp = Mathf.SmoothDamp(directionUp, targetDirectionUp, ref velocityDirectionUp, 0.1f);
        directionRight = Mathf.SmoothDamp(directionRight, targetDirectionRight, ref velocityDirectionRight, 0.1f);

        Vector2 directionAxis = SquareToCircle(new Vector2(directionRight, directionUp));
        float tempDirectionRight = directionAxis.x;
        float tempDirectionUp = directionAxis.y;

        directionMagnitude =
            Mathf.Sqrt((tempDirectionUp * tempDirectionUp) + (tempDirectionRight * tempDirectionRight));
        directionVector = transform.forward * tempDirectionUp + transform.right * tempDirectionRight;

        run = Input.GetKey(keyA);

        bool tempJump = Input.GetKey(keyB);
        if (tempJump != lastJump && tempJump)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        lastJump = tempJump;
        
        bool tempAttack = Input.GetKey(keyX);
        if (tempAttack != lastAttack && tempAttack)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }

        lastAttack = tempAttack;
    }

    Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }
}
