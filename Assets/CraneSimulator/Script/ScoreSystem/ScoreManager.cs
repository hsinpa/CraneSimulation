using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ScoreManager {

	private static ScoreManager instance = null;		

	public static ScoreManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ScoreManager();
			}
			return instance;
		}
	}

	public static bool HasInstance
	{
		get
		{
			return (instance != null);
		}
	}

	// Explicit static constructor to tell C# compiler
	// not to mark type as beforefieldinit
	static ScoreManager() {  }
	private ScoreManager() {  }

	private Dictionary<string, Score> _score_dictionary = new Dictionary<string, Score>();

	public void Load(TextAsset p_textAsset) {
		TextAsset deductionTextAsset = Resources.Load<TextAsset>("Database/deduction_sheet");
		_score_dictionary = ParseJSONToDict( JSON.Parse(p_textAsset.text) );
	}

	public Dictionary<string, Score> ParseJSONToDict(JSONNode p_jsonNode) {
		Dictionary<string, Score> score_dictionary = new Dictionary<string, Score>();
		foreach(JSONNode emp in p_jsonNode.Keys)
		{
			if (!score_dictionary.ContainsKey(emp.Value)) {
				var single_statement_node = p_jsonNode[emp.Value];
				bool isFail = single_statement_node["value"].IsString;

				score_dictionary.Add(emp.Value,  new Score((isFail) ? 0 : single_statement_node["value"].AsInt, isFail, single_statement_node["text"].Value ));
			}
		}

		return score_dictionary;
	}

	public Score GetValue(string p_key) {
		if (!_score_dictionary.ContainsKey(p_key)) return new Score();
		return _score_dictionary[p_key];
	}

	public struct Score {
		public int score;
		public bool isFail;
		public string description;

		public Score (int p_score, bool p_isFail, string p_description) {
			score = p_score;
			isFail = p_isFail;
			description = p_description;
		}
	}

}
