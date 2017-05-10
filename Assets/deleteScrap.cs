using UnityEngine;
using System.Collections;

public class deleteScrap : MonoBehaviour {

	//public Collider ProcessArea;


	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}
	
}
