using UnityEngine;
using System.Collections;

public class JointCarry : MonoBehaviour {

	public int reach = 5;
	public string carriableTag = "Carriable";
	public Rigidbody hands;


	private bool carrying = false;
	private bool grabbed = false;
	private Rigidbody carried;
	private Collider collider;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (carrying && !grabbed) {
			carried.transform.position = Vector3.Lerp(carried.transform.position, hands.transform.position, Time.deltaTime * 10);
			if(Vector3.Distance(carried.transform.position, hands.transform.position) < 0.25f) {
				pickUp();
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			if(carrying) {
				drop();
			} else {
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit, reach)) {
					if (hit.transform.tag == carriableTag) {
						grab(ref hit);
					}
				}
			}
		}
	}

	void pickUp() {
		grabbed = true;
		carried.useGravity = true;
		HingeJoint joint = carried.gameObject.AddComponent<HingeJoint>();
		joint.connectedBody = hands;
		joint.anchor = new Vector3(0, 0, 0);
		joint.axis = new Vector3(1, 1, 1);
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedAnchor = new Vector3(0, 0, 0);
		joint.useSpring = true;
		JointSpring spring = new JointSpring();
		spring.spring = 10;
		spring.damper = 0.5f;
		spring.targetPosition = 0;
		joint.enablePreprocessing = true;
		
		joint.spring = spring;
	}

	void grab(ref RaycastHit hit) {
		carrying = true;
		carried = hit.rigidbody;
		carried.useGravity = false;
		collider = hit.collider;
		collider.enabled = false;
	}

	void drop() {
		carried.useGravity = true;
		carrying = false;
		if (grabbed) {
			HingeJoint joint = carried.gameObject.GetComponent<HingeJoint> ();
			Destroy (joint);
			grabbed = false;
		}
		collider.enabled = true;
		carried = null;
		collider = null;
	}
}
