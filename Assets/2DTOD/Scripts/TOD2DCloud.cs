/// <summary>
/// Time of Day 2D Cloud Script
/// by "Grownup Games"
/// 
/// contact of via our website thedwarves.org
/// or via e-mail nenadradojcic@yahoo.com
/// 
/// THIS SCRIPT CREATES CLOUD FOR WEATHER IN YOUR GAME. BEST FOR YOU IS TO USE PREFAB FROM "PREFABS" IN YOUR PROJECTS DIRECTORY.
/// 
/// </summary>
using UnityEngine;
using System.Collections;

[AddComponentMenu("Grownup Games/Time Of Day 2D/Time of Day 2D Cloud Script")]
[RequireComponent(typeof(SpriteRenderer))]
public class TOD2DCloud : MonoBehaviour { //
	
	public Sprite[] cloudSprites;
	private int randomCloudSprite;
	public Sprite usedCloudSprite;

	private int randomValue;
	private int isNegative;

	private float cloudFader = 0.0f;

	private SpriteRenderer sp;
	private TimeOfDay2DScript tod2d;

	void Start() {

		sp = GetComponent<SpriteRenderer>();
		tod2d = GameObject.FindGameObjectWithTag("2DTOD").GetComponent<TimeOfDay2DScript>();

		randomCloudSprite = Random.Range(0, cloudSprites.Length);
		usedCloudSprite = cloudSprites[randomCloudSprite];
		sp.sprite = usedCloudSprite;

		randomValue = Random.Range(1, 10);
		isNegative = Random.Range(0, 100);

		transform.position = new Vector2(tod2d.transform.position.x + (Random.Range(-10f, 10f)), tod2d.transform.position.y + (Random.Range(-2.7f, 5f)));

		if(isNegative < 50) {
			transform.localScale = new Vector3(randomValue, randomValue, 1f);
		}
		else {
			transform.localScale = new Vector3(-randomValue, randomValue, 1f);
		}
	}

	void Update() {
		CloudCalculations();
	}

	void CloudCalculations() {

		sp.material.color = new Color(1f, 1f, 1f, cloudFader); //We set cloudFader variable as alpha mask of our renderers material

		float cloudPosX = transform.position.x;
		float tod2dPosx = tod2d.transform.position.x;

		if(cloudPosX < tod2dPosx - 10f) { //This value here (in this case 10f) sets when will cloud reset its position. If clouds x position is more than main Tod2d object position more than -10f, it resets his position to the begining.
			transform.position = new Vector2(tod2d.transform.position.x + 10f, tod2d.transform.position.y + (Random.Range(-2.7f, 5f)));
		}

		if(tod2d.currentWeatherIs == CurrentWeatherIs.Clear) { //If current weather is Clear fade our alpha out and move slowly.
			if(cloudFader > 0f) {
				cloudFader -= Time.deltaTime/2; //Divided by two so it would be more realistic transition;
			}
			transform.position = new Vector2(transform.position.x - tod2d.calmCloudSpeed * Time.deltaTime, transform.position.y);
		}
		if(tod2d.currentWeatherIs == CurrentWeatherIs.Rain) {
			if(cloudFader < 1.0f) {
				cloudFader += Time.deltaTime/2; //Divided by two so it would be more realistic transition;
			}
			transform.position = new Vector2(transform.position.x - tod2d.calmCloudSpeed * Time.deltaTime, transform.position.y);
		}
		if(tod2d.currentWeatherIs == CurrentWeatherIs.Storm) {
			if(cloudFader < 1.0f) {
				cloudFader += Time.deltaTime/2; //Divided by two so it would be more realistic transition;
			}
			transform.position = new Vector2(transform.position.x - tod2d.stormCloudSpeed * Time.deltaTime, transform.position.y);
		}
		if(tod2d.currentWeatherIs == CurrentWeatherIs.Snow) { //If current weather is Clear fade our alpha out and move slowly.
			if(cloudFader > 0f) {
				cloudFader -= Time.deltaTime/2; //Divided by two so it would be more realistic transition;
			}
			transform.position = new Vector2(transform.position.x - tod2d.calmCloudSpeed * Time.deltaTime, transform.position.y);
		}
	}
}
