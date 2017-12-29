using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Arrow : MonoBehaviour {

	private bool isAttached = false;

	private bool isFired = false;

	void OnTriggerStay() {
		AttachArrow ();
	}

	void OnTriggerEnter(Collider other) {
		// call stopArrow in this function rather than in the update of the arrow manager. Performance
		Appendage appendage = AppendageFactory.createAppendage(other.gameObject.name);
		if (appendage != null) {
			print (appendage.Damage);
			ArrowManager.Instance.StopArrow ();
		}
		AttachArrow ();

	}

	void Update() {
		if (isFired && transform.GetComponent<Rigidbody> ().velocity.magnitude > 1f) {
			transform.LookAt (transform.position + transform.GetComponent<Rigidbody> ().velocity);
		}
	}

	public void Fired() {
		isFired = true;
	}

	private void AttachArrow () {
		var device = SteamVR_Controller.Input ((int)ArrowManager.Instance.trackedObj.index);
		if (!isAttached && device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
			ArrowManager.Instance.AttachBowToArrow ();
			isAttached = true;
		} 
	}

}