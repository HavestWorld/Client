/// <summary>
/// Time of Day 2D Base Script
/// by "Red Moose Games"
/// 
/// contact of via our website thedwarves.org
/// or via e-mail nenadradojcic@yahoo.com
/// 
/// THIS SCRIPT IS BASE TOD2D SCRIPT. USE PREFAB "2DTOD_GO" FROM "PREFABS" FOLDER IN YOUR PROJECT DIRECTORY
/// 
/// </summary>
using UnityEngine;
using System.Collections;

public enum DayLengthIs { //
	Fast = 0,
	Normal = 1,
	Slow = 2,
}

public enum WeatherIs {
	Static = 0,
	Dynamic = 1,
}

public enum CurrentWeatherIs {
	Clear = 0,
	Rain = 1,
	Storm = 2,
	Snow = 3,
}

public enum CurrentSeason {
	Spring = 0,
	Summer = 1,
	Autumn = 2,
	Winter = 3,
}

public enum CalendarMonth {
	January = 0,
	February = 1,
	March = 2,
	April = 3,
	May = 4,
	June = 5,
	July = 6,
	August = 7, 
	September = 8,
	October = 9,
	November = 10,
	December = 11,
}

public enum LunarPhases {
	NewMoon = 0,
	WaxingCrescent = 1,
	FirstQuarter = 2,
	WaxingGibbous = 3,
	FullMoon = 4,
	WaningGibbous = 5,
	LastQuarter = 6,
	WaningCrescent = 7,
}
[AddComponentMenu("Grownup Games/Time Of Day 2D/Time of Day 2D Base Script")]
public class TimeOfDay2DScript : MonoBehaviour {

	[Header("SKY")]
	[Tooltip("Do we want for sky to follow main camera tagged 'MainCamera'? If yes this must be checked.")]
	public bool skyFollowMainCamera = false; //Will the whole system follow your main camera?
	public string mainCameraTag = "MainCamera"; //The tag of your games main camera
	private Camera mainCam; //This fetches Camera component of gameobject whose tag is assigned in mainCameraTag
	[Tooltip("At what speed will the whole system move?")]
	public DayLengthIs dayLengthIs; //With this we can choose how fast will day transit to night and vice versa.
	private float dayLengthCalculator;
	private float dayLength = 10.0f; //If you are not scripting "guru" leave this be. Change pace with dayLenghtIs.
	private float dayTimer; //Timer for counting down passed time.
	private float celestialSpeed = 0.01f; //Speed on which sun and moon will move.
	[Header("Celestials")]
	public GameObject daySkyGO;	//This speak for itself.
	private SpriteRenderer daySkySR;
	public GameObject rainSkyGO;
	private SpriteRenderer rainSkySR;
	private float rainSkyFader = 1.0f;
	public GameObject moonGO;		//This is moon game object that has SpriteRenderer on it
	private SpriteRenderer currentMoonPhaseSR; //Here we will assign SpriteRenderer from moonGO at Awake function
	public Sprite[] moonPhases;
	public LunarPhases moonPhaseIs;
	public GameObject sunGO;		//}
	public GameObject starsGO;		//}
	public GameObject planetsGO;
	public Transform celestialTop; // This is the highest point that moon and sun will move to
	public Transform celestialBottom; //This is the point from where moon/sun will start moving when day/night starts
	private float dayNightFader = 1.0f; //Used to fade in and out alpha chanell of daySky sprite.
	[Header("Sun, moon and ambient lighting")]
	public float sunIntensity = 0.7f; //With what intensity the sun will shine
	private float sunIntensityBadWeather = 0.4f; //What will be intensity of the sun while it is raining or while it is storm out there
	public float moonIntensity = 0.1f; //What will be the intensity of the moon shining
	public Light sunLight; //Place Light component of your sun object here
	public Light moonLight; //Place Light component of your moon object here
	private bool night = false; //Here we check is it day or night
	[Header("-------------------------------")]
	[Space(15f)]
	[Header("WEATHER")]
	public WeatherIs weatherIs; //Is weather in your game static or dynamic
	public CurrentWeatherIs currentWeatherIs; //What is the current weather, clear, rain, storm or snow?
	[Header("Weather chances")]
	public int clearWeatherChance = 60;	//}
	public int rainWeatherChance = 50; 	//}
	public int stormWeatherChance = 25;	//}THOSE THREE MUST NEVEB BE OF SAME VALUE. EVERY ONE OF THEM NEEDS TO HAVE DIFFERENT VALUE FROM 0 TO 100
	public int snowWeatherChance = 10;	//}
	[Header("Season, calendar and day in the month")]
	public CurrentSeason currentSeasonIs;
	public CalendarMonth currentMonthIs;
	public int currentDayIs = 1;
	private bool dayCountedAlready = true;
	[Header("Particles and SFX")]
	public GameObject rainParticles;//}
	public GameObject snowParticles;//}Those sepaks for themselves
	public GameObject lightingGO;	//}
	private ParticleEmitter partSystem;
	private float lightingLastTime;
	public AudioClip rainClip; //Audio soud that will play when it is RAINING
	public AudioClip stormWindClip; //Audio sounds that will play will it is STORM weather
	public AudioClip[] thunderClip; //Audio sounds that will play randomly while it is STORM weather
	[Header("Particles values")]
	public float calmCloudSpeed = 0.5f; //What will be the moving speed for your clouds while it is raining?
	public float stormCloudSpeed = 1.0f; //What will be the moving speed for your clouds while it is storm?
	public float rainAmmount = 250; //This is default rain ammount while it is plain rain and cloudy day
	public float stormRainAmmount = 500; //This is default rain ammount while it is storm
	public float snowAmmount = 150; //This is default snow ammount while snow is falling
	private float changeWeatherCooldown = 5; //This is cooldown in seconds for when can weather change again
	private int randomWeatherNumb0; //}
	private int randomWeatherNumb1; //}These 3 ints are responsible for calculation of current weather type if dynamic weather is activated
	private int randomWeatherNumb3; //}


