using UnityEngine;
using System.Collections;

public class Carry : MonoBehaviour {

	public int wpnRange = 50;
	public string carriableTag = "Carriable";
	public Transform hands;

	private bool carrying = false;
	private Transform carried;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (Camera.main.transform.position, Camera.main.transform.forward);
		if (Input.GetMouseButtonDown (0)) {
			if(carrying) {
				carrying = false;
				hands.GetComponentInChildren<Rigidbody> ().useGravity = true;
				hands.GetComponentInChildren<Rigidbody> ().isKinematic = false;
				carried.SetParent(null);
				carried = null;
			} else {
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit, wpnRange)) {
					Debug.Log(hit.transform.tag);
					if (hit.transform.tag == carriableTag) {
						carrying = true;
						hit.rigidbody.useGravity = false;
						hit.rigidbody.isKinematic = true;
						carried = hit.rigidbody.transform;
						carried.position = hands.position;
						carried.SetParent(hands);
					}
				}
			}
		}
	}
}
