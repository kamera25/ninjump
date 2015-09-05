using UnityEngine;
using System.Collections;

public class Oni : Enemy 
{
	Animator anim;
	//public SpriteRenderer spriteR;

	[SerializeField]
	float appearTime = 10F; // 鬼の出現時間.

	public float nowTime = 0F;

	public float faceTime = 0F;




	void Awake()
	{
		//spriteR = this.GetComponent<SpriteRenderer> ();
	}

	void Start()
	{
		anim = this.GetComponent<Animator> ();
		anim.enabled = false;
	}

	void Update()
	{
		if( IsAppearOni())
		{
			anim.enabled = true;
		}

		nowTime += Time.deltaTime;
	}

	new void OnTriggerEnter2D (Collider2D col)
	{
		// まだ鬼が出てなければ,ダメージ判定をしない.
		if (!IsAppearOni()) 
		{
			return;
		}

		base.OnTriggerEnter2D (col);

		if (hp < 0F) 
		{
			anim.SetTrigger ("IsDead");
			faceTime = nowTime + 10F;
		} 
		else 
		{
			anim.SetTrigger("HitDamage");
			faceTime = nowTime + 3F;
		}
	}
	
	private bool IsAppearOni()
	{
		return appearTime < nowTime;
	}
}
