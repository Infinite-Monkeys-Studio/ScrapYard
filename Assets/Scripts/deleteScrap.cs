using UnityEngine;
using System.Collections;

public class deleteScrap : MonoBehaviour {
	
	public string[] ValidInputs;

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Testing...");
		if (isValidInput (other.gameObject)) {
			Debug.Log ("Valid. destroy");
			Destroy (other.gameObject);
		} else {
			Debug.Log("invalid");
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
