using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    float alpha = 0;
    float hor, ver;
    private void FixedUpdate()
    {


        if (Input.touchCount > 0)
        {
            if (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) < 2f) { hor = 0f; }
            if (Mathf.Abs(Input.GetTouch(0).deltaPosition.y) < 2f) { ver = 0; }
            if (Input.GetTouch(0).deltaPosition.x > 10.0f) { hor = 1f; }
            if (Input.GetTouch(0).deltaPosition.x < -10.0f) { hor = -1f; }
            if (Input.GetTouch(0).deltaPosition.y > 10f) { ver = 1f; }
            if (Input.GetTouch(0).deltaPosition.y < -10.0f) { ver = -1f; }


            gameObject.GetComponent<Rigidbody>().velocity += Vector3.right * hor * Time.deltaTime * speed;
            gameObject.GetComponent<Rigidbody>().velocity += Vector3.forward * ver * Time.deltaTime * speed;
            gameObject.GetComponent<Rigidbody>().velocity *= 0.85f;

            alpha = Mathf.Atan2(hor, ver);
            transform.rotation = Quaternion.EulerAngles(0, Mathf.PI + alpha, 0);

        }
    }

}
