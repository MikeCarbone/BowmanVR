using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		ArrowManager.Instance.StopArrow ();
	}
}
