using UnityEngine;
using System.Collections;

public class FlowerScript : MonoBehaviour {
  public int X = 0, Y = 0;
  public bool eaten = false;
  public Vector3 GetPosVect()
  {
    return new Vector3(X, Y, -4f);
  }
  public void SetPos(Vector3 pos)
  {
    X = Mathf.RoundToInt(pos.x);
    Y = Mathf.RoundToInt(pos.y);
    transform.position = new Vector3(X, Y, -4f);
  }
  public bool VectMatchesPos(Vector3 vect)
  {
    return Mathf.RoundToInt(vect.x) == X && Mathf.RoundToInt(vect.y) == Y;
  }
  // Use this for initialization
  void Start () {
    SetPos(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void SetEaten(bool eaten)
  {
    this.eaten = eaten;
    GetComponent<SpriteRenderer>().enabled = !eaten;
  }
}
