Epic Adventure Orchestral Background Music (Free Sample): SETUP GUIDE

Dear Unity Developer,
Thank you for downloading this package.

Here are some indications as to how to use these samples.

These samples are NOT to be used by simply activating the "loop" function.  In order to avoid abrupt cuts in the natural release of instruments (the time it takes for the reverb and sound to completely disappear)
you have to alternate between two samples instead of looping them.  

For instance, if you have 2 samples that are 12 beats and 3 measures long, this means that you will have to make sure the second sample starts at the 9TH BEAT or 3rd MEASURE of the first one to have a seamless transition.
To illustrate this, imagine that the length of a sample is illustrated by the numbers below with 12 beats in total.
The lines _ mean that there is music playing during these beats.  The X marks means that there is silence and the natural release of the sound.
In the illustration below, sample 1B starts after 8 beats or exactly on the 9th beat of sample 1A. This can go on until you decide to play another loop or end the cycle with an "end" sample.

SAMPLE 1A

1  2  3  4  5  6  7  8  9  10 11 12             1  2  3  4  5  6  7  8  9  10 11 12
_  _  _  _  _  _  _  _  X  X  X  X              _  _  _  _  _  _  _  _  X  X  X  X 


SAMPLE 1B

                        1  2  3  4  5  6  7  8  9  10 11 12 
          		_  _  _  _  _  _  _  _  X  X  X  X

Music always works well in sequences of two.  Meaning you can either repeat twice the sample sample, or alternate twice between samples A and B.
Usually, odd numbers don't work well in music.  Meaning if you repeat a sample 3 times and then go to another sample, it will sound strange.
This has to do with music structure and the deep cultural roots of western music, so keep it in mind when you program your loops! :)


The script provided with the package does this for you.

The piece of code below (at the very end of the script) simply counts the NUMBER OF MEASURES through the i variable and goes up to 32 measures, then resets and starts counting again.
Why 32 measures? Because that is the MAXIMUM length of the samples I have included in this package (the main exploration themes 1a, 1b, 2a and 2b)
Thanks to this code, I can then set conditions to play the next sample after i has reached measure 32 or after it has reset to 0.

if (time + 1.0F > singleMeasureTime) {
			i +=1;
			Debug.Log ("The i int equals  " + i);
			if (i==32){
				i = 0;
			}
			singleMeasureTime += 60.0F / bpm * beatsPerMeasure;
		}


HOW TO SET UP THIS SCRIPT
-------------------------

Well, I coded a rudimentary script in the 1.4 version, but although all the people that commented and rated the asset looked happy, some people that only rated it didn't seem satisfied judging by the fact that I lost a star...
So, since I hate when users are unhappy with my work, I have spent two days developing a FULLY AUTOMATED AND COMPREHENSIVE script that manages EVERYTHING for you!!! :-)
I hope this will satisfy even the most critical...

The way to use it is simple. Drag and drop the script "audio.cs" onto a game object.  Then make sure that you drag and drop the "Player" game object onto the "Player" cell inside the script (see the demo video).
The cells right below the "Player" cell are to be used as following:

VERY IMPORTANT: the "trigger_sensitivity" cell (last cell of the script) allows you to manage how close/far you have to be to a game object to trigger a transition.

