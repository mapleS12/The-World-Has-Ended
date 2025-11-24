using UnityEngine;
using Terresquall;  // 🔥 IMPORTANT: your joystick lives in this namespace

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public VirtualJoystick joystick;  

    void Update()
    {
        if (joystick == null) return;

        float h = joystick.GetAxis("Horizontal");
        float v = joystick.GetAxis("Vertical");

        Vector2 move = new Vector2(h, v);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
