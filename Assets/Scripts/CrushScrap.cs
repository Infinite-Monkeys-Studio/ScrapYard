using UnityEngine;
using System.Collections;

public class CrushScrap : MonoBehaviour {
	
	public string[] ValidInputs;
	public GameObject typeOutputObject;
	public float proccessSpeed = 5;

	public Transform inTran;
	public Transform outTran;
	public Transform dropTran;

	private bool crushing = false;
	private bool producing = false;
	private Transform input;
	private Rigidbody output;

	void OnTriggerEnter(Collider other) {
		if (crushing || producing) { //TODO this is wrong
			return;
		}

		if (isValidInput (other.gameObject)) {
			//it's scrap. crush it
			//Destroy (other.gameObject);

			//pull input into machine
			crushing = true;
			producing = true;
			input = other.transform;
			other.attachedRigidbody.useGravity = false;
			other.enabled = false;

			Collider[] cols = other.GetComponents<Collider>();
			foreach (Collider c in cols) {
				c.enabled = false;
			}
			//push output out.
		} else {
			// don't process
		}
	}

	void Update() {
		if (producing) {
			if (output == null) {
				output = Instantiate (typeOutputObject).GetComponent<Rigidbody> ();
				output.transform.position = outTran.position;
				output.useGravity = false;

				Collider[] cols = output.GetComponents<Collider>();
				foreach (Collider c in cols) {
					c.enabled = false;
				}
			}

			output.transform.position = Vector3.Lerp (output.transform.position, dropTran.position, Time.deltaTime * proccessSpeed);

			if (Vector3.Distance (output.transform.position, dropTran.position) < 0.25f) {
				//drop it
				output.useGravity = true;
				producing = false;
				Collider[] cols = output.GetComponents<Collider>();
				foreach (Collider c in cols) {
					c.enabled = true;
				}
				output = null;
			}
		}

		if(crushing) {
			input.transform.position = Vector3.Lerp(input.transform.position, inTran.position, Time.deltaTime * proccessSpeed);

			if(Vector3.Distance(input.transform.position, inTran.position) < 1.0f) {
				//it's gone
				Destroy (input.gameObject);
				crushing = false;
			}
		}
	}

	bool isValidInput(GameObject obj) {
		foreach(string inp in ValidInputs) {
			if (obj.name == string.Format("{0}(Clone)", inp)) {
				return true;
			}
		}
		return false;
	}
	
}
