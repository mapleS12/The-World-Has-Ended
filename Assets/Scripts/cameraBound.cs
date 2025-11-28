using UnityEngine;

[RequireComponent(typeof(Camera))]
public class cameraBound : MonoBehaviour
{
    public Collider2D bounds;

    private float halfHeight;
    private float halfWidth;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (bounds == null) return;

        Bounds b = bounds.bounds;

        float clampedX = Mathf.Clamp(transform.position.x, b.min.x + halfWidth, b.max.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, b.min.y + halfHeight, b.max.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
