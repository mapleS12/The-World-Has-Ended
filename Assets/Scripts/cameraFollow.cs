using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth = 5f;

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 p = transform.position;
        p.x = target.position.x;
        p.y = target.position.y;
        transform.position = Vector3.Lerp(transform.position, p, smooth * Time.deltaTime);
    }
}
