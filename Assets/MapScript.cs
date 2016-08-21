using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
    var square = Resources.Load("SquarePrefab");
    var floor = GameObject.Find("Floor");
    float halfWidth = floor.transform.localScale.x/2;
    float halfHeight = floor.transform.localScale.y/2;

    

    for (float xPos = -halfWidth; xPos <= halfWidth; xPos += 2f)
    {
      GameObject g = (GameObject)Instantiate(square, new Vector3(xPos, 0f, -0.5f), Quaternion.identity);
      g.transform.localScale = new Vector3(1f, halfHeight * 2, 1f);
      g.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.2f);
    }
    for (float yPos = -halfHeight; yPos <= halfHeight; yPos += 2f)
    {
      GameObject g = (GameObject)Instantiate(square, new Vector3(0f, yPos, -0.5f), Quaternion.identity);
      g.transform.localScale = new Vector3(halfWidth * 2, 1f, 1f);
      g.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.2f);
    }

  }
  

	// Update is called once per frame
	void Update () {
	}
}
