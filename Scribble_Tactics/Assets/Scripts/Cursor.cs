using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private float moveSpeed = 6f;

    // Update is called once per frame
    // Unless its not, then it just wont work. 
    // also, you cant tell me what to do.
    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;

        transform.position += (Vector3)inputVector * moveSpeed * Time.deltaTime;
    }
}
