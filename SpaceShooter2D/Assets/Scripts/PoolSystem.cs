using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolTypes {
	score,
	laserPlayer,
    laserEnemy
}

public class PoolSystem : MonoBehaviour {

	static Dictionary <PoolTypes, List <GameObject>> pooling = new Dictionary<PoolTypes, List<GameObject>>();

	static void Initialize () {
		pooling.Add (PoolTypes.score, new List<GameObject> ());
		pooling.Add (PoolTypes.laserPlayer, new List<GameObject> ());
        pooling.Add (PoolTypes.laserEnemy, new List<GameObject>());
    }

    static GameObject GetElement (PoolTypes type) {
		if (pooling.Count == 0)
			Initialize ();

		if (pooling [type].Count > 0) {
			GameObject element = pooling [type] [0];
			pooling [type].Remove (element);
			element.SetActive (true);
			return element;
		} 
			
		return null;
	}

	public static void AddElementToPool (PoolTypes type, GameObject element) {
		pooling [type].Add (element);
		element.SetActive (false);
	}

	public static GameObject SpawnObject (GameObject prefab, PoolTypes type, Vector2 position) {
		GameObject spawnObject = PoolSystem.GetElement(type);
		if (spawnObject == null) {
			spawnObject = Instantiate (prefab);
			spawnObject.transform.position = position;

		}
		return spawnObject;
	}

}
