using UnityEngine;
using System.Collections;

public class CrushScrap : MonoBehaviour {
	
	public string[] ValidInputs;
	public GameObject Output;
	public int ratio = 5;

	void OnTriggerEnter(Collider other) {
		if (isValidInput (other.gameObject)) {
			//it's scrap. crush it
			//Destroy (other.gameObject);

		} else {
			// don't process
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
