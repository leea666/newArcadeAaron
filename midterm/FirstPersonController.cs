using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO.Ports;
using System.Threading;

[RequireComponent (typeof (gravityBody))]
public class FirstPersonController : MonoBehaviour {

	private SerialPort serialPort = null ;
	private String portName = "/dev/cu.usbmodem1411";
	private int baudRate = 115200;
	private int readTimeOut = 100;

	private string serialInput;

	bool programActive = true;
	Thread thread;

	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 6;
//	public float jumpForce = 220;
	public LayerMask groundedMask;
	public float inputX = 0 ;
	public float inputY = 0 ;

	// System vars
//	bool grounded;
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbody;


	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody> ();
	}

	void Start() {
		try
		{
			serialPort = new SerialPort();
			serialPort.PortName = portName;
			serialPort.BaudRate = baudRate;
			serialPort.ReadTimeout = readTimeOut;
			serialPort.Open();
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
		thread = new Thread(new ThreadStart(ProcessData));
		thread.Start();
	}

	void ProcessData()
	{
		Debug.Log("Thread: Start");
		while (programActive)
		{
			try
			{
				serialInput = serialPort.ReadLine();
			}
			catch (TimeoutException)
			{

			}
		}
		Debug.Log("Thread: Stop");
	}

	void Update() {

		if (serialInput != null)
		{
			string[] strEul = serialInput.Split(',');
			if (strEul.Length > 0)
			{
				float switch1 = float.Parse(strEul[0]);
				float switch2 = float.Parse(strEul[1]);
				float switch3 = float.Parse(strEul[2]);
				float switch4 = float.Parse(strEul[3]);
				float switch5 = float.Parse(strEul[4]);
				float switch6 = float.Parse(strEul[5]);
				float switch7 = float.Parse(strEul[6]);
				float switch8 = float.Parse(strEul[7]);
				if (switch7 == 0) {
					inputX = 0 ;
					inputY = -1;
				}
				if (switch8 == 0) {
					inputX = 1 ;
					inputY = -1;
				}
				if (switch1 == 0) {
					inputX = 1 ;
					inputY = 0;
				}
				if (switch2 == 0) {
					inputX = 1 ;
					inputY = 1;
				}
				if (switch3 == 0) {
					inputX = 0 ;
					inputY = 1;
				}
				if (switch4 == 0) {
					inputX = -1 ;
					inputY = 1;
				}
				if (switch5 == 0) {
					inputX = -1 ;
					inputY = 0;
				}
				if (switch6 == 0) {
					inputX = -1 ;
					inputY = -1;
				}

				if (switch1 == 1 && switch2 == 1 && switch3 == 1 && switch4 == 1 && switch5 == 1 && switch6 == 1 && switch7 == 1 && switch8 == 1) {
					inputX = 0;
					inputY = 0;
				}

			}
		}

		// Look rotation:
//		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
//		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
//		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
//		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

		// Calculate movement:
//		float inputX = Input.GetAxisRaw("Horizontal");
//		float inputY = Input.GetAxisRaw("Vertical");


		Vector3 moveDir = new Vector3(inputX,0, inputY).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);

		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

//		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask)) {
//			grounded = true;
//		}
//		else {
//			grounded = false;
//		}

	}

	void FixedUpdate() {
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
	}

	public void OnDisable()
	{
		programActive = false;
		if (serialPort != null && serialPort.IsOpen)
			serialPort.Close();
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
		}
//		Destroy(other.gameObject) ;
	}
}
