using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] Camera camera;
    [SerializeField] float speed = 5f;
    [SerializeField] float foo = 100f;

    float horizontalDirection;
    float verticalDirection;

    void Update()
    {
        SetDirection();
        CameraMovement();
    }

    private void SetDirection()
    {
        float cameraHorizontalLine = camera.pixelWidth / 2;
        float cameraVerticalLine = camera.pixelHeight / 2;

        if (Input.mousePosition.x < cameraHorizontalLine)
        {
            horizontalDirection = -1;
        }
        else
        {
            horizontalDirection = 1;
        }

        if (Input.mousePosition.y < cameraVerticalLine)
        {
            verticalDirection = 1;
        }
        else
        {
            verticalDirection = -1;
        }
    }

    private void CameraMovement()
    {
        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        
        if (mousePosX <= foo || mousePosX >= camera.pixelWidth - foo)
        {
            float clampedCordsHorizontal = Mathf.Clamp(transform.position.z + speed * horizontalDirection * Time.deltaTime, 25, 80);
            Vector3 vectorHorizontal = new Vector3(transform.position.x, transform.position.y, clampedCordsHorizontal);
            transform.position = vectorHorizontal;
        }
        
        if(mousePosY <= foo || mousePosY >= camera.pixelHeight - foo)
        {
            float clampedCordsVertical = Mathf.Clamp(transform.position.x + speed * verticalDirection * Time.deltaTime,70,108);
            Vector3 vectorVertical = new Vector3(clampedCordsVertical, transform.position.y, transform.position.z);
            transform.position = vectorVertical;
        } 
    }
}
