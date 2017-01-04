using UnityEngine;
using System.Collections;

public class SimpleFastForward : MonoBehaviour {

	public float fastForwardAmmount;
	public KeyCode codeForFF;

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(codeForFF)) {
			Time.timeScale = fastForwardAmmount;
		}
		else {
			Time.timeScale = 1.0f;
		}
	}
}