	void Awake() {

		rainParticles.GetComponent<ParticleEmitter>().maxEmission = 0;
		snowParticles.GetComponent<ParticleEmitter>().maxEmission = 0;

		daySkySR = daySkyGO.GetComponent<SpriteRenderer>();
		rainSkySR = rainSkyGO.GetComponent<SpriteRenderer>();

		DayNight2DLengthCalculations();

		sunLight.intensity = sunIntensity;
		moonLight.intensity = moonIntensity;

		if(mainCameraTag != null) {
			mainCam = GameObject.FindGameObjectWithTag(mainCameraTag).GetComponent<Camera>();
		}

		currentMoonPhaseSR = moonGO.GetComponent<SpriteRenderer>();

		rainParticles.GetComponent<AudioSource>().clip = rainClip;
		lightingGO.GetComponent<AudioSource>().clip = stormWindClip;
	}

	void Update() {
		sunLight.intensity = sunIntensity;
		moonLight.intensity = moonIntensity;
		StartCoroutine(DayNight2DCalculations());
		CelestialsMoving();
		WeatherTypes();
		MoonPhaseSpriteChanger();

		if(weatherIs == WeatherIs.Dynamic) {
			WeatherDynamicCalculator();
			SeasonsCalculations();
			MoonPhases();
		}

		if(skyFollowMainCamera) {
			transform.position = new Vector2(mainCam.transform.position.x, mainCam.transform.position.y);
		}
	}

	void DayNight2DLengthCalculations() { //Do necessary calculations for realtime day length and sun/moon move speed
		if(dayLengthIs == DayLengthIs.Fast) {
			dayLength = 13f;
			dayLengthCalculator = 1f; //It is set to 1 because 0 means infinity ;)
			celestialSpeed = 1f;
		}
		else if(dayLengthIs == DayLengthIs.Normal) {
			dayLength = 26f;
			dayLengthCalculator = 5f;
			celestialSpeed = 0.09f;
		}
		else if(dayLengthIs == DayLengthIs.Slow) {
			dayLength = 40f;
			dayLengthCalculator = 10f;
			celestialSpeed = 0.0265f;
		}

		dayTimer = dayLength; //Timer must always be equal to dayLength
	}

