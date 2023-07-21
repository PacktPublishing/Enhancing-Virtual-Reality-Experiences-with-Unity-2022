using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class audio : MonoBehaviour {
	public float bpm = 150.0F;
	public int beatsPerMeasure = 4;

	public Transform Player;
	public Transform Soft_exploration;
	public Transform Loud_exploration;
	public Transform Battle_start_immediately;
	public Transform Play_battle1;
	public Transform Play_battle2;
	public Transform Play_battle3;
	public Transform Play_battle_final;

	Object[] AudioArray_intro;
	Object[] AudioArray_battle1;
	Object[] AudioArray_battle2;
	Object[] AudioArray_battle3;
	Object[] AudioArray_trans;

	private double singleMeasureTime;
	private double delayEvent;
	private AudioSource audio_explorationA;
	private AudioSource audio_explorationB;
	private AudioSource audio_battleA;
	private AudioSource audio_battleB;
	private AudioSource audio_melody;
	private AudioSource audio_trans;
	private AudioSource audio_start_end;
	private bool running = false;
	public bool intro;
	public bool exploration_soft;
	public bool exploration_loud;
	public bool battle_start;
	public bool battle1;
	public bool battle2;
	public bool battle3;
	public bool battle_final;
	public bool melody;
	public bool battle_start_initial;

	private bool exploration_soft_isPlaying;
	private bool exploration_loud_isPlaying;
	private bool battle1_isPlaying;
	private bool battle2_isPlaying;
	private bool battle3_isPlaying;
	private bool intro_isPlaying;
	public bool pause_and_reset;
	double time;
	private bool single_trigger = true;
	private float nearest_gameobject;
	public float soft_exploration_distance = 5000;
	public float loud_exploration_distance= 5000;
	public float battle_initial_distance= 5000;
	public float battle1_distance = 5000;
	public float battle2_distance= 5000;
	public float battle3_distance= 5000;
	public float battle_final_distance= 5000;
	public float trigger_sensitivity;


	private int i;
	private int j;
	private int k;
	private int l;
	void Start() {
		bpm = 150.0F;
		beatsPerMeasure = 4;
		soft_exploration_distance = 5000;
		loud_exploration_distance= 5000;
		battle_initial_distance= 5000;
		battle1_distance = 5000;
		battle2_distance= 5000;
		battle3_distance= 5000;
		battle_final_distance= 5000;

		intro= false;
		exploration_soft= false;
		exploration_loud= false;
		battle_start= false;
		battle1= false;
		battle2= false;
		battle3= false;
		battle_final= false;
		melody= false;
		battle_start_initial= false;

		audio_explorationA = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_explorationB = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_melody = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_battleA = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_battleB = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_trans = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_start_end = (AudioSource)gameObject.AddComponent <AudioSource>();
		AudioArray_intro = Resources.LoadAll("epic_battle/intro",typeof(AudioClip));
		AudioArray_battle1 = Resources.LoadAll("epic_battle/battle1",typeof(AudioClip));
		AudioArray_battle2 = Resources.LoadAll("epic_battle/battle2",typeof(AudioClip));
		AudioArray_battle3 = Resources.LoadAll("epic_battle/battle3",typeof(AudioClip));
		AudioArray_trans = Resources.LoadAll("epic_battle/trans",typeof(AudioClip));

		exploration_soft_isPlaying= false;
		exploration_loud_isPlaying= false;
		battle1_isPlaying= false;
		battle2_isPlaying= false;
		battle3_isPlaying = false;
		intro_isPlaying = false;

		singleMeasureTime = AudioSettings.dspTime + 2.0F;
		running = true;
	}
	void Update() {
				if (!running)
						return;
				time = AudioSettings.dspTime;


	

		//To enable the melody
		if (Input.GetKey(KeyCode.L)) {
			
			Debug.Log ("The L key was pressed");

			melody = false;

		}

		//To enable the melody
		if (Input.GetKey(KeyCode.M)) {
			
			Debug.Log ("The M key was pressed");
			
			melody = true;
			
		}

		//To run only the soft exploration samples 
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


		//To run the strong exploration samples
		if (Input.GetKey(KeyCode.E)|(!pause_and_reset & loud_exploration_distance < trigger_sensitivity & nearest_gameobject == loud_exploration_distance)) {
			if (!pause_and_reset & !intro_isPlaying & !exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying 
			    & !battle3_isPlaying){
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
					exploration_soft =false;
					exploration_loud = true;
					battle1 = false;
					battle2 = false;
					battle3 = false;
					battle_final = false;
					melody = false;
					battle_start = false;
				}
				
			}
			

		}

		//To run the Battle1 samples with start
		if (Input.GetKey(KeyCode.R)|(battle_initial_distance < trigger_sensitivity  & nearest_gameobject == battle_initial_distance)) {
			if (!exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying 
			    & !battle3_isPlaying){
				j = 0;
				i = 50;
				pause_and_reset = true;
				battle_start_initial= true;
			}
			//Debug.Log ("The R key was pressed");

		}
		// To run the Battle1 
		if (Input.GetKey(KeyCode.T)|(battle1_distance < trigger_sensitivity & nearest_gameobject == battle1_distance)) {
			if (!pause_and_reset & !exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying 
			    & !battle3_isPlaying){
				j = 0;
				i = 0;
				k = 0;
			}
			if (exploration_soft_isPlaying | exploration_loud_isPlaying){
				battle_start = true;
			}
			//Debug.Log ("The T key was pressed");
			if (!pause_and_reset){
			intro = false;
			exploration_soft =false;
			exploration_loud = false;
			battle1 = true;
			battle2 = false;
			battle3 = false;
			battle_final = false;
			}

		}
		// To run the Battle3
		if (Input.GetKey(KeyCode.U)|(battle3_distance < trigger_sensitivity & nearest_gameobject == battle3_distance)) {
			if (!pause_and_reset & !exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying 
			    & !battle3_isPlaying){
				j = 0;
				i = 0;
				k = 0;
			}
			if (exploration_soft_isPlaying | exploration_loud_isPlaying){
				battle_start = true;
			}
			//Debug.Log ("The U key was pressed");
			intro = false;
			exploration_soft =false;
			exploration_loud = false;
			battle1 = false;
			battle2 = false;
			battle3 = true;
			battle_final = false;
			melody = false;
		}

		// To run the Battle2 
		if (Input.GetKey(KeyCode.Y)|(battle2_distance < trigger_sensitivity & nearest_gameobject == battle2_distance)) {
			if (!pause_and_reset & !exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying 
			    & !battle3_isPlaying){
				j = 0;
				i = 0;
			}
			if (exploration_soft_isPlaying | exploration_loud_isPlaying){
				battle_start = true;
			}
			//Debug.Log ("The Y key was pressed");
			if (!pause_and_reset){
				intro = false;
				exploration_soft =false;
				exploration_loud = false;
				battle1 = false;
				battle2 = true;
				battle3 = false;
				battle_final = false;
				melody = false;
			}

		}

		if (Input.GetKey(KeyCode.F)|(battle_final_distance < trigger_sensitivity & nearest_gameobject == battle_final_distance)) {
			
			battle_final = true;
			battle3 = false;

		}

		if (intro) {
			Intro ();
		}

		if (pause_and_reset) {
			Pause_and_reset ();
		}

		if (!battle1_isPlaying & !battle2_isPlaying & !battle3_isPlaying) {
			if (exploration_soft | exploration_soft_isPlaying) {
				Exploration_Soft ();
			}
			
			
			if (exploration_loud | exploration_loud_isPlaying) {
				Exploration_Loud ();
			}
		}

		if (!exploration_soft_isPlaying & !exploration_loud_isPlaying) {
			if (!battle2_isPlaying & !battle3_isPlaying){
				if (battle1 | battle1_isPlaying) {
					Battle1 ();
				}
			}
			if (!battle1_isPlaying & !battle3_isPlaying){
				if (battle2 | battle2_isPlaying) {
					Battle2 ();
				}
			}

			if (!battle1_isPlaying & !battle2_isPlaying){
				if (battle3 | battle3_isPlaying) {
					Battle3 ();
				}
			}
		}

		if (!exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying & !battle3_isPlaying) {
			if (battle_start) {
				Battle_start ();
			}
		}

			


		//THE most important part of this script: this is the metronome, keeping count of the measures and making sure the audio is in sync
		if (time + 1.0F > singleMeasureTime) {

			Debug.Log ("The j int equals  " + j + " " + time);
			Debug.Log ("The i int equals  " + i + " " + time);
			Debug.Log ("The k int equals  " + k + " " + time);
			Debug.Log ("The l int equals  " + l + " " + time);
			if (j==16){
				j = 0;
			}
			if (i == 8){
				i = 0;
			}
			if (k == 16){
				k = 0;
			}
			if (l == 10){
				l = 0;
			}
			i +=1;
			j += 1;
			k += 1;
			l +=1;
			singleMeasureTime += 60.0F / bpm * beatsPerMeasure;
			CheckDistanceToTrigger ();
		}

	}

	void Intro(){
		if (!exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying & !battle3_isPlaying) {
			if (j == 50) {
				if (time + 1.0F > singleMeasureTime) {
					audio_start_end.clip = AudioArray_trans[1] as AudioClip;
					audio_start_end.PlayScheduled (time);
				}
			}

			if (j == 54) {
				intro = false;
				if (nearest_gameobject == soft_exploration_distance) {
					exploration_soft = true;
				}
				if (nearest_gameobject == loud_exploration_distance) {
					exploration_loud = true;
				}
				j = 0;
			}
		
		}

	}

	void Pause_and_reset(){
		if (time + 1.0F > singleMeasureTime) {
		if (!exploration_soft_isPlaying & !exploration_loud_isPlaying & !battle1_isPlaying & !battle2_isPlaying & !battle3_isPlaying) {
			if (l == 1) {
		
					if (battle_start_initial){

						battle1=true;
					}
					if (nearest_gameobject == battle1_distance) {
						battle1 = true;
					}
					if (nearest_gameobject == battle2_distance) {
						battle2 = true;
					}
					if (nearest_gameobject == soft_exploration_distance) {
						exploration_soft = true;
					}
					if (nearest_gameobject == loud_exploration_distance) {
						exploration_loud = true;
					}

			}

			if (l == 2) {
				
				if (battle1 | battle2) {
					audio_start_end.clip = AudioArray_trans [0] as AudioClip;
					audio_start_end.PlayScheduled (time);

				}
				if (exploration_soft | exploration_loud) {
					audio_start_end.clip = AudioArray_trans [1] as AudioClip;
					audio_start_end.PlayScheduled (time);
				}
			}

			if (l == 3) {
				if (battle1 | battle2) {
					pause_and_reset = false;
						battle_start_initial = false;
					i = 0;
					j = 0;
					k = 0;
				}
			}
				if (l == 5) {
					if (exploration_loud) {
						audio_trans.clip = AudioArray_trans [3] as AudioClip;
						audio_trans.PlayScheduled (time);
					}
				}

			if (l == 6) {
				if (exploration_soft | exploration_loud) {
					pause_and_reset = false;
					i = 0;
					j = 0;
					k = 0;
				}
			}
			}
		}
	}



	void Battle_start(){
		
		if (i == 0) {
			if (time + 1.0F > singleMeasureTime) {
				audio_start_end.clip = AudioArray_trans[0] as AudioClip;
				audio_start_end.PlayScheduled (time);
			}
		}
		if (i == 1) {
			battle_start_initial = false;
			battle_start = false;
			if (nearest_gameobject == battle1_distance){
				battle1 = true;
			}
			if (nearest_gameobject == battle2_distance){
				battle2 = true;
			}
			i = 0;
			j = 0;
			k = 0;
		}
	}

	void Exploration_Soft(){
		if (time + 1.0F > singleMeasureTime) {
			if (j == 0 & exploration_soft) {
				intro = false;
				intro_isPlaying = false;
				exploration_soft_isPlaying = true;
				audio_explorationA.clip = AudioArray_intro [0] as AudioClip;
				audio_explorationA.PlayScheduled (time);
			
			}
			if (j == 7 & exploration_soft == false) {
				Transitions ();
			}
			if (j == 8 & exploration_soft == false) {
				i = 0;
				k = 0;
				exploration_soft_isPlaying = false;
			}

			if (j == 8 & exploration_soft) {
				exploration_soft_isPlaying = true;
				intro_isPlaying = false;
				audio_explorationB.clip = AudioArray_intro [1] as AudioClip;
				audio_explorationB.PlayScheduled (time);
			}
			if (j == 15 & exploration_soft == false) {
				Transitions ();
			}
			if (j == 16 & exploration_soft == false) {
				i = 0;
				k = 0;
				j = 0;
				exploration_soft_isPlaying = false;
			}
			if (j == 16 & exploration_soft) {
				intro = false;
				exploration_soft_isPlaying = true;
				audio_explorationA.clip = AudioArray_intro [0] as AudioClip;
				audio_explorationA.PlayScheduled (time);
				
			}
		}
	}

	void Exploration_Loud(){
		if (time + 1.0F > singleMeasureTime) {
			if (j == 0 & exploration_loud) {
				intro_isPlaying = false;
				exploration_loud_isPlaying = true;
				audio_explorationA.clip = AudioArray_intro [2] as AudioClip;
				audio_explorationA.PlayScheduled (time);
				}
			if (j == 7 & exploration_loud == false) {
				Transitions ();
			}
			if (j == 8 & exploration_loud == false) {
				i = 0;
				k = 0;
				exploration_loud_isPlaying = false;
			}
			if (j == 8 & exploration_loud) {
				exploration_loud_isPlaying = true;
				intro_isPlaying = false;
				audio_explorationB.clip = AudioArray_intro [3] as AudioClip;
				audio_explorationB.PlayScheduled (time);
			}
			if (j == 15) {
				Transitions ();
			}
			if (j == 16 & exploration_loud == false) {

				i = 0;
				k = 0;
				j = 0;
				if (exploration_soft){
					Exploration_Soft();
				}
				exploration_loud_isPlaying = false;
			}
			if (j == 16 & exploration_loud) {
				exploration_loud_isPlaying = true;
				audio_explorationA.clip = AudioArray_intro [2] as AudioClip;
				audio_explorationA.PlayScheduled (time);
			}
		}
	}

	void Battle1(){
		if (time + 1.0F > singleMeasureTime) {
			if (i == 0) {
				battle1_isPlaying = true;
				audio_battleA.clip = AudioArray_battle1 [1] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (i == 2) {
				battle1_isPlaying = true;
				audio_battleB.clip = AudioArray_battle1 [1] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (i == 4 & battle1 == false) {
					battle1_isPlaying = true;
					Transitions ();
			}
			if (i == 4 & battle1_isPlaying) {
				battle1_isPlaying = true;
				audio_battleA.clip = AudioArray_battle1 [2] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (i == 6) {
				battle1_isPlaying = true;
				audio_battleB.clip = AudioArray_battle1 [2] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (i == 7 & !battle1) {
				if (battle2|battle3){
					audio_trans.clip = AudioArray_trans [2] as AudioClip;
					audio_trans.PlayScheduled (time);
				}

			}
			if (i == 7 & battle1) {
				audio_trans.clip = AudioArray_trans [3] as AudioClip;
				audio_trans.PlayScheduled (time);
			}
			if (i == 8 & battle1) {
				i = 0;
				Battle1();
			}
			if (i == 8 & battle1 == false) {
				if (exploration_soft | exploration_loud | intro){
					battle1_isPlaying = true;
					Transitions ();
				}else if (battle2 | battle3){
					battle1_isPlaying = false;
					i = 0;
					k = 0;
				}
			}
			if (melody){
				if (i == 0 | i == 2 | i == 4 | i == 6) {
					audio_melody.clip =   AudioArray_battle1 [3] as AudioClip;
					audio_melody.PlayScheduled (time);
				}
			}
		}
	}

	void Battle2(){
		if (time + 1.0F > singleMeasureTime) {
			if (i == 0) {
				battle2_isPlaying = true;
				audio_battleA.clip = AudioArray_battle2 [1] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (i == 2) {
				battle2_isPlaying = true;
				audio_battleB.clip = AudioArray_battle2 [1] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (i == 4 & battle2 == false) {
				if (exploration_soft | exploration_loud | intro){
					battle2_isPlaying = true;
					Transitions ();
				}else{
					battle2_isPlaying = true;
				}
			}
			if (i == 4 & battle2_isPlaying) {
				battle2_isPlaying = true;
				audio_battleA.clip = AudioArray_battle2 [2] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (i == 6) {
				battle2_isPlaying = true;
				audio_battleB.clip = AudioArray_battle2 [2] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (i == 7 & !battle2) {
				if (battle1 | battle3){
					audio_trans.clip = AudioArray_trans [2] as AudioClip;
					audio_trans.PlayScheduled (time);
				}

			}
			if (i == 7 & battle2) {
				audio_trans.clip = AudioArray_trans [3] as AudioClip;
				audio_trans.PlayScheduled (time);

			}
			if (i == 8 & battle2) {
				i = 0;
				Battle2();
			}			

			if (i == 8 & battle2 == false) {
				if (exploration_soft | exploration_loud | intro){
					battle2_isPlaying = true;
					Transitions ();

				}else if (battle1 | battle3){
					battle2_isPlaying = false;
					i = 0;
					k = 0;
					if (battle1){
						Battle1();
					}
				}
			}
		}
	}

	void Battle3(){
		if (time + 1.0F > singleMeasureTime) {
			if (k == 0) {
				battle3_isPlaying = true;
				audio_battleA.clip = AudioArray_battle3 [1] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (k == 2 & battle3 == false & !battle_final) {
				Transitions ();
				battle3_isPlaying = false;
			}
			if (k == 2 & battle3_isPlaying & battle3) {
				battle3_isPlaying = true;
				audio_battleB.clip = AudioArray_battle3 [1] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (k == 2 & battle3_isPlaying & battle_final) {
				battle3_isPlaying = true;
				audio_battleB.clip = AudioArray_battle3 [1] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (k == 4 & battle3_isPlaying) {
				battle3_isPlaying = true;
				audio_battleA.clip = AudioArray_battle3 [2] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (k == 6) {
				battle3_isPlaying = true;
				audio_battleB.clip = AudioArray_battle3 [3] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (k == 8 & !battle_final) {
				battle3_isPlaying = true;
				audio_battleA.clip = AudioArray_battle3 [1] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (k == 8 & battle_final) {
				battle3_isPlaying = true;
				Transitions ();
					
			}
			if (k == 10 & battle3 == false & battle_final == false) {
					Transitions ();
					battle3_isPlaying = false;
					
			}
			if (k == 10 & battle3_isPlaying & battle3) {
				battle3_isPlaying = true;
				audio_battleB.clip = AudioArray_battle3 [1] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (k == 10 & battle3_isPlaying & battle_final) {
				battle3_isPlaying = true;
				audio_battleB.clip = AudioArray_battle3 [1] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (k == 12) {
				battle3_isPlaying = true;
				audio_battleA.clip = AudioArray_battle3 [1] as AudioClip;
				audio_battleA.PlayScheduled (time);
			}
			if (k == 14) {
				battle3_isPlaying = true;
				audio_battleB.clip = AudioArray_battle3 [4] as AudioClip;
				audio_battleB.PlayScheduled (time);
			}
			if (k == 15 & battle3 & battle3_isPlaying) {
				audio_trans.clip = AudioArray_trans [2] as AudioClip;
				audio_trans.PlayScheduled (time);
			}
			if (k == 16 & !battle_final) {
				k = 0;
				Battle3();
			}

			if (k == 16 & battle_final) {
					battle3_isPlaying = true;
					Transitions ();
					
			}
		}
	}


	void Transitions (){
		if (time + 1.0F > singleMeasureTime) {
			if (exploration_loud_isPlaying) {
				audio_trans.clip = AudioArray_trans [3] as AudioClip;
				audio_trans.PlayScheduled (time);
			}
			if (battle_start) {
				if (!battle1_isPlaying & !battle2_isPlaying & !battle3_isPlaying){
					if (exploration_soft_isPlaying | exploration_loud_isPlaying){
						audio_start_end.clip = AudioArray_trans[0] as AudioClip;
						audio_start_end.PlayScheduled (time);
						battle_start = true;

					}
				}
			}
			if (exploration_soft_isPlaying) {
				audio_trans.clip = AudioArray_trans [3] as AudioClip;
				audio_trans.PlayScheduled (time);
			}

		

			if (battle3_isPlaying & !battle3 & !battle_final){
				audio_start_end.clip = AudioArray_battle3 [0] as AudioClip;
				audio_start_end.PlayScheduled (time);
				l = 7;
				pause_and_reset = true;

				i = 50;
				j = 50;
				k = 50;
				battle3_isPlaying = false;
			}
			if (battle1_isPlaying & !battle1 & !battle2 & !battle3){
				audio_start_end.clip = AudioArray_battle1 [0] as AudioClip;
				audio_start_end.PlayScheduled (time);
				l = 7;
				pause_and_reset = true;

				i = 50;
				j = 50;
				k = 50;
				battle1_isPlaying = false;
			}

			if (battle2_isPlaying & !battle1 & !battle2 & !battle3){
				audio_start_end.clip = AudioArray_battle2 [0] as AudioClip;
				audio_start_end.PlayScheduled (time);
				l = 7;
				pause_and_reset = true;

				i = 50;
				j = 50;
				k = 50;
				battle2_isPlaying = false;
			}

			if (battle3_isPlaying & battle_final) {
				Debug.Log ("Trigger to leave Battle 3 and play FINAL worked");
				battle3_isPlaying = false;
				audio_start_end.clip = AudioArray_battle3 [5] as AudioClip;
				audio_start_end.PlayScheduled (time);

				
			}

		}

	}


	void CheckDistanceToTrigger(){
		//THIS BLOCK TESTS IF THE PLAYER MOVES, IF YES, THEN THE CODE GETS THE DISTANCE OF THE NEAREST ENEMIY/TRIGGER
		if (Player) {
			//ADD ENEMIES OR OTHER TRIGGERS TO THIS SCRIPT AS NEEDED

			if(Soft_exploration != null){
				soft_exploration_distance = Vector3.Distance(Player.position, Soft_exploration.position);

			}
			if(Loud_exploration != null){
				loud_exploration_distance = Vector3.Distance(Player.position, Loud_exploration.position);
			}

			if(Battle_start_immediately != null){
				battle_initial_distance = Vector3.Distance(Player.position, Battle_start_immediately.position);
			}
			if(Play_battle1 != null){
				battle1_distance = Vector3.Distance(Player.position, Play_battle1.position);
			}
			if(Play_battle2 != null){
				battle2_distance = Vector3.Distance(Player.position, Play_battle2.position);
			}
			if(Play_battle3 != null){
				battle3_distance = Vector3.Distance(Player.position, Play_battle3.position);
			}
			if(Play_battle_final != null){
				battle_final_distance = Vector3.Distance(Player.position, Play_battle_final.position);
			}

			float[] distance_to_gameobject = {soft_exploration_distance ,
				loud_exploration_distance,
				battle_initial_distance,
				battle1_distance ,
				battle2_distance,
				battle3_distance,
				battle_final_distance};
			System.Array.Sort(distance_to_gameobject);
			nearest_gameobject = distance_to_gameobject[0];
			//IF THE GAME OBJECT IS DESTROYED, YOU ASIGN A HIGH DEFAULT VALUE TO THE DISTANCE TO AVOID KEEPING THE LAST
			//VALUE OF THE DEAD ENEMY'S DISTANCE TO YOU AND JUMP TO THE NEAREST "LIVING" GAME OBJECT

			if(Soft_exploration == null){
				soft_exploration_distance = 5000;
			}
			if(Loud_exploration == null){
				loud_exploration_distance = 5000;
			}

			if(Battle_start_immediately == null){
				battle_initial_distance = 5000;
			}
			if(Play_battle1 == null){
				battle1_distance = 5000;
			}
			if(Play_battle2 == null){
				battle2_distance = 5000;
			}
			if(Play_battle3 == null){
				battle3_distance = 5000;
			}
			if(Play_battle_final == null){
				battle_final_distance = 5000;
			}
			

		}
	}



}