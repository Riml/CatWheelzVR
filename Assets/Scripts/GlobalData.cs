using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GlobalData : MonoBehaviour
{
    public bool VRMode = false;
    public GameObject GVR;
    public Image face;
    public InfestationMeter IM;
    public Text ratsKilledTxt;
    public int ratsKilled=0;

    public int ratCounter = 0;
    public Sprite[] faces;
    private float faceChangeTime = 1f;

    public UnityEngine.Networking.NetworkManager networkManager;



    // Use this for initialization
    void Start()
    {
       
        networkManager = GameObject.Find("NetworkManager").GetComponent<UnityEngine.Networking.NetworkManager>();

        if(!VRMode)
            StartCoroutine(StartTheGame());


    }

    // Update is called once per frame
    void Update()
    {
        if (ratCounter == 60) {

            StartCoroutine(EndGame());
        }



    }

    public void IncreaseInfectationLevel()
    {
       
        faceChangeTime = 1f;
        if (VRMode)
            return;
        ratCounter++;
       
        IM.addToValue(5);
        face.sprite = faces[1];
        if (ratCounter % 10 == 0)
        {
            faceChangeTime = 2.5f;
            face.sprite = faces[3];
        }
        StartCoroutine(resetFace());
    }

    public void DecreaseInfectationLevel()
    {
       

        faceChangeTime = 1f;
        if (VRMode)
            return;
        ratCounter--;
        IM.addToValue(-5);
        face.sprite = faces[2];
        StartCoroutine(resetFace());

        ratsKilled++;
        ratsKilledTxt.text = ratsKilled.ToString();
    }
    public void PauseTheGame()
    {
        Debug.Log("Settings pressed");
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    IEnumerator resetFace()
    {

        yield return new WaitForSeconds(faceChangeTime);
        face.sprite = faces[0];

    }

    IEnumerator EndGame() {

        UpdateScoreSave();
        face.sprite = faces[2];

        yield return new WaitForSeconds(3f);
        if (VRMode)
        {
            networkManager.StopClient();
            SceneManager.LoadScene("start_vr");
           
        }
        else {
            networkManager.StopHost();
            SceneManager.LoadScene("start_tablet");
          
        }
    }

    IEnumerator StartTheGame() {

        yield return new WaitForSeconds(1.5f);
        networkManager.StartHost();


    }

    private void UpdateScoreSave() {
        if (ratsKilled > PlayerPrefs.GetInt("HighScore")) {
            PlayerPrefs.SetInt("HighScore", ratsKilled);
        }

        PlayerPrefs.Save();
    }
}
