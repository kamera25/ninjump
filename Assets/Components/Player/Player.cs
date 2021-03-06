using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public int hp = 3;
	float tapTime = 0F;

	Rigidbody2D rb;
	public int moveSpeed = 2;
	public LayerMask groundLayer; //地面のレイヤー
	float jumpForce = 10F; //ジャンプ力
	bool isGrounded; //着地しているかの判定

	public float distance;
	public Vector2 UnitVector;

	public GameObject effect_player;


	[SerializeField]
	private GameObject bullet;
	private GameObject trriger;
	private Animator animator;

	Vector2 hitPoint1, hitPoint2;
	Vector2 min;
	Vector2 max;

	AudioSource audioSource;
	AudioClip shurikenSE;
	AudioClip jumpSE;

	void Start (){
		rb = GetComponent<Rigidbody2D>();

		animator = GetComponent<Animator> ();
		min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//音声の取得
		audioSource = this.GetComponent<AudioSource> ();
		shurikenSE = Resources.Load <AudioClip>("Sounds/tm2_swing000");
		jumpSE = Resources.Load<AudioClip> ("Sounds/tm2_hit000");
	}
	
	void Update ()
	{
		if(hp <= 0 )
		{
			FindObjectOfType<StageManager>().DisplayGameOver();
			animator.SetTrigger("IsDead");
			Debug.Log("GAME OVER");
		}

		if (Input.GetMouseButtonDown(0)) {
			HitFirst();

		}

		if (Input.GetMouseButton (0)) {
			HitLast ();

			// もしタップし続けてたら,速度を上げる.
			if (0.5F < tapTime) {
				Time.timeScale = Mathf.Clamp (Time.timeScale + Time.deltaTime, 1F, 3F);
			}
		} else 
		{
			if( 1F < Time.timeScale)
			{
				Time.timeScale = Mathf.Clamp (Time.timeScale - Time.deltaTime, 1F, 3F);
			}
		}


		
		if(Input.GetMouseButtonUp(0)){

			// 距離
			distance = Vector2.Distance(hitPoint2, hitPoint1);
			Debug.Log(hitPoint1);
			Debug.Log(hitPoint2);

			// 大きさが1以上で、ベクトルがy軸に対して30度未満のものをフリック入力として受け取る。
			if(distance > 1f){
				Debug.Log("Flick");
				audioSource.PlayOneShot( shurikenSE);
				animator.SetTrigger("Attack");
				Instantiate (bullet, transform.position, transform.rotation);
			}else if(isGrounded && distance <= 1f){
				// Empty
				Jump ();
			}

			//タップ時間を初期化
			tapTime = 0F;
		}

		animator.SetBool("IsGround", isGrounded);
	}

	// 最初の位置を得る。
	void HitFirst(){
		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		hitPoint1= worldPoint;
	}
	// 最後の位置を得る。
	void HitLast(){
		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		hitPoint2= worldPoint;

		tapTime += Time.deltaTime;
	}

	void OnCollisionEnter2D (Collision2D col){
	//void OnTriggerEnter2D (Collider2D col){
		string layerName = LayerMask.LayerToName (col.gameObject.layer);
		Debug.Log (layerName);
		if (layerName == "Ground") {
			animator.SetTrigger("Ground");
			isGrounded = true;
			Vector2 temp = gameObject.transform.localScale;
			gameObject.transform.localScale = temp;
			}
	}

	void Jump (){
		//上方向へ力を加える
		Debug.Log("Jump");

		rb.AddForce (Vector2.up * jumpForce * Mathf.Clamp( tapTime * 9F, 0.65F, 1F), ForceMode2D.Impulse);
		isGrounded = false;
		animator.SetTrigger("IsJump");
		audioSource.PlayOneShot (jumpSE);
	}

	public void Damage(int dmg){
		Instantiate (effect_player, transform.position, transform.rotation);
		hp = hp - dmg;
	}
}
