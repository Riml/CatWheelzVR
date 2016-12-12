using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class UIControlsIntro : MonoBehaviour {


    public GameObject playBtn;
    public GameObject howToBtn;
    public GameObject tutorial;
    public GameObject loading;
    
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame() {

        SceneManager.LoadScene("main_tablet");
    }
    public void StartGameVR()
    {

        if (loading)
            loading.SetActive(true);
        SceneManager.LoadScene("main_vr");
    }

    public void BackBtn()
    {
        tutorial.SetActive(false);
        playBtn.SetActive(true);
        howToBtn.SetActive(true);

    }

    public void HowToBtn()
    {
        tutorial.SetActive(true);
        playBtn.SetActive(false);
        howToBtn.SetActive(false);


    }
}
