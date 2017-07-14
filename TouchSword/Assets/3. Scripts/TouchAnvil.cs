using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchAnvil : MonoBehaviour {

	[Header("Beat Settings")]
	public Vector2 Beat = new Vector2 (1f, 2f);
	[Range(0, 1)]
	public float HalfBeat = 0;
	public float BeatSuccess = 0.2f;
	public Slider BeatSlider;

	[Header("Game Settings")]
	public float TouchDelay = 0.1f;
	public Text HitText;

	[Header("Audio Settings")]
	public AudioSource Anvil;
	public Vector2 SoundRange = new Vector2(0.8f, 1.2f);
	public AudioClip[] AnvilSounds;

	[Header("Light Settings")]
	public Light SparkLight;
	public float SparkRange = 3f;
	public float SparkDestroySpeed = 1f;

	[Header("Effect Settings")]
	public GameObject Spark;
	public Animator Hammer;
	public int SparkNum = 10;
	public float SparkForce = 5f;

	private float tdelay = 0;
	private int hit = 0;
	private float currentBeat;

	// Use this for initialization
	void Start () {
		currentBeat = BeatSuccess;
	}
	
	// Update is called once per frame
	void Update () {
		if (tdelay <= 0 && Input.GetMouseButtonDown (0)) {
			tdelay = TouchDelay;
			OnClick ();
		} else
			tdelay -= Time.deltaTime;
		
		LightCtrl ();
		HitCtrl ();
	}

	void OnClick() {
		if (currentBeat > BeatSuccess) {
			// Beat Fail
			hit = 0;
			currentBeat = -BeatSuccess;
			return;
		}
		// Sound
		Anvil.pitch = Random.Range (SoundRange.x, SoundRange.y);
		int snd = (int)(Random.Range (0, AnvilSounds.Length));
		Anvil.clip = AnvilSounds[snd];
		Anvil.Play ();

		// Effect
		SparkLight.range = SparkRange;
		SparkCreate ();
		gameObject.GetComponent<CamCtrl> ().ScaleShake ();
		Hammer.SetTrigger ("Smash");

		// GameSystem
		hit++;
		currentBeat = Beat.x / Beat.y;
		if (Random.Range (0f, 1f) < HalfBeat)
			currentBeat /= 2;
	}

	void LightCtrl() {
		if (SparkLight.range > 0) {
			SparkLight.range -= SparkRange / SparkDestroySpeed * Time.deltaTime;
		}
	}

	void SparkCreate() {
		for (int i = 0; i < SparkNum; i++) {
			GameObject obj = Instantiate (Spark);
			Vector3 force = new Vector3 (Random.Range (-SparkForce, SparkForce), SparkForce, Random.Range (-SparkForce, SparkForce));
			obj.GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);
			Destroy (obj, 3f);
		}
	}

	void HitCtrl(){
		if (currentBeat <= -BeatSuccess) {
			// Beat Fail
			hit = 0;
		} else {
			currentBeat -= Time.deltaTime;
		}
		HitText.text = hit == 0 ? "" : hit.ToString () + " Hit!";
		BeatSlider.value = currentBeat / (Beat.x / Beat.y);
	}

}
