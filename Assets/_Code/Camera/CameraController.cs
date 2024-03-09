using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float edgeThickness = 20f;
    public float minXLimit = -20f;
    public float maxXLimit = 20f;

    private Camera mainCamera;
    private Rect rightEdgeRect;
    private Rect leftEdgeRect;

    void Start()
    {
        mainCamera = Camera.main;
        rightEdgeRect = new Rect(Screen.width - edgeThickness, 0, edgeThickness, Screen.height);
        leftEdgeRect = new Rect(0, 0, edgeThickness, Screen.height);
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (rightEdgeRect.Contains(Input.mousePosition))
        {
            moveDirection += Vector3.right;
        }
        else if (leftEdgeRect.Contains(Input.mousePosition))
        {
            moveDirection += Vector3.left;
        }

        moveDirection.Normalize();
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // Clamp the camera's x-position within the specified limits
        newPosition.x = Mathf.Clamp(newPosition.x, minXLimit, maxXLimit);

        transform.position = newPosition;
    }
}
