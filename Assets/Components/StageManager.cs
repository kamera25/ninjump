using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

	const float MoveX = 14f;
	
	public GameObject[] enemys;
	public GameObject item;
	public GameObject[] ground;

	public Vector2 v_enemy;
	public Vector2 v_item;
	public Vector2 v_ground;


	public float seconds = 3f;
	private int count = 0;

	private GameObject player;

	public float INTERVAL = 3.0f;
	public float timer;

	private bool isGameOver;

	// Startメソッドをコルーチンとして呼び出す
	IEnumerator Start ()
	{
		while (true) {
			
			int rand = Random.Range(1, 4);
			float randY = Random.Range (-1.5f, 1.5f);
			switch(rand){
			case 1:
				int ene_rand = Random.Range(1, 5);
				transform.position = v_enemy + new Vector2( 0F, randY);
				Instantiate (enemys[ene_rand], transform.position, transform.rotation);
				break;
			case 2:
				transform.position = new Vector2(MoveX, randY);
				Instantiate (item, transform.position, transform.rotation);
				break;
			}
			
			count++;
			
			// seconds秒待つ
			yield return new WaitForSeconds (seconds);
			
		}
	}

	private void Awake()
	{
		player = GameObject.FindWithTag ("Player");
	}

	private void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			//transform.position = v_ground;
			int rand = Random.Range( 0, ground.Length);
			transform.position = new Vector2(MoveX, 18f) + v_ground;
			Instantiate (ground[rand], transform.position, transform.rotation);

			timer = INTERVAL;
		}

		if(isGameOver)
		{
			if (Input.GetMouseButtonDown (0)) {
				Application.LoadLevel("title");
			}

			// プレイヤーが落ちてたら.
			if( player == null)
			{
				Time.timeScale -= Time.deltaTime;
			}
		}
	}

	/// <summary>
	/// Displaies the game over.
	/// </summary>
	public void DisplayGameOver()
	{
		GameObject gameOver = GameObject.Find ("GameOverCanvas/GameOverText");
		if(gameOver == null)
		{
			return;
		}

		gameOver.GetComponent<Text> ().enabled = true;
		isGameOver = true;
	}

	public void StopDeadMotion()
	{
		GameObject.Instantiate<GameObject> (Resources.Load<GameObject>("Prefabs/UI/GameClearCanvas"));
	}

		
}
