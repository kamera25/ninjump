using UnityEngine;
using System.Collections;

public enum Stage
{
	none,
	sky,
	city,
	space
}

public class TitleMenuController : MonoBehaviour 
{
	[SerializeField]
	GameObject helpCanvas = null;
	Stage stage = Stage.none;

	void Start()
	{
		Time.timeScale = 1F;
	}

	/// <summary>
	/// ステージ選択画面を呼び出す.
	/// </summary>
	public void CallSelectStageMenu()
	{
		GameObject.Instantiate<GameObject>( Resources.Load<GameObject>("Prefabs/Menu/StageSelectCanvas"));
		GameObject.Find ("StartTap").SetActive(false);
	}

	public void CallSkyScene()
	{
		stage = Stage.sky;
		helpCanvas.SetActive (true);
	}
		
	public void CallCityScene()
	{
		stage = Stage.city;
		helpCanvas.SetActive (true);
	}
	
	public void CallSpaceScene()
	{
		stage = Stage.space;
		helpCanvas.SetActive (true);
	}

	public void GoToStage()
	{
		Application.LoadLevel ( stage.ToString());
	}

}
