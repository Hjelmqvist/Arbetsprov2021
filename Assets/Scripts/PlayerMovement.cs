using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += GetMoveDirection() * movementSpeed * Time.deltaTime;
    }

    Vector3 GetMoveDirection()
    {
        // Move character according to the cameras rotation
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward *= Input.GetAxisRaw("Vertical");
        forward.Normalize();

        Vector3 right = Camera.main.transform.right * Input.GetAxisRaw("Horizontal");
        return (forward + right).normalized;
    }
}
