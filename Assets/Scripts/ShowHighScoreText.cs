using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowHighScoreText : MonoBehaviour {
    public Text highScoreTxt;

	// Use this for initialization
	void Start () {
        highScoreTxt.text = "Most Rats Exterminated: "  + PlayerPrefs.GetInt("HighScore");

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
