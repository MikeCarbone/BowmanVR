using UnityEngine;
using System.Collections;

public class ArrowManager : MonoBehaviour {

	public static ArrowManager Instance;
	public SteamVR_TrackedObject trackedObj;
	public SteamVR_TrackedObject leftController;

	//Vars
	private GameObject currentArrow;
	public GameObject stringAttachPoint;
	public GameObject arrowStartPoint;
	public GameObject stringStartPoint;
	public GameObject arrowPrefab;
	public GameObject tracer;
	public GameObject aiChar;
	public GameObject cameraRig;
	private ArrayList tracerList;

	public GameObject tracerRef;

	//Script References
	public Arrow arrowScript;
	public AI aiScript;

	//States
	private bool isAttached = false;
	public bool isFired = false;
	public bool aiTurn = false;
	public bool isArrowSpawned = false;

	void Awake() {
		if (Instance == null)
			Instance = this;
	}
	//void OnDestroy() {
	//	if (Instance == this)
	//		Instance = null;
	//}
	// Use this for initialization
	void Start () {
		AttachArrow ();
		tracerList = new ArrayList ();
	}

	public void StopArrow() {
		if (isFired == true){
		Rigidbody arrowVelocity = currentArrow.GetComponent<Rigidbody> ();
	
		arrowScript = currentArrow.GetComponent<Arrow>();

		arrowVelocity.velocity = Vector3.zero;
		arrowVelocity.useGravity = false;

		cameraRig.transform.position = currentArrow.transform.position;

		AttachArrow ();
		aiTurn = true;
		isFired = false;
		//print ("arrow velocity is ");
		}
	}

	public void DrawTracer (){
		if (aiTurn == false && isAttached == false && isArrowSpawned == false) {
			tracerRef = Instantiate (tracer);
			tracerRef.transform.position = currentArrow.transform.position;
			tracerList.Add (tracerRef);
		}
	}

	private void DeleteTracer() {
		if (tracerList.Count != 0) {
			for (int i = 0; i < tracerList.Count; i++) {
				Destroy ((GameObject) tracerList [i]);
			}
			tracerList.Clear ();
		}
	}

	// Update is called once per frame
	void Update () {
			PullString ();
		DrawTracer ();
	}
		
	private void PullString() {
		if (isAttached) {
			float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;
			stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3 (5f* dist, 0f, 0f);

			var device = SteamVR_Controller.Input((int)trackedObj.index);
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
				Fire ();
			}
		}
	}

	private void Fire() {
		float dist = (stringStartPoint.transform.position - trackedObj.transform.position).magnitude;

		currentArrow.transform.parent = null;
		currentArrow.GetComponent<Arrow> ().Fired ();

		Rigidbody r = currentArrow.GetComponent<Rigidbody> ();
		r.velocity = currentArrow.transform.forward * 30f * dist;
		r.useGravity = true;

		currentArrow.GetComponent<Collider> ().isTrigger = false;

		stringAttachPoint.transform.position = stringStartPoint.transform.position;

		isFired = true;
		isAttached = false;
		isArrowSpawned = false;

		DeleteTracer ();
	}

	public void AttachArrow() {
		if (aiTurn == false && isArrowSpawned == false) {
			currentArrow = Instantiate (arrowPrefab);
			currentArrow.transform.parent = trackedObj.transform;
			currentArrow.transform.localPosition = new Vector3 (0f, 0f, .33f);
			currentArrow.transform.localRotation = Quaternion.identity;
			isArrowSpawned = true;
		}
	}

	public void AttachBowToArrow() {
		currentArrow.transform.parent = stringAttachPoint.transform;
		currentArrow.transform.localPosition = arrowStartPoint.transform.localPosition;
		currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

		isAttached = true;
	}
}
