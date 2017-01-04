- 2D Time Of Day -

2D Time of Day is a addon for every platform type 2D game! Add real time day and night cycle to your game by just draggin one prefab to your scene!
2D Time of Day is very performance friendly, also!

Changelog: 

	- Version 1.4 brings completely new weather system that can be set as a dynamic where with day and night cycle your weather will process realisticly or you can
	set it to be static with just one weather type. Weather transition is soft and realistic.

	- Version 1.5 brings calendar system with moon phases system, where the year is divided by twelves months and month by days accordingly and where the moon
	phases changes depending on the day in the month. We also added more control over the system and made our script even more visualy appealing.

If you want to use it for big scale level, just resize the Day and Night Sky and fill in the stars. :)

#######################:SETUP GUIDE:#######################

 - 1.0 BASICS

2D Time of Day is a simple :drop and use" plugin for Unity. You are provided 2DTOD prefab that you need to place into your scene.

First and most basic is "Sky Follow Main Camera" boolean of the 2DTOD script in prefab. It is best to let it stay true. Why? Because then the whole
system will follow your main camera, you will never run to the edges of the system that way. You will also need to enter your Mains Camera tag in Main Camera Tag field.
If in other hand you want it to be static in your scene you will have to resize DaySky, NightSky, RainSky and you will need to change parameters of 
rain and snow particle emitters so that they cover your whole level.

Second thing that you should take look at are CelestialsTop and CelestialsBottom objects. These two are the beginning and the ending of the path that sun and moon
follow throughout the day or night. If you want to change path that sun and moon will follow, just move those two to desired locations.

If you want to change the intensity you sun/moon will shine, just tweak Sun Intensity/Moon Intensity.

You can also choose the speed that system will follow throughout its course. You can choose from Fast, Normal and Slow. Slow is somewhat default value.

 - 1.1 WEATHER SYSTEM
You should also take look at is STATIC and DYNAMIC weather system. I think that these two speak for themselves. 

 - STATIC WEATHER SYSTEM
Static weather system means that the weather you choose will be static, it will loop itself over and over again. 
Day and night will cycle but everything else will be static.

 - DYNAMIC WEATHER SYSTEM
Dynamic weather system, on the other hand, is a complete dynamic system. It means that day and night will cycle, calendar system will count days and months,
moon will go through its eight phases and seasons will change. Weather chances will also change depending on the current season.

 - 1.2 CELESTIALS SYSTEM
Celestials are now part of the 2DTOD asset. They are planets that rotate around the system and main camera (if system is set to follow the main camera).
They are currently only visuals that enchance the look of the night, but can be used to display whole unique solar system of your game.

 - 1.3 CALENDAR SYSTEM
Calendar system is now integrated part of the DYNAMIC weather system. If the DYNAMIC weather system is used, the calendar will automaticly update itself
based on the number of days passed, where on already pre set value the month will change. When enough months pass the season will change dynamicly
between Spring, Summer, Autumn and Winter.

 - 1.4 MOON PHASES SYSTEM
Integral part of the Calendar system is the Moon Phases system where on certain days in the moonth moons state and graphics will change between eight phases.
This system can be used by any game type be it for visuals only or any "werewolfish" type of the game segment...

List of Layer Orders for sprites:

-5 = Clouds
-6 = Horizon
-7 = Rain sky
-8 = Moon
-9 = Sun
-10 = Day sky
-11 = Night stars
-12 = Night sky