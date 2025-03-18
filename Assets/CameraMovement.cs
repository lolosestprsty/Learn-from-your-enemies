using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    // Speed at which the camera pans horizontally.
    public float panSpeed = 5f;
    // How far left and right the camera can pan.
    public float leftLimit = -5f;
    public float rightLimit = 5f;
    
    [Header("Input Settings")]
    // If true, use A/D keys to pan; if false, use external UI triggers.
    public bool ADKeys = true;
    public bool movingLeft = false;
    public bool movingRight = false;
    
    [Header("Framerate Settings")]
    // Target frame rate for the game.
    public int targetFramerate = 60;

    void Start()
    {
        Application.targetFrameRate = targetFramerate;
    }

    void Update()
    {
        float move = 0f;

        // Use keyboard input to pan left/right.
        if (ADKeys)
        {
            if (Input.GetKey(KeyCode.A))
            {
                move = -panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move = panSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Alternatively, control via booleans (e.g., set by UI buttons)
            if (movingLeft)
            {
                move = -panSpeed * Time.deltaTime;
            }
            if (movingRight)
            {
                move = panSpeed * Time.deltaTime;
            }
        }

        // Move the camera horizontally while clamping the position.
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x + move, leftLimit, rightLimit);
        transform.position = newPos;
    }

    // These methods can be attached to UI button events to control panning.
    public void PanLeft()
    {
        movingLeft = true;
    }

    public void PanRight()
    {
        movingRight = true;
    }

    public void StopPanLeft()
    {
        movingLeft = false;
    }

    public void StopPanRight()
    {
        movingRight = false;
    }
}