	IEnumerator DayNight2DCalculations() {

		if(dayLength > 0) {
			if(dayTimer >= dayLength) {
				night = false;
			}

			if(dayTimer <= 2) { //When timer reachs zero night stars.
				night = true;
			}

			if(night == false) {
				if(dayCountedAlready == false) {
					currentDayIs++;
					dayCountedAlready = true;
				}
				dayTimer -= Time.deltaTime / dayLengthCalculator; //Our timer will go from 10 to 0 and then from 0 to 10, so we can track day/night.
			}

			if(night == true) {
				dayCountedAlready = false;
				yield return new WaitForSeconds(0.1f);
				dayTimer += Time.deltaTime / dayLengthCalculator;
			}
		}
		else {
			Debug.LogError("Day Length can not be set to 0 (zero)!");
		}
	}

	void CelestialsMoving() { //Here we simply move our moon and sun depending on the time of day and moving speed

		starsGO.transform.Rotate(Vector3.forward * celestialSpeed * Time.deltaTime);
		planetsGO.transform.Rotate(Vector3.forward * (celestialSpeed/4) * Time.deltaTime); //We divide celestial speed by 4, so that planets move slower than other celestial bodies

		if(night == false) {

			sunGO.transform.position = Vector2.MoveTowards(sunGO.transform.position, celestialTop.position, celestialSpeed * Time.deltaTime);
			moonGO.transform.position = new Vector2(celestialBottom.position.x, celestialBottom.position.y);
		}
		else { //WHEN IT IS NIGHT
			moonGO.transform.position = Vector2.MoveTowards(moonGO.transform.position, celestialTop.position, celestialSpeed * Time.deltaTime);
			sunGO.transform.position = new Vector2(celestialBottom.position.x, celestialBottom.position.y);
		}
	}

	void WeatherTypes() {

		daySkySR.material.color = new Color(0f, 1f, 1f, dayNightFader);
		rainSkySR.material.color = new Color(1f, 1f, 1f, rainSkyFader); //We set rainSkyFader variable as alpha mask of our rain sky material


		if(currentWeatherIs == CurrentWeatherIs.Clear) {

			lightingGO.GetComponent<Light>().enabled = false;

			rainParticles.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime*100;
			snowParticles.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime*100;

			partSystem = null;
			if(rainParticles.GetComponent<AudioSource>().volume > 0) {
				rainParticles.GetComponent<AudioSource>().volume -= Time.deltaTime/4;
			}
			if(lightingGO.GetComponent<AudioSource>().volume > 0) { //Since the strong wind audio is attached on lightingGO audio source, we need to check it there
				lightingGO.GetComponent<AudioSource>().volume -= Time.deltaTime/4;
			}
			
			if(dayLengthIs == DayLengthIs.Fast) {
				if(rainSkyFader > 0f) {
					rainSkyFader -= Time.deltaTime/2; //Divided by two so it would be more realistic transition
				}

				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity < 0.7f) {
						sunIntensity += Time.deltaTime/2; //Slowly decrease intensity of the sunLight.
						}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/2; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Normal) {
				if(rainSkyFader > 0f) {
					rainSkyFader -= Time.deltaTime/6; //Divided by two so it would be more realistic transition
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/6; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity < 0.7f) {
						sunIntensity += Time.deltaTime/6; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/6; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/6; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Slow) {
				if(rainSkyFader > 0f) {
					rainSkyFader -= Time.deltaTime/12; //Divided by two so it would be more realistic transition
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/12; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity < 0.7f) {
						sunIntensity += Time.deltaTime/12; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/12; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/12; //Slowly increase intensity of the sunLight.
					}
				}
			}
		}

