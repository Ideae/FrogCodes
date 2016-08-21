using UnityEngine;
using System.Collections;

public class EnterButtonScript : MonoBehaviour {

  private BoxCollider2D boxCollider2D;
  private SpriteRenderer spriteRenderer;

  //private bool buttonEnabled = true;
	// Use this for initialization
	void Start () {
    boxCollider2D = GetComponent<BoxCollider2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
	  //if (Game.state == GameState.ControlFrog)
    //{
    //  if (!buttonEnabled)
    //  {
    //    EnableButton();
    //  }
    //}
    //else
    //{
    //  if (buttonEnabled)
    //  {
    //    DisableButton();
    //  }
    //}
	}

  void OnMouseDown()
  {
    //print("clicked");
    if (Game.state == GameState.ControlFrog)
    {
      Game.currentFrogControlling.FinishFrogControlState();
    } 
  }

  public void EnableButton()
  {
    //buttonEnabled = true;
    boxCollider2D.enabled = true;
    spriteRenderer.enabled = true;
  }
  public void DisableButton()
  {
    //buttonEnabled = false;
    boxCollider2D.enabled = false;
    spriteRenderer.enabled = false;
  }
}
