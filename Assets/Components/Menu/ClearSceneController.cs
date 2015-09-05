using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClearSceneController : MonoBehaviour 
{
	private GameObject scoreObj;

	[SerializeField]
	private GameObject highScoreText = null;

	[SerializeField]
	private Text scoreText = null;

	private string saveKey = null;
	private int oldScore = 0;
	private int nowScore = 0;

	void Start()
	{
		Time.timeScale = 0F; // 処理と時間を止める.

		Score score = GameObject.Find("ScoreGUI").GetComponent<Score>();

		// 大量の情報を取得
		saveKey = score.highScoreKey;
		oldScore = PlayerPrefs.GetInt ( saveKey);
		nowScore = score.GetPoint ();

		// スコアの記入
		scoreText.text = "Score : " + nowScore;
		if (oldScore < nowScore) 
		{
			highScoreText.SetActive(true);
			score.Save(); // スコアの更新.
		}
	}

	public void PushBackToSelectButton()
	{
		Time.timeScale = 1F; // 処理時間を戻す.

		Debug.Log (Application.loadedLevel);

		if (Application.loadedLevel == 3) 
		{
			Application.LoadLevel ("ending");
		} 
		else {
			Application.LoadLevel ("title");
		}




	}
}
