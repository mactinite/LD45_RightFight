using UnityEngine;
using System.Collections;

public class BackAndForth_Demo : MonoBehaviour {
	
	Quaternion startRotation;
	public float xOscilation = 10;
	public float period = 2;
	
	// Use this for initialization
	void Start () {
		startRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = startRotation;
		transform.Rotate(transform.up,Mathf.Sin(Time.time*Mathf.PI/period) * xOscilation);
	}
}
