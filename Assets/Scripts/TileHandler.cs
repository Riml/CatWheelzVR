using UnityEngine;
using System.Collections;

public class TileHandler : MonoBehaviour {

	public GameObject tile;
	public ArrayList tiles = new ArrayList();
	public int tilesWide;
	public int tilesLong;
	public int centerWidth;
	public int maxTileHeight;
	public int minTileHeight;
	public Vector3 tileGenStartPoint;
	public bool additionalZones;

	// Use this for initialization
	void Start () {
		generateTiles ();	
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Generate tiles
	private void generateTiles(){

		// Create empty game objects to group tiles and attach mesh merge script
		GameObject innerTiles = new GameObject();
		innerTiles.name = "InnerTiles";
		//innerTiles.AddComponent <MergeMeshes>();
		innerTiles.transform.parent = gameObject.transform;

		GameObject outerTiles = new GameObject();
		outerTiles.name = "OuterTiles";
		//outerTiles.AddComponent <MergeMeshes>();
		outerTiles.transform.parent = gameObject.transform;

		// Center tile path bounds
		int minBound = (tilesWide) / 2 - (centerWidth / 2) - 1; 
		int maxBound = (tilesWide) / 2 + (centerWidth / 2);

		for (int i = 0; i < tilesWide; i++) {
			for (int j = 0; j < tilesLong; j++) {
				
				// new tile location
				Vector3 tileLocation = new Vector3 (tileGenStartPoint.x + i, 
													tileGenStartPoint.y, 
													tileGenStartPoint.z + j);

				GameObject newTile = Instantiate (tile, tileLocation, Quaternion.identity) as GameObject;

				// Set tile types
				if ((i < maxBound && i > minBound) || (j < maxBound && j > minBound)) {
					newTile.GetComponent<Tile> ().setTileType (0);
					newTile.transform.parent = innerTiles.transform;
				} else {
					// Select random tile type
					newTile.GetComponent<Tile> ().setTileType (Random.Range (1, 4));

					// Generate random tile height
					int tileHeight = Random.Range (minTileHeight, maxTileHeight + 1);
					float translateUp = tileHeight / 2f;

					// Transform local scale and translate tile appropriately
					newTile.transform.localScale += new Vector3(0, tileHeight, 0);
					newTile.transform.position += new Vector3 (0, translateUp, 0);

					// Set as a child object
					newTile.transform.parent = outerTiles.transform;
				}
				tiles.Add (newTile);
			}
		}

		// Generate additional zones
		if (additionalZones) {

			// Zone 2
			GameObject innerTiles2 = (GameObject)Instantiate (innerTiles, transform.position + new Vector3(tilesWide, 0, 0), Quaternion.identity);
			innerTiles2.name = "innerTiles2";
			innerTiles2.transform.parent = gameObject.transform;
			GameObject outerTiles2 = (GameObject)Instantiate (outerTiles, transform.position + new Vector3(tilesWide, 0, 0), Quaternion.identity);
			outerTiles2.name = "outerTiles2";
			outerTiles2.transform.parent = gameObject.transform;

			// Zone 3
			GameObject innerTiles3 = (GameObject)Instantiate (innerTiles, transform.position + new Vector3(-tilesWide, 0, 0), Quaternion.identity);
			innerTiles3.name = "innerTiles3";
			innerTiles3.transform.parent = gameObject.transform;
			GameObject outerTiles3 = (GameObject)Instantiate (outerTiles, transform.position + new Vector3(-tilesWide, 0, 0), Quaternion.identity);
			outerTiles3.name = "outerTiles3";
			outerTiles3.transform.parent = gameObject.transform;

			// Zone 4
			GameObject innerTiles4 = (GameObject)Instantiate (innerTiles, transform.position + new Vector3(0, 0, tilesLong), Quaternion.identity);
			innerTiles4.name = "innerTiles4";
			innerTiles4.transform.parent = gameObject.transform;
			GameObject outerTiles4 = (GameObject)Instantiate (outerTiles, transform.position + new Vector3(0, 0, tilesLong), Quaternion.identity);
			outerTiles4.name = "outerTiles4";
			outerTiles4.transform.parent = gameObject.transform;

			// Zone 5
			GameObject innerTiles5 = (GameObject)Instantiate (innerTiles, transform.position + new Vector3(0, 0, -tilesLong), Quaternion.identity);
			innerTiles5.name = "innerTiles5";
			innerTiles5.transform.parent = gameObject.transform;
			GameObject outerTiles5 = (GameObject)Instantiate (outerTiles, transform.position + new Vector3(0, 0, -tilesLong), Quaternion.identity);
			outerTiles5.name = "outerTiles5";
			outerTiles5.transform.parent = gameObject.transform;
		
		}
	}
}
