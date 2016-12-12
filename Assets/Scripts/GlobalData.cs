using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GlobalData : MonoBehaviour
{
    public bool VRMode = false;
    public GameObject GVR;
    public Image face;
    public InfestationMeter IM;

    public int ratCounter = 0;
    public Sprite[] faces;


    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {




    }

    public void IncreaseInfectationLevel()
    {
        if (VRMode)
            return;
        ratCounter++;
        IM.addToValue(10);
        face.sprite = faces[1];
        StartCoroutine(resetFace());
    }

    public void DecreaseInfectationLevel()
    {
        if (VRMode)
            return;
        ratCounter++;
        IM.addToValue(-10);
        face.sprite = faces[2];
        StartCoroutine(resetFace());
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

        yield return new WaitForSeconds(1f);
        face.sprite = faces[0];

    }
}
