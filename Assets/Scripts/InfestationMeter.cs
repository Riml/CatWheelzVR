using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfestationMeter : MonoBehaviour {

	public Transform icon;
	public Transform loadingBar;
	public Sprite iconTexture;
	public Sprite loadingBarTexture;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;
	[SerializeField] private Color[] colours = new Color[3];
	[SerializeField] private int colourIndex;

	void Start(){
		currentAmount = 0;
		speed = 30;
		// Intensity colours
		colourIndex = 0;
		colours[0] = new Color(0, 255, 0);
		colours[1] = new Color(255, 255, 0);
		colours[2] = new Color(255, 0, 0);
		loadingBar.GetComponent<Image> ().color = colours[colourIndex];
	}

	// Update is called once per frame
	void Update () {
		//if (currentAmount < 100) {
		//	currentAmount += speed * Time.deltaTime;
		//}
		loadingBar.GetComponent<Image> ().fillAmount = currentAmount / 100;
		if (loadingBar.GetComponent<Image> ().fillAmount == 1) {
			if (colourIndex < colours.Length - 1) {
				colourIndex++;
				loadingBar.GetComponent<Image> ().color = colours[colourIndex];
				currentAmount = 0;
			}
		}
	}

	// Change value
	public void addToValue(int changeByAmount){
		currentAmount += changeByAmount;
		currentAmount = currentAmount < 0 ? 0 : currentAmount;
	}

	// Set speed
	public void setSpeed(int newSpeed){
		speed = newSpeed;
	}
   
}