- Soft_exploration: choose a game object that is very close to or at the area where the player spawns when the level loads and the soft exploration theme will start.
- Loud_exploration: choose a game object that is farther away from the player and will transition to the louder exploration theme for added tension.
- Battle_start_immediately: drag and drop an object close to the player's spawning area if you want the script to immediately enter battle mode.
- Play_battle1: choose a game object that signals the beginning of a Battle.
- Play_battle2: choose a game object that will transition to the Battle 2 theme (more intense)
- Play_battle3: choose a game object or another trigger that will go to the final phase of a Battle (there is no transition from Battle3 back to Battle1 or Battle2, however, you can easily transition between Battle1 and Battle2)
- Play_final_battle: choose a game object or trigger that will signal the end of a Battle (the most critical time, when the boss's life is nearly at an end).

For the Battle 3, you will have to do a bit of coding I'm afraid...
You will have to add, inside this line (battle3_distance < trigger_sensitivity & nearest_gameobject == battle3_distance) by another condition for instance "boss_life < 100" or something of the sort, and the same for the line that triggers
the Final Battle3 sample.

Also, for the moment, the "M" key activates the playback of an extra melody during Battle 1 and the "L" key disables it. 
You can set your own triggers to play the melody or not based on your game...

You can also play around with the samples by using the following keys on the keyboard (don't forget to delete the "KeyEvent" calls in the script so that the player doesn't accidentaly change the music by pressing a key!)

- Pressing "Z" will start playing the exploration theme directly with no introduction.
- Pressing "E" will start playing the stronger exploration theme.
- Pressing "R" will let you start the soundtrack in Battle mode directly.
- Pressing "T" will let transition to the first Battle mode.
- Pressing "Y" will let transition to the second Battle mode.
- Pressing "U" will let transition to the third and last Battle mode.
- Pressing "F" will break out of the last Battle mode and play the Finale.


IMPORTANT TO REMEMBER
---------------------

Please note that you need to wait before some transitions occur. This is precisely to AVOID doing the good old "fade in - fade out" effects that you hear in many soundtracks.
The transitions will occur always at the end of the natural musical "cycle" of the samples so it sounds like a regular composition.

For instance, if you are in Battle mode, the "natural" sequence of the samples is 1A repeated twive then 1B repeated twice.
If you press the "Z" key while in Battle mode ("T" key), the transition to break out of battle mode will only be played once the second repetition of 1B was played.



HOW TO USE THIS SCRIPT IN YOUR GAME
-----------------------------------

You can easily replace the code (below) that triggers the different samples to be played and looped with your own trigger 
(for instance placing a trigger in your game that checks how far an enemy is and prepare for the transition early).
Only replace the Input.GetKey(KeyCode.Z) as the rest of the condition triggers the sample based on proximity to a game object.

//To run only the soft exploration samples without intro
		if (Input.GetKey(KeyCode.Z)|( soft_exploration_distance < trigger_sensitivity & nearest_gameobject == soft_exploration_distance)) {
			if (!pause_and_reset & !intro_isPlaying & !exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying 
				    & !battle3_isPlaying){
					Debug.Log ("The Z key was pressed");
					intro_isPlaying = true;
					intro = true;
					j = 50;
					i = 0;
					k = 0;
					Intro ();
				}else{
				if (!pause_and_reset & !intro_isPlaying & !exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying 
				    	& !battle3_isPlaying){
						j = 0;
						i = 0;
						k = 0;
				}
				if (!intro & !intro_isPlaying){
					intro = false;
					exploration_soft =true;
					exploration_loud = false;
					battle1 = false;
					battle2 = false;
					battle3 = false;
					battle_final = false;
					melody = false;
					battle_start = false;
				}

					}


			}


Remember that to have a good user experience with this technique, the key word is ANTICIPATION.  
You need to predict for instance that the player is likely to reach an enemy in 2, 3, or more seconds and trigger the transition in advance so that the Battle sequence 
initiates as close as possible with the actual encounter with the enemy.  Similarily, while in Battle, you need to calculate when all enemies are likely to be slain 
by measuring for instance their remaining health and that of the player to trigger the "end battle" sequence as close as possible to either the death of all enemies or that of the player.

WHAT'S LEFT FOR YOU TO DO?
-------------------------

Nothing, or virtually nothing... I've really tried to make a "plug and play" type script so that you don't have to go through the painful process of coding this yourself! :-)

Please refer yourself to the two videos I have put online to demo this sample pack as you can see an illustration of the sequence in which the Battle samples can be looped.

I hope you'll be able to make use of this!
Don't forget to leave a review and suggestions/ideas for how to make this work even better!
After all, I am a noob at coding!

Marma

CONTACT: marma.developer@gmail.com

