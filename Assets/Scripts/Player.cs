using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] GameInput gameInput;
    private bool isWalking;

    void Update()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();        

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y) * moveSpeed * Time.deltaTime;

        transform.position += moveDir;

        float rotationSpeed = 10f;

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
