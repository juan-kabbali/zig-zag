using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float force, time;
	public GameObject coinsContainer;
	private bool changeDir;
	private Vector3 dir;
	private Rigidbody rb;
	private Text txtScore;
	private Text txtTime;
	private int score;
	private bool isFirstClick;

	void Start () {
		//flag to change direction by clicking 
		changeDir = false;
		// this flag allow to start time count down when the player make the first click 
		isFirstClick = false;
		rb = GetComponent<Rigidbody>();
		dir = new Vector3(0,0,0);
		score = 0;
		//get txt components references
		txtScore = GameObject.Find("Score").GetComponent<Text>();
		txtTime = GameObject.Find("Time").GetComponent<Text>();


	}
	
	void Update () {

		time -= Time.deltaTime;
		if(time <= 0){
			restartGame();
		}
		if(transform.position.y < -1){
			restartGame();
		}
		listenPlayerInteraction();
	}

	void FixedUpdate(){
		rb.MovePosition(transform.position + dir * Time.deltaTime * force);
	}

	void OnTriggerEnter(Collider obj){
		if(obj.gameObject.tag == "Coin"){
			obj.gameObject.SetActive(false);
			force++;
			score++;
			txtScore.text = "Score: " + score.ToString();
		}
		// if the player win, will play again
		if(obj.gameObject.tag == "Goal"){
			restartGame();
		}
	}

	void listenPlayerInteraction (){

		if(Input.GetMouseButtonDown(0)){
			if(!isFirstClick){
				isFirstClick = true;
			}
			//Stop player animation
			rb.Sleep();
			if(changeDir){
				dir = new Vector3(0,0,-1);
				changeDir = false;
			}else{
				dir = new Vector3(-1,0,0);
				changeDir = true;
			}
		}
		if(isFirstClick){
			txtTime.text = "Time: " + time.ToString("F2");
		}
	}

	/* Restart all init variables values to start again */
	void restartGame(){
		score = 0;
		txtScore.text = "Score: 0";
		time = 30;
		force = 5;
		txtTime.text = "Time: 30";
		isFirstClick = false;
		//initial position
		this.transform.position = new Vector3(4,0.7f,4);
		//Reset dir vector values
		dir = new Vector3(0,0,0);
		//allows that first click will move the ball forward 
		changeDir = false;
		rb.Sleep();
		// this loop place or show all coins in the initial position  
		for(int i = 0; i < coinsContainer.transform.childCount; i++){
			coinsContainer.transform.GetChild(i).gameObject.SetActive(true);
		}
	}

}


















