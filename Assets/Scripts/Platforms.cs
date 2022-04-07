using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

public class Platforms : MonoBehaviour
{
	enum PlatformType { moving, circular, falling, toggled }

	[SerializeField] PlatformType platformType;
	[SerializeField] float speed = 2f;
	[SerializeField] float howFar = 4f;
	[SerializeField] bool moveHor = true;
	[SerializeField] float fallTime = 0.5f;
	[SerializeField] float rotRadius = 5f;
	[SerializeField] float angSpeed = 2f;

	Vector3 initPos;
	bool moveBack = false;

	/*moving
	float speed = 2f;
	float howFar = 4f;
	bool moveHor = true;*/


	/*falling
	float fallTime = 0.5f;*/
	Rigidbody rb;

	/*circular
	Transform rotCenter;
	float rotRadius = 5f;
	float angSpeed = 2f; */
	float posX, posZ, angle = 0f;

	/*
	#region Editor
#if UNITY_EDITOR

	[CustomEditor(typeof(Platforms))]
	public class PlatTypeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

			Platforms platform = (Platforms)target;

			if(platform.platformType == PlatformType.moving)
            {
				platform.speed = EditorGUILayout.FloatField("Speed", platform.speed);
				platform.howFar = EditorGUILayout.FloatField("Distance", platform.howFar);
				platform.moveHor = EditorGUILayout.Toggle("Move sideways", platform.moveHor);
			}
			else if (platform.platformType == PlatformType.falling)
			{
				platform.fallTime = EditorGUILayout.FloatField("Fall time", platform.fallTime);
			}
            else if (platform.platformType == PlatformType.circular)
			{
				platform.rotRadius = EditorGUILayout.FloatField("Rotation radius", platform.rotRadius);
				platform.angSpeed = EditorGUILayout.FloatField("Angular speed", platform.angSpeed);
				//platform.rotCenter = EditorGUILayout.ObjectField("Rotation Center",
				//	platform.rotCenter, typeof(Transform), true) as Transform;
			}
		}
    }

#endif
	#endregion
	*/

	void Start()
	{
		initPos = transform.position;
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (platformType == PlatformType.moving)
		{
			if (moveHor)
			{
				if (transform.position.z > initPos.z+ howFar)
					moveBack = true;
				if (transform.position.z < initPos.z - howFar)
					moveBack = false;

				if (!moveBack)
					transform.position = new Vector3(transform.position.x, transform.position.y,
						transform.position.z + speed * Time.deltaTime);
				else
					transform.position = new Vector3(transform.position.x, transform.position.y,
						transform.position.z - speed * Time.deltaTime);
			}
			else
            {
				if (transform.position.y > initPos.y + howFar)
					moveBack = true;
				if (transform.position.y < initPos.y - howFar)
					moveBack = false;

				if (!moveBack)
					transform.position = new Vector3(transform.position.x,
						transform.position.y + speed * Time.deltaTime, transform.position.z);
				else
					transform.position = new Vector3(transform.position.x,
						transform.position.y - speed * Time.deltaTime, transform.position.z);
			}
		}
	
		else if (platformType == PlatformType.falling)
		{
			if (moveBack)
				transform.position = Vector3.MoveTowards(transform.position, initPos, 20f * Time.deltaTime);

			if (transform.position.y == initPos.y)
				moveBack = false;
		}
		else if (platformType == PlatformType.circular)
		{
			posX = initPos.x + Mathf.Cos(angle) * rotRadius;
			posZ = initPos.z + Mathf.Sin(angle) * rotRadius;
			transform.position = new Vector3(posX, transform.position.y, posZ);
			angle = angle + Time.deltaTime * angSpeed;

			if (angle >= 360f)
				angle = 0f;
		}

	}

	void OnTriggerEnter(Collider col)
	{
		if (platformType == PlatformType.falling)
		{
			if (col.gameObject.tag == "Player")
			{
				Invoke("DropPlatform", fallTime);
				//Destroy(gameObject, 2f);
			}
		}
		if(platformType == PlatformType.circular || platformType == PlatformType.moving)
        {
			if (col.gameObject.tag == "Player")
				col.transform.parent = this.transform;
		}
			
	}

	void OnTriggerExit(Collider col)
	{
		if (platformType == PlatformType.circular || platformType == PlatformType.moving)
		{
			if (col.gameObject.tag == "Player")
				col.transform.parent = null;
		}
	}

	void DropPlatform()
	{
		rb.isKinematic = false;
		Invoke("GetPlatformBack", 3f);
	}

	void GetPlatformBack()
	{
		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
		moveBack = true;
	}

}
