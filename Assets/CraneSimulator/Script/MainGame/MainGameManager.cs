using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utility;

public class MainGameManager : Singleton<MainGameManager> {

	#region private public variable
	public static int score {
		get {
			return _score;
		}
	}

	public int maxTime = 12;
	private static int _score;
	private int _maxScore = 100;
	private int _minScore = 60;
	private int _maxTime;

	
	public Transform bargage;
	public ScoreBoardUI scoreBoardUI;
	private RectTransform endGameUI;
	public Transform camera;
	private CameraTransition cameraTransition;

	private enum GameState {
		Normal,
		End
	}
	private GameState gameState;
	#endregion
	// Use this for initialization
	void Start () {
		ScoreManager.Instance.Load(Resources.Load<TextAsset>("Database/deduction_sheet"));

		if (camera != null)
			cameraTransition = camera.gameObject.AddComponent<CameraTransition>();

		Init();
	}

	void Init() {
		_score = _maxScore;
		_maxTime = maxTime;
		gameState = GameState.Normal;
		endGameUI = UtilityMethod.FindObject(transform.Find("view/ui").gameObject, "end_game_ui").GetComponent<RectTransform>();
		
		if (endGameUI)
			endGameUI.gameObject.SetActive(false);

		Transform environmentalObjects = transform.Find("view/environment");
		if (environmentalObjects != null) {
			BaseWorldTransform[] baseWorldTransform = GetComponentsInChildren<BaseWorldTransform>();
			foreach (var mObject in baseWorldTransform) mObject.SetUp(bargage);
		}

		scoreBoardUI.SetUp(Time.time, _score);
	}

	private void Update() {
		if (gameState == GameState.End) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	public void EndGameHandler() {
		gameState = GameState.End;
	}

	public void ShowEndGameUI(string p_message, int p_score, bool p_isSuccess) {
		if (endGameUI == null) return;
			endGameUI.gameObject.SetActive(true);

		Color32 failBGColor = new Color32(192, 25, 40, 202);
		Color32 successBGColor = new Color32(43, 152, 66, 202);

		Image endGameBG = endGameUI.Find("text_holder").GetComponent<Image>();
		endGameBG.color = (p_isSuccess) ? successBGColor : failBGColor;

		Text endGameField = endGameUI.Find("text_holder/field").GetComponent<Text>();

		string endgamestring = "模擬實績 " + p_score +"\n";
		endgamestring += (p_isSuccess) ? "測驗成功" : p_message;
		endgamestring += "\n\n請點擊按鍵Space 繼續";

		endGameField.text = endgamestring;
	}

	public static void DeductScore(string p_key) {
		if (Instance.gameState == GameState.End) return;

		var score = ScoreManager.Instance.GetValue(p_key);
		
		string failMessage = "";
		if (score.isFail) failMessage = score.description;

		if (failMessage == "") {
			_score -= score.score;
			MainGameManager.Instance.scoreBoardUI.AddScoreColumn(score.description, score.score);

			if (_score < Instance._minScore) 
				failMessage = "分數不得低於60分";
		}
		
		if (failMessage != "") {
			Instance.EndGameHandler();
			Instance.ShowEndGameUI(failMessage, _score, false);
		}

	}

	public void ChangeCameraXPosition(float xPos) {
		if (camera == null) return;
		Vector3 cameraPosition = camera.position;
		cameraTransition.TransitionTo(2, new Vector3(xPos, cameraPosition.y, cameraPosition.z));
	}
	
}