		if(currentWeatherIs == CurrentWeatherIs.Rain) {

			lightingGO.GetComponent<Light>().enabled = false;

			snowParticles.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime*100;

			partSystem = rainParticles.GetComponent<ParticleEmitter>();
			if(partSystem.maxEmission < rainAmmount) {
				partSystem.maxEmission += Time.deltaTime*20;
			}
			if(rainParticles.GetComponent<AudioSource>().volume < 1) {
				rainParticles.GetComponent<AudioSource>().volume += Time.deltaTime/8;
			}
			if(lightingGO.GetComponent<AudioSource>().volume > 0) {
				lightingGO.GetComponent<AudioSource>().volume -= Time.deltaTime/4;
			}
			
			if(dayLengthIs == DayLengthIs.Fast) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/2;
				}

				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity >= sunIntensityBadWeather) {
						sunIntensity -= Time.deltaTime/2; //Slowly decrease intensity of the sunLight.
					}
					if(sunIntensity <= sunIntensityBadWeather) {
						sunIntensity += Time.deltaTime/2; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/2; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Normal) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/6;
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/6; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity >= sunIntensityBadWeather) {
						sunIntensity -= Time.deltaTime/6; //Slowly decrease intensity of the sunLight.
					}
					if(sunIntensity <= sunIntensityBadWeather) {
						sunIntensity += Time.deltaTime/6; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/6; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/6; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Slow) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/12;
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/12; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity >= sunIntensityBadWeather) {
						sunIntensity -= Time.deltaTime/12; //Slowly decrease intensity of the sunLight.
					}
					if(sunIntensity <= sunIntensityBadWeather) {
						sunIntensity += Time.deltaTime/12; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/12; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/12; //Slowly increase intensity of the sunLight.
					}
				}
			}
		}

		if(currentWeatherIs == CurrentWeatherIs.Storm) {

			AudioSource thunderAS = gameObject.GetComponent<AudioSource>(); //See if there is Audio Source alreadt on gameobject

			if(thunderAS == null) { //If there is not....
				thunderAS = gameObject.AddComponent<AudioSource>() as AudioSource; //...create one...
				thunderAS.minDistance = 16f; //...and set minimun distance to 16f
			}

			float minimumTime = 1.0f;
			float threshold = 0.1f;

			if((Time.time - lightingLastTime) > minimumTime) {
				if(Random.value > threshold) {
					lightingGO.GetComponent<Light>().enabled = true;
				}
				else {
					lightingGO.GetComponent<Light>().enabled = false;
					lightingLastTime = Time.time;
				}
			}

			if(lightingGO.GetComponent<Light>().enabled == true) { //If thunder strike play randomly one of the sound clips from thunderAS array
				if(!thunderAS.isPlaying) {
					thunderAS.clip = thunderClip[Random.Range(0, thunderClip.Length)];
					thunderAS.Play();
				}
			}

			snowParticles.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime*100;

			partSystem = rainParticles.GetComponent<ParticleEmitter>();
			if(partSystem.maxEmission < stormRainAmmount) {
				partSystem.maxEmission += Time.deltaTime*20;
			}
			if(rainParticles.GetComponent<AudioSource>().volume < 1) {
				rainParticles.GetComponent<AudioSource>().volume += Time.deltaTime/8;
			}
			if(lightingGO.GetComponent<AudioSource>().volume < 1) {
				lightingGO.GetComponent<AudioSource>().volume += Time.deltaTime/8;
			}
			
			
			if(dayLengthIs == DayLengthIs.Fast) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/2;
				}

				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity >= sunIntensityBadWeather) {
						sunIntensity -= Time.deltaTime/2; //Slowly decrease intensity of the sunLight.
					}
					if(sunIntensity <= sunIntensityBadWeather) {
						sunIntensity += Time.deltaTime/2; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/2; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Normal) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/6;
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/6; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity >= sunIntensityBadWeather) {
						sunIntensity -= Time.deltaTime/6; //Slowly decrease intensity of the sunLight.
					}
					if(sunIntensity <= sunIntensityBadWeather) {
						sunIntensity += Time.deltaTime/6; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/6; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/6; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Slow) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/12;
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/12; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity >= sunIntensityBadWeather) {
						sunIntensity -= Time.deltaTime/12; //Slowly decrease intensity of the sunLight.
					}
					if(sunIntensity <= sunIntensityBadWeather) {
						sunIntensity += Time.deltaTime/12; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/12; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/12; //Slowly increase intensity of the sunLight.
					}
				}
			}
		}

		if(currentWeatherIs == CurrentWeatherIs.Snow) {
			
			lightingGO.GetComponent<Light>().enabled = false;

			rainParticles.GetComponent<ParticleEmitter>().maxEmission -= Time.deltaTime*100;

			partSystem = snowParticles.GetComponent<ParticleEmitter>();
			if(partSystem.maxEmission < snowAmmount) {
				partSystem.maxEmission += Time.deltaTime*20;
			}
			if(rainParticles.GetComponent<AudioSource>().volume > 0) {
				rainParticles.GetComponent<AudioSource>().volume -= Time.deltaTime/4;
			}
			if(lightingGO.GetComponent<AudioSource>().volume > 0) {
				lightingGO.GetComponent<AudioSource>().volume -= Time.deltaTime/4;
			}

			if(dayLengthIs == DayLengthIs.Fast) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/2;
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity < 0.7f) {
						sunIntensity += Time.deltaTime/2; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/2; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Normal) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/6;
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/6; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity < 0.7f) {
						sunIntensity += Time.deltaTime/6; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/6; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/6; //Slowly increase intensity of the sunLight.
					}
				}
			}
			if(dayLengthIs == DayLengthIs.Slow) {
				if(rainSkyFader < 1.0f) {
					rainSkyFader += Time.deltaTime/12;
				}
				
				if(night == false) {
					if(dayNightFader <= 1.0f) {
						dayNightFader += Time.deltaTime/12; //Fade out alpha of daySky sprite material.
					}
					if(sunIntensity < 0.7f) {
						sunIntensity += Time.deltaTime/12; //Slowly decrease intensity of the sunLight.
					}
				}
				if(night == true) {
					if(dayNightFader >= 0f) {
						dayNightFader -= Time.deltaTime/12; //Fade in alpha of daySky sprite material.
					}
					if(sunIntensity > 0f) {
						sunIntensity -= Time.deltaTime/12; //Slowly increase intensity of the sunLight.
					}
				}
			}
		}
	}

	void WeatherDynamicCalculator() {
		randomWeatherNumb0 = Random.Range(0, 5);
		randomWeatherNumb1 = Random.Range(0, 5);
		randomWeatherNumb3 = Random.Range(1, 100);

		if(randomWeatherNumb0 == 1 && randomWeatherNumb1 == 2) {
			if(changeWeatherCooldown < 1) {
				if(clearWeatherChance == randomWeatherNumb3 && clearWeatherChance > 0) {
					if(currentWeatherIs != CurrentWeatherIs.Clear) { //First check if current weather is not already the same weather so the counter will not start
						currentWeatherIs = CurrentWeatherIs.Clear;

						if(dayLengthIs == DayLengthIs.Fast) {
							changeWeatherCooldown = 30;
						}
						if(dayLengthIs == DayLengthIs.Normal) {
							changeWeatherCooldown = 100;
						}
						if(dayLengthIs == DayLengthIs.Slow) {
							changeWeatherCooldown = 400;
						}
					}
				}
				if(rainWeatherChance == randomWeatherNumb3 && rainWeatherChance > 0) {
					if(currentWeatherIs != CurrentWeatherIs.Rain) {
						currentWeatherIs = CurrentWeatherIs.Rain;

						if(dayLengthIs == DayLengthIs.Fast) {
							changeWeatherCooldown = 30;
						}
						if(dayLengthIs == DayLengthIs.Normal) {
							changeWeatherCooldown = 100;
						}
						if(dayLengthIs == DayLengthIs.Slow) {
							changeWeatherCooldown = 400;
						}
					}
				}
				if(stormWeatherChance == randomWeatherNumb3 && stormWeatherChance > 0) {
					if(currentWeatherIs != CurrentWeatherIs.Storm) {
						currentWeatherIs = CurrentWeatherIs.Storm;

						if(dayLengthIs == DayLengthIs.Fast) {
							changeWeatherCooldown = 30;
						}
						if(dayLengthIs == DayLengthIs.Normal) {
							changeWeatherCooldown = 100;
						}
						if(dayLengthIs == DayLengthIs.Slow) {
							changeWeatherCooldown = 400;
						}
					}
				}
				if(snowWeatherChance == randomWeatherNumb3 && snowWeatherChance > 0) {
					if(currentWeatherIs != CurrentWeatherIs.Snow) {
						currentWeatherIs = CurrentWeatherIs.Snow;

						if(dayLengthIs == DayLengthIs.Fast) {
							changeWeatherCooldown = 30;
						}
						if(dayLengthIs == DayLengthIs.Normal) {
							changeWeatherCooldown = 100;
						}
						if(dayLengthIs == DayLengthIs.Slow) {
							changeWeatherCooldown = 400;
						}
					}
				}
			}
		}

		if(changeWeatherCooldown >= 1) { //This is weather cooldown that needs to be zero so the weather can change...
			changeWeatherCooldown -= Time.deltaTime; //It is there so that weather dont change contantly.
		}
	}

	void SeasonsCalculations() { //This are calulations that run only when the weather type is set to dynamic

		if(currentMonthIs == CalendarMonth.January && currentDayIs == 32) { //If the month is january and it has passed 32 days, month will change to february
			currentMonthIs = CalendarMonth.February;						//and day count will reset to 0. We know that january has 31 days, so we will do
			currentDayIs = 1;												//the reset when it counts 32 days.
		}
		if(currentMonthIs == CalendarMonth.February && currentDayIs == 29) {
			currentMonthIs = CalendarMonth.March;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.March && currentDayIs == 32) {
			currentMonthIs = CalendarMonth.April;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.April && currentDayIs == 31) {
			currentMonthIs = CalendarMonth.May;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.May && currentDayIs == 32) {
			currentMonthIs = CalendarMonth.June;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.June && currentDayIs == 31) {
			currentMonthIs = CalendarMonth.July;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.July && currentDayIs == 32) {
			currentMonthIs = CalendarMonth.August;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.August && currentDayIs == 32) {
			currentMonthIs = CalendarMonth.September;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.September && currentDayIs == 31) {
			currentMonthIs = CalendarMonth.October;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.October && currentDayIs == 32) {
			currentMonthIs = CalendarMonth.November;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.November && currentDayIs == 31) {
			currentMonthIs = CalendarMonth.December;
			currentDayIs = 1;
		}
		if(currentMonthIs == CalendarMonth.December && currentDayIs == 32) {
			currentMonthIs = CalendarMonth.January;
			currentDayIs = 1;
		}

		//Here now comes the season calculations. On certain days seasons will pass, which means there is more chance that certain weather type will occur
		//SPRING
		if((currentMonthIs == CalendarMonth.March && currentDayIs >= 21) || currentMonthIs == CalendarMonth.April || currentMonthIs == CalendarMonth.May ||
		   (currentMonthIs == CalendarMonth.June && currentDayIs < 22)) {

			currentSeasonIs = CurrentSeason.Spring;
		}
		//SUMMER
		if((currentMonthIs == CalendarMonth.June && currentDayIs >= 22) || currentMonthIs == CalendarMonth.July || currentMonthIs == CalendarMonth.August ||
		   (currentMonthIs == CalendarMonth.September && currentDayIs < 23)) {
			
			currentSeasonIs = CurrentSeason.Summer;
		}
		//AUTUMN
		if((currentMonthIs == CalendarMonth.September && currentDayIs >= 23) || currentMonthIs == CalendarMonth.October || currentMonthIs == CalendarMonth.November ||
		   (currentMonthIs == CalendarMonth.December && currentDayIs < 22)) {
			
			currentSeasonIs = CurrentSeason.Autumn;
		}
		//WINTER
		if((currentMonthIs == CalendarMonth.December && currentDayIs >= 22) || currentMonthIs == CalendarMonth.January || currentMonthIs == CalendarMonth.February ||
		   (currentMonthIs == CalendarMonth.March && currentDayIs < 21)) {
			
			currentSeasonIs = CurrentSeason.Winter;
		}

		if(currentSeasonIs == CurrentSeason.Spring) {
			clearWeatherChance = 50;
			rainWeatherChance = 50;
			stormWeatherChance = 10;
			snowWeatherChance = 5;
		}
		if(currentSeasonIs == CurrentSeason.Summer) {
			clearWeatherChance = 60;
			rainWeatherChance = 20;
			stormWeatherChance = 40;
			snowWeatherChance = 0;
		}
		if(currentSeasonIs == CurrentSeason.Autumn) {
			clearWeatherChance = 20;
			rainWeatherChance = 50;
			stormWeatherChance = 40;
			snowWeatherChance = 5;
		}
		if(currentSeasonIs == CurrentSeason.Winter) {
			clearWeatherChance = 30;
			rainWeatherChance = 10;
			stormWeatherChance = 0;
			snowWeatherChance = 80;
		}
	}

	void MoonPhases() { //Here we calculate current moon phase depending on current day

		if(currentDayIs >= 1 && currentDayIs <= 3) {
			moonPhaseIs = LunarPhases.NewMoon;
		}
		if(currentDayIs >= 4 && currentDayIs <= 6) {
			moonPhaseIs = LunarPhases.WaxingCrescent;
		}
		if(currentDayIs >= 7 && currentDayIs <= 10) {
			moonPhaseIs = LunarPhases.FirstQuarter;
		}
		if(currentDayIs >= 11 && currentDayIs <= 13) {
			moonPhaseIs = LunarPhases.WaxingGibbous;
		}
		if(currentDayIs >= 14 && currentDayIs <= 17) {
			moonPhaseIs = LunarPhases.FullMoon;
		}
		if(currentDayIs >= 18 && currentDayIs <= 21) {
			moonPhaseIs = LunarPhases.WaningGibbous;
		}
		if(currentDayIs >= 22 && currentDayIs <= 25) {
			moonPhaseIs = LunarPhases.LastQuarter;
		}
		if(currentDayIs >= 26 && currentDayIs <= 28) {
			moonPhaseIs = LunarPhases.WaningCrescent;
		}
		if(currentDayIs >= 29 && currentDayIs <= 32) {
			moonPhaseIs = LunarPhases.NewMoon;
		}
	}

	void MoonPhaseSpriteChanger() {
		if(moonPhaseIs == LunarPhases.NewMoon) {
			currentMoonPhaseSR.sprite = moonPhases[0];
		}
		if(moonPhaseIs == LunarPhases.WaxingCrescent) {
			currentMoonPhaseSR.sprite = moonPhases[1];
		}
		if(moonPhaseIs == LunarPhases.FirstQuarter) {
			currentMoonPhaseSR.sprite = moonPhases[2];
		}
		if(moonPhaseIs == LunarPhases.WaxingGibbous) {
			currentMoonPhaseSR.sprite = moonPhases[3];
		}
		if(moonPhaseIs == LunarPhases.FullMoon) {
			currentMoonPhaseSR.sprite = moonPhases[4];
		}
		if(moonPhaseIs == LunarPhases.WaningGibbous) {
			currentMoonPhaseSR.sprite = moonPhases[5];
		}
		if(moonPhaseIs == LunarPhases.LastQuarter) {
			currentMoonPhaseSR.sprite = moonPhases[6];
		}
		if(moonPhaseIs == LunarPhases.WaningCrescent) {
			currentMoonPhaseSR.sprite = moonPhases[7];
		}
	}
}
