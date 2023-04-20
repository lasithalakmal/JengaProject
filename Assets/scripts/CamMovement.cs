using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour {

    public Vector2 turn;

    public float rotation_rate;
    public float zooming_rate;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            turn.x += (Input.GetAxis("Mouse X") * rotation_rate);
            turn.y += (Input.GetAxis("Mouse Y") * rotation_rate);
            transform.parent.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            Camera.main.fieldOfView -= zooming_rate;
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            Camera.main.fieldOfView += zooming_rate;
        }
    }
}
