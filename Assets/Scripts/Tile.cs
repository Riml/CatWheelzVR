using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private int tileType;
	public Material[] tileMaterials;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Set tile type
	public void setTileType(int type){
		gameObject.GetComponent<Renderer>().material = tileMaterials [type];
	}
}
