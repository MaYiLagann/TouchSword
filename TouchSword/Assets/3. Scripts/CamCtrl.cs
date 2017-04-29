using UnityEngine;
using System.Collections;

public class CamCtrl : MonoBehaviour {

	[Header("Scale Shake Settings")]
	public float ScaleRange = 1f;
	public float ScaleTime = 1f;

	private float originalScale;
	private float tscale = 0;

	// Use this for initialization
	void Start () {
		originalScale = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		ScaleCtrl ();
	}

	public void ScaleShake(){
		tscale = 1f;
	}

	void ScaleCtrl(){
		if (tscale > 0) {
			Camera.main.orthographicSize = Mathf.Lerp (originalScale, originalScale - ScaleRange, tscale);
			tscale -= Time.deltaTime / ScaleTime;
		} else {
			Camera.main.orthographicSize = originalScale;
		}
	}
}
