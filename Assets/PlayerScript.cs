using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
  
  // Use this for initialization
  void Start () {
    
  }
  
  void Update()
  {
    if (Game.state == GameState.ControlPlayer)
    {
      foreach (KeyCode keycode in Game.keyCodeVects.Keys)
      {
        if (Input.GetKeyDown(keycode))
        {
          Vector3 vect = Game.keyCodeVects[keycode];
          Vector3 nextPos = transform.position + vect;

          if (!Game.IsWithinGrid(nextPos)) break;
          //transform.position = nextPos;
          MovePlayer(nextPos);
          break;
        }
      }
    }
    else if (Game.state == GameState.ControlFrog)
    {
      if (Input.GetKeyDown(KeyCode.Return))
      {
        Game.currentFrogControlling.FinishFrogControlState();
      }
      else {
        foreach (KeyCode keycode in Game.keyCodeVects.Keys)
        {
          if (Input.GetKeyDown(keycode))
          {
            Game.currentFrogControlling.AddFrogAction(keycode);
          }
        }
      }
    }
  }
  
  void MovePlayer(Vector3 nextPos)
  {
    transform.position = nextPos;
    CheckPlayerCollisionWithFrogs(nextPos);
  }

  public void CheckPlayerCollisionWithFrogs(Vector3 nextPos)
  {
    foreach (FrogScript frogScript in LevelManager.currentFrogs)
    {
      if (frogScript.frogState == FrogScript.FrogState.Normal && frogScript.VectMatchesFrogPos(nextPos))
      {
        frogScript.EnterFrogControlState();
        break;
      }
    }
  }

}
