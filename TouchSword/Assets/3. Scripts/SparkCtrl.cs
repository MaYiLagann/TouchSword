using UnityEngine;
using System.Collections;

public class SparkCtrl : MonoBehaviour {

	public Color StartColor = Color.white;
	public Color EndColor = Color.black;
	public float ChangeTime = 1f;

	private float time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.Lerp (StartColor, EndColor, time);
		if (time < 1) {
			time += Time.deltaTime / ChangeTime;
		}
	}
}
