using UnityEngine;
using System.Collections;

public class CrushScrap : MonoBehaviour {
	
	public string[] ValidInputs;
	public GameObject typeOutputObject;
	public float proccessSpeed = 0.1f;

	public Transform inTran;
	public Transform outTran;
	public Transform dropTran;
	private Vector3 inStart;

	private bool crushing = false;
	private float crushAmount = 0;
	private bool producing = false;
	private float produceAmount = 0;
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
			crushAmount = 0;
			produceAmount = 0;
			input = other.transform;
			inStart = input.position;
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

			produceAmount += proccessSpeed;
			output.transform.position = Vector3.Lerp (outTran.position, dropTran.position, produceAmount);

			if (produceAmount >= 1f) {
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
			crushAmount += proccessSpeed;
			input.transform.position = Vector3.Lerp(inStart, inTran.position, crushAmount);

			if(crushAmount >= 1.0f) {
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
