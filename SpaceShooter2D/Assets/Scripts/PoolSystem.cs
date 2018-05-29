using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolTypes {
	score,
	laserPlayer,
    laserEnemy,
    misile
}

public class PoolSystem : MonoBehaviour {

    //Diccionario que contiene el tipo de Pooltype y una lista de GameObects
	static Dictionary <PoolTypes, List <GameObject>> pooling = new Dictionary<PoolTypes, List<GameObject>>();

	static void Initialize () {
        //Añadimos cada tipo de pooltype creando una lista nueva
		pooling.Add (PoolTypes.score, new List<GameObject> ());
		pooling.Add (PoolTypes.laserPlayer, new List<GameObject> ());
        pooling.Add (PoolTypes.laserEnemy, new List<GameObject>());
        pooling.Add (PoolTypes.misile, new List<GameObject>());

    }

    static GameObject GetElement (PoolTypes type) {
        //si es la primera vez que se ejecuta se inicializa
		if (pooling.Count == 0)
			Initialize ();

        //Si hay algún elemento de ese tipo lo combrueba, lo activa y lo devuelve
		if (pooling [type].Count > 0) {
			GameObject element = pooling [type] [0];
			pooling [type].Remove (element);
			element.SetActive (true);
			return element;
		} 
			
		return null;
	}

	public static void AddElementToPool (PoolTypes type, GameObject element) {
        //el objecto se añade al pool y se desactiva
		pooling [type].Add (element);
		element.SetActive (false);
	}

	public static GameObject SpawnObject (GameObject prefab, PoolTypes type, Vector2 position) {

        //Buscamos un objeto de ese tipo en el pool
		GameObject spawnObject = PoolSystem.GetElement(type);

        //Si no hay ninguno lo creamos
		if (spawnObject == null) {
			spawnObject = Instantiate (prefab);
		}

        //Lo posicionamos en el spwnpoint y lo activamos
		spawnObject.transform.position = position;
        spawnObject.SetActive(true);
		return spawnObject;
	}

    //Añadir una sobrecarga de la fución spawnObject pasando el parametro de la rotación
    public static GameObject SpawnObject(GameObject prefab, PoolTypes type, Vector2 position,Quaternion rotation)
        {
            GameObject spawnObject = SpawnObject(prefab,type,position);
            spawnObject.transform.rotation = rotation;
            return spawnObject;
        }
    }
