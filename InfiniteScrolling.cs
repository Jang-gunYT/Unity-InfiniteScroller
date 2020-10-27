using UnityEngine;

// Created by Jang-gun
/// <summary> Infinite scrolling objects </summary>
public class InfiniteScrolling : MonoBehaviour
{
    [Header("Attributes")]
    [Tooltip("Amount to increment each object's position every FixedUpdate")]
    [SerializeField] float increment = 5f;
    [Tooltip("Lowest object is reset to the top when this many increments is reached")]
    [SerializeField] float resetAt = 200f;
    [Tooltip("Current amount of total increments which have occurred. " +
        "This is set at runtime and returns to 0 when it is >= resetAt")]
    float runningIncrement;
    [Tooltip("Index of the current lowest object in the \"conveyor\" of scrolling objects (toScroll)")]
    int lowestIndex;

    [Header("Positions")]
    [Tooltip("The starting transform position of the highest object in the objects to scroll through." +
        "The lowest object moves to this position when resetAt has been reached")]
    Vector2[] startPositions;

    [Header("References")]
    [Tooltip("Array of all objects to scroll. Should be sorted from the HIGHEST to LOWEST objects")]
    [SerializeField] GameObject[] toScroll;


    void Start()
    {
        // Initial
        // Set lowest index equal to the current LOWEST object
        lowestIndex = toScroll.Length - 1;
        /* Create new array of startPositions.
         * An array is used to account for scrolling objects having different x positions */
        startPositions = new Vector2[toScroll.Length];
        // Set startPositions
        for (int i = 0; i < toScroll.Length; i++)
        {
            startPositions[i] = new Vector2
                (
                    toScroll[i].transform.position.x,
                    toScroll[0].transform.position.y
                );
        }
    }

    void FixedUpdate()
    {
        // Scroll each object
        foreach (GameObject obj in toScroll)
        {
            // Position to move each object toward
            Vector2 newPosition = new Vector2
                (
                // If you want the scrolling objects to move in a different manner you would change code here
                    obj.transform.position.x,
                    obj.transform.position.y - increment
                );
            // Output newPosition vector to the transform position
            obj.transform.position = newPosition;
            runningIncrement += increment;

            // Check if resetAt has been reached
            if (runningIncrement >= resetAt)
            {
                toScroll[lowestIndex].transform.position = startPositions[lowestIndex];

                // Ensure lowestIndex stays positive to avoid going OutOfBounds
                if (lowestIndex > 0) lowestIndex--;
                else lowestIndex = toScroll.Length - 1;
                // Reset runningIncrement when reset has been reached
                runningIncrement = 0f;
            }
        }
    }
}
