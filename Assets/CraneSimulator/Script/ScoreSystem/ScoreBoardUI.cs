using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class ScoreBoardUI : MonoBehaviour {
	public int max_display_line;
	
	private float startTime = -1;

	public int score {
		get {
			return _score;
		}

		set {
			_score = value;
			overallScore.text = score.ToString();	
		}
	}
	private int _score = 100;
	private GameObject scoreLinePrefab;

	private RectTransform scoreHolder;
	private Text timer, overallScore;
	

	public void SetUp(float p_max_time, int p_init_score) {
			scoreLinePrefab = Resources.Load<GameObject>("Prefab/UI/single_score_line");

			timer = transform.Find("timer").GetComponent<Text>();
			overallScore = transform.Find("overall/field").GetComponent<Text>();

			startTime = p_max_time;
			score = p_init_score;

			timer.text = "00:00";
			scoreHolder = transform.Find("score_records").GetComponent<RectTransform>();

			StartCoroutine(ClearAll());
	}

	public void AddScoreColumn(string p_desc, int p_score) {
		GameObject scoreObject = UtilityMethod.CreateObjectToParent(scoreHolder, scoreLinePrefab);
		scoreObject.transform.Find("info").GetComponent<Text>().text = p_desc;
		scoreObject.transform.Find("score").GetComponent<Text>().text = "-"+p_score;

		if (scoreHolder.childCount >= max_display_line+1) {
			GameObject.Destroy( scoreHolder.GetChild(0).gameObject );
		}
		score -= p_score;
	}

	void Update() {
		if (startTime < 0) return; 
		UpdateTimer();
	}

	private void UpdateTimer() {
		float passTime = Time.time - startTime;
		float minutes =  Mathf.Floor(passTime / 60);
		int second = Mathf.FloorToInt(passTime % 60);

		timer.text = minutes + " : " + (( second < 10 ) ? ("0"+second) : second.ToString());
	}

	private IEnumerator ClearAll() {
		yield return new WaitForEndOfFrame();
		UtilityMethod.ClearChildObject(scoreHolder);
	}

}
