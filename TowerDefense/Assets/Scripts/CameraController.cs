using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 30f;
    private float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minZoomIn = 10f;
    public float maxZoomOut = 80f;

	// Update is called once per frame
	void Update () {

        if (GameManager.gameIsOver) {
            this.enabled = false;
            return;
        }

        /*Camera movment via keycodes and mouse position*/
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            //Vector3.forward = new Vector3(0f, 0f, 1f)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
            //Vector3.forward = new Vector3(0f, 0f, 1f)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
            //Vector3.forward = new Vector3(0f, 0f, 1f)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
            //Vector3.forward = new Vector3(0f, 0f, 1f)
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        /*Scroll wheel zoom movement*/
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 /*scroll values are teeny tiny*/ * scrollSpeed * Time.deltaTime;
        //Clamp values
        pos.y = Mathf.Clamp(pos.y, minZoomIn, maxZoomOut);
        //apply position
        transform.position = pos;
    }
}
