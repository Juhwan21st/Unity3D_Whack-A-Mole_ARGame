using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetObject_Jump : MonoBehaviour
{
    [Header("TargetObject - Jump Settings")]
    [SerializeField] float popHeight = 0.2f; // Height the object will pop up

    Vector3 startPos; // Starting position of the object
    Vector3 popPos; // Position when the object is popped up

    [Header("TargetObject - Debugging Temp")]
    [SerializeField] int popState = 0; // Current state of the popping animation
    [SerializeField] float waitTime = 1; // Time to wait before changing state

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // Store the starting position
        popPos = transform.position; // Initialize popPos with the starting position
        popPos.y += popHeight; // Adjust the y position to create the pop effect
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PopState: " + popState + " Position: " + transform.position);  // Log current state and position for debugging

        // If in state 1, move the object upwards
        if (popState == 1)
        {
            transform.position = Vector3.Lerp(transform.position, popPos, 5 * Time.deltaTime); // Smoothly move to pop position
            if (Mathf.Abs(transform.position.y - popPos.y) < 0.01f)  // Check if close enough to the target position
            {
                popState = 2; // Change to waiting state
                waitTime = Random.Range(0.5f, 2); // Set random wait time
            }
        }

        // If in state 2, wait for the set amount of time
        if (popState == 2)
        {
            waitTime -= Time.deltaTime; // Decrease wait time
            if (waitTime < 0)
            {
                popState = 3; // Change to state 3 to move back to start position
            }
        }

        // If in state 3, move the object back downwards
        if (popState == 3)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 5 * Time.deltaTime); // Smoothly move back to start position
            if (Mathf.Abs(transform.position.y - startPos.y) < 0.01f)  // Check if close enough to the start position
            {
                popState = 0; // Reset to initial state
                transform.position = startPos; // Ensure exact start position
            }
        }
    }   // end of Update
    
    // Public method to initiate the pop sequence
    public void Pop()
    {
        if (popState == 0)
        {
            Debug.Log("Pop called"); // Log that the Pop method was called
            popState = 1; // Change state to start popping up
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hammer" && popState > 0)
        {
            popState = 0;
            transform.position = startPos;
            WhackAMoleGame.score += 10;
            //play sound
            //audio.PlayOneShot(sfx, 1);
        }
    }
}
