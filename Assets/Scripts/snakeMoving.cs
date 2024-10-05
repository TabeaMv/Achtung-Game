using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class snakeMoving : MonoBehaviour
{
    public float snakeSpeed = 3f;
    public float directionChange = 5f;
    private Vector3 movementDirection;
    public Button leftButton;
    public Button rightButton;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    
    private LineRenderer lineRenderer;
    private List<Vector3> prevPositions;

    
    // Start is called before the first frame update    
    void Start()
    {
        // initialize movement direction at the start
        movementDirection = new Vector3(1, 1, 0);

        // initialize line renderer
        lineRenderer = GetComponent<LineRenderer>();
        prevPositions = new List<Vector3>();

        // Set the first position in the trail (X, Y = current dot coordinates, Z = 0)
        prevPositions.Add(new Vector3(transform.position.x, transform.position.y, 0));
        lineRenderer.positionCount = prevPositions.Count; // adding number of points to be connected by line renderer
        lineRenderer.SetPosition(0, prevPositions[0]); // starting point for the drawn line

        // Set the width to make the line thinner
        lineRenderer.widthMultiplier = 0.1f;

        // Set the material to glow
        // Material glowingMaterial = new Material(Shader.Find("Unlit/Color"));
        // glowingMaterial.color = Color.white;
        // glowingMaterial.SetColor("_EmissionColor", Color.white * 2f);  // Increase for stronger glow
        // Assign the glowing material to the LineRenderer
        //lineRenderer.material = glowingMaterial;

        // Set sorting order for 2D
        lineRenderer.sortingLayerName = "UI"; // Is UI fine? Probably add snake to its own layer below UI
        lineRenderer.sortingOrder = 1;  // Ensure it appears correctly with your sprites
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementDirection * snakeSpeed * Time.deltaTime);

        if (isMovingLeft) {
            movementDirection = Quaternion.Euler(0, 0, directionChange) * movementDirection;
            }
        if (isMovingRight) {
            movementDirection = Quaternion.Euler(0, 0, -directionChange) * movementDirection;
        }

        // Create a rotation from the angle
        //Quaternion rotation = Quaternion.Euler(0, 0, 90f);
        // Apply the rotation to the current movement direction
        //movementDirection = rotation * movementDirection;
        movementDirection.Normalize(); // Normalize to ensure consistent speed

        UpdateTrail();
    }

    void UpdateTrail()
    {
        // Add the current position (X, Y and Z = 0) to the list
        prevPositions.Add(new Vector3(transform.position.x, transform.position.y, 0));

        // Update the LineRenderer with the new positions
        lineRenderer.positionCount = prevPositions.Count;
        for (int i = 0; i < prevPositions.Count; i++)
        {
            lineRenderer.SetPosition(i, prevPositions[i]);
        }
    }

    public void OnLeftButtonDown()
    {
        // Debug.Log("OnLeftButtonDown");
        isMovingLeft = true;
        isMovingRight = false;
    }

    public void OnLeftButtonUp()
    {
        isMovingLeft = false;
        isMovingRight = false;
    }

    public void OnRightButtonDown()
    {
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void OnRightButtonUp()
    {
        isMovingRight = false;
        isMovingLeft = false;
    }
}
