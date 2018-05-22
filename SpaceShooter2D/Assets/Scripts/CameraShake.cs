using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Health.OnDie += Shake;

	}
	
	// Update is called once per frame
	void Shake () {
        transform.DOShakePosition(1, new Vector3(0.1f, 0, 0), 40, 90, false);
	}
    private void OnDestroy()
    {
        Health.OnDie -= Shake;

    }
}
