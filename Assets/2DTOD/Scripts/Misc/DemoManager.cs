using UnityEngine;
using System.Collections;

public class DemoManager : MonoBehaviour {

	public TimeOfDay2DScript tod2d;
	
	void Start() {
		tod2d = GameObject.FindGameObjectWithTag("2DTOD").GetComponent<TimeOfDay2DScript>();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Z)) {
			tod2d.currentWeatherIs = CurrentWeatherIs.Clear;
		}
		if(Input.GetKeyDown(KeyCode.X)) {
			tod2d.currentWeatherIs = CurrentWeatherIs.Rain;
		}
		if(Input.GetKeyDown(KeyCode.C)) {
			tod2d.currentWeatherIs = CurrentWeatherIs.Storm;
		}
		if(Input.GetKeyDown(KeyCode.V)) {
			tod2d.currentWeatherIs = CurrentWeatherIs.Snow;
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 250, 500), "Press : \n 'Z' to set weather to CLEAR \n 'X' to set weather to RAIN \n 'C' to set weather to STORM \n 'V' to set weather to SNOW \n\n\n Press J to speed up time");
		GUI.Label(new Rect(Screen.width - 250, 10, 250, 500), "'2D Time of Day' is a fully customizable Unity plugin that gives you dynamic day and night cycle together with complete weather, moon phases and calendar systems." +
			"\n Version 1.5 features great assortment of features that can make your 2d game even more appealing");
	}
}
