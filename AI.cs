using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {

	public GameObject aiCharacter;
	public GameObject arrowPrefab;
	private GameObject aiArrow;
	public GameObject tracer2;
	public GameObject motioncontrollerRight;
	public ArrowManager arrowManager;
	private Vector3 aimDirection;
	private float enemyDistance;
	public bool isAiArrowFired = false;

	public float MinX = -100;
	public float MaxX = 100;
	public float MinZ = -100;
	public float MaxZ = 100;
	public float MinA = 2;
	public float MaxA = 4;


	void AiArrowSpawn (){
		//Spawns and sets AI Arrow's direction
		aiArrow = Instantiate (arrowPrefab);
		aiArrow.transform.position = aiCharacter.transform.position;
		aiArrow.transform.rotation = Quaternion.LookRotation (aimDirection);
		aiArrow.transform.position = new Vector3 (aiArrow.transform.position.x, aiArrow.transform.position.y + 2, aiArrow.transform.position.z);
		print ("AI arrow spawned");
	}

	void MoveAI (){
		float x = Random.Range (MinX, MaxX);
		float z = Random.Range (MinZ, MaxZ);
		aiCharacter.transform.position = new Vector3 (x+15, 0, z+15);
		print ("AI is located at " + aiCharacter.transform.position);
	}

		
	void Start () {
		MoveAI ();

		arrowManager = motioncontrollerRight.GetComponent<ArrowManager> ();
		this.enemyDistance = Vector3.Distance(arrowManager.arrowStartPoint.transform.position, aiCharacter.transform.position);
		print (enemyDistance);

		aiArrow = Instantiate (arrowPrefab);
		aiArrow.transform.position = aiCharacter.transform.position;
		Vector3 targetDir = arrowManager.arrowStartPoint.transform.position - aiArrow.transform.position;
		aimDirection = Vector3.RotateTowards (aiArrow.transform.forward, targetDir, 100, 0.0f);
		aiCharacter.transform.rotation = Quaternion.LookRotation (aimDirection);

		//Debug.DrawRay (aiArrow.transform.position, aimDirection, Color.red);
	}
		
	void Update () {
		//if (arrowManager.isFired == true) {
		//	tracer2.transform.position = aiArrow.transform.position;
		//	Instantiate (tracer2);
	//}

		if (arrowManager.aiTurn == true && isAiArrowFired == false) {
			
			AiArrowSpawn();
			float randomForce = Random.Range (MinA, MaxA);
			Rigidbody r = aiArrow.GetComponent<Rigidbody> ();
			r.velocity = aiArrow.transform.forward * (enemyDistance*2);
			print ("AI Velocity is " + r.velocity);
			r.useGravity = true;

			if (isAiArrowFired && aiArrow.GetComponent<Rigidbody> ().velocity.magnitude > 1f) {
				aiArrow.transform.LookAt (aiArrow.transform.position + aiArrow.transform.GetComponent<Rigidbody> ().velocity);
			}

			arrowManager.aiTurn = false;
			//isAiArrowFired = true;
			arrowManager.isFired = false;

		}

	}
}
