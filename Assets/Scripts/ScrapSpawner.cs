using UnityEngine;
using System.Collections;

public class ScrapSpawner : MonoBehaviour {

	public float SpawnRadius = 5;
	public float Spawnfreq = 10;
	public float SpawnChance = 0.5f;
	public GameObject[] SpawnObjects;
	
	private Transform spawnCenter;

	// Use this for initialization
	void Start () {
		spawnCenter = this.transform;

		InvokeRepeating ("Spawn", Spawnfreq, Spawnfreq);
	}
	
	void Spawn () {
		if(Random.value <= SpawnChance) {
			Vector3 rand = Random.insideUnitCircle * SpawnRadius;
			rand.z = rand.y;
			rand.y = 0;
			Vector3 pos = spawnCenter.position + rand;
			int i = Random.Range(0, SpawnObjects.Length);
			Quaternion rot = Random.rotation;
			Instantiate(SpawnObjects[i],pos, rot);
		}
	}
}
