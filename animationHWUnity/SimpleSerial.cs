using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO.Ports;
using System.Threading;

public class SimpleSerial : MonoBehaviour {

	Animator anim;
	private SerialPort serialPort = null ;
	private String portName = "/dev/cu.usbmodem1411";
	private int baudRate = 115200;
	private int readTimeOut = 100;

	private string serialInput;

	bool programActive = true;
	Thread thread;

	void Start()
	{
		anim = GetComponent<Animator> ();
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

	void Update()
	{
		if (serialInput != null)
		{
			string[] strEul = serialInput.Split(',');
			if (strEul.Length > 0)
			{
				float embroideredSensor = float.Parse(strEul[0]);
//				float potB = float.Parse(strEul[1]);
//				potA = (potA - 512) / 512;
//				potB = (potB - 512) / 512;
//				this.transform.rotation = Quaternion.Euler(new Vector3(90.0f * potA, 90.0f * potB, 0f));
//				if (int.Parse(strEul[2]) != 0)
//				{
//					this.GetComponent<MeshRenderer>().enabled = true;
//				}
//				else
//				{
//					this.GetComponent<MeshRenderer>().enabled = false; 
//				}
				Debug.Log(embroideredSensor) ;
				if (embroideredSensor > 850) {
					anim.SetBool ("singleFinger", false);
					anim.SetBool ("doubleFinger", false);
					anim.SetBool ("tripleFinger", false);
				}
				else if (embroideredSensor > 550) {
					anim.SetBool ("singleFinger", true);
					anim.SetBool ("doubleFinger", false);
					anim.SetBool ("tripleFinger", false);
				} else if (embroideredSensor > 400) {
					anim.SetBool ("singleFinger", false);
					anim.SetBool ("doubleFinger", true);
					anim.SetBool ("tripleFinger", false);	
				} else if (embroideredSensor > 0) {
					anim.SetBool ("singleFinger", false);
					anim.SetBool ("doubleFinger", false);
					anim.SetBool ("tripleFinger", true);
				}
			}
		}
	}

	public void OnDisable()
	{
		programActive = false;
		if (serialPort != null && serialPort.IsOpen)
			serialPort.Close();
	}
}
