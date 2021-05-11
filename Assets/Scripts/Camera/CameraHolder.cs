using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour {

    CameraMovement camScript;

    private void Awake() {
        camScript = Camera.main.gameObject.GetComponent<CameraMovement>();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        /*cam.GetComponent<CameraMovement>().enabled = false;
        StartCoroutine(MoveCamera());*/
        camScript.overridePos = transform.position;
        camScript.ChangeMode();
        
    }

    public void OnTriggerExit2D(Collider2D col) {
        /*StopAllCoroutines();
        cam.GetComponent<CameraMovement>().enabled = true;*/

        camScript.overridePos = Vector3.zero;
        camScript.Invoke("ChangeMode", 0.5f);
    }

    /*IEnumerator MoveCamera() {
        while (cam.transform.position.x < transform.position.x - 0.01f) {
            cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position, 0.01f);
            yield return null;
        }

        yield return null;
    }
    */
}
