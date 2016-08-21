using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class FrogScript : MonoBehaviour {
  public enum FrogState
  {
    Normal,
    Moving,
    Fed
  }
  public FrogState frogState = FrogState.Normal;
  public int frogX = 0, frogY = 0;
  public float frogMoveDelay = 1f;
  public List<KeyCode> frogMoves = new List<KeyCode>();
  public List<GameObject> arrowList = new List<GameObject>();
  //public bool frogFed = false;

    private int frogId = 0;
    private static int frogCounter = 0;


  private EnterButtonScript enterButton;
  private static Text instructionText;

  public Vector3 GetFrogPosVect()
  {
    return new Vector3(frogX, frogY, -2f);
  }
  public void SetFrogPos(Vector3 pos)
  {
    frogX = Mathf.RoundToInt(pos.x);
    frogY = Mathf.RoundToInt(pos.y);
    transform.position = new Vector3(frogX, frogY, -3f);
  }
  public bool VectMatchesFrogPos(Vector3 vect)
  {
    return Mathf.RoundToInt(vect.x) == frogX && Mathf.RoundToInt(vect.y) == frogY;
  }
  
  private Object[] sprites;
  private int count = 1;

  void Start () {
    //GameObject frogPrefab = (GameObject)Resources.Load("Frog");
    //frog = (GameObject)Instantiate(frogPrefab, GetFrogPosVect(), Quaternion.identity);

    frogId = frogCounter++;

    sprites = Resources.LoadAll("frogs");
    enterButton = FindObjectOfType<EnterButtonScript>();
    if (instructionText == null)
    {
      instructionText = GameObject.Find("InstructionText").GetComponent<Text>();
      instructionText.gameObject.SetActive(false);
    }
  }
	

	void Update () {
	  
  }

  void PollForFaceChange()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      int add = count == sprites.Length - 1 ? 2 : 1;
      count = (count + add) % sprites.Length;
      SetFrogFace(count);
    }
  }

  void SetFrogFace(int index)
  {
    Sprite sprite = (Sprite)sprites[index];
    //print(count + "NAME: " + sprite.name);
    GetComponent<SpriteRenderer>().sprite = sprite;
  }

  public void EnterFrogControlState()
  {
    Game.state = GameState.ControlFrog;
    frogState = FrogState.Moving;
    Game.currentFrogControlling = this;
    frogMoves = new List<KeyCode>();
    //give affordance so they player knows they don't control the player atm
    arrowList = new List<GameObject>();

    instructionText.gameObject.SetActive(true);
    instructionText.text = "Direct Frog to Flower";
    Game.DeactivateArrowKeys();
  }
  public int maxMoves = 8;
  public void AddFrogAction(KeyCode keycode)
  {
    if (frogMoves.Count >= maxMoves) return;
    frogMoves.Add(keycode);
    AddArrow(keycode);
    if (frogMoves.Count == 1)
    {
      enterButton.EnableButton();
    }
  }
  public void AddArrow(KeyCode keycode)
  {
    float padding = 0.5f;
    Vector3 start = new Vector3(Game.halfWidth + 1 + padding + frogId, Game.halfHeight - arrowList.Count - padding, -3f);
    GameObject nextArrow = (GameObject)Instantiate(Game.upArrowPrefab, start, Quaternion.identity);
    nextArrow.transform.up = Game.keyCodeVects[keycode];
    //print(nextArrow.transform.forward);
    arrowList.Add(nextArrow);
  }

  public void FinishFrogControlState()
  {
    Game.state = GameState.ControlPlayer;
    StartFrogMoveState();

    enterButton.DisableButton();
    instructionText.gameObject.SetActive(false);
    Game.ActivateArrowKeys();
  }

  int moveCounter = 0;

  public void StartFrogMoveState()
  {
    StartCoroutine(FrogMovementCoroutine());
  }

  IEnumerator FrogMovementCoroutine()
  {
    while (moveCounter < frogMoves.Count)
    {
      KeyCode keycode = frogMoves.ElementAt(moveCounter);
      Vector3 vect = Game.keyCodeVects[keycode];
      Vector3 nextPos = transform.position + vect;

      bool withinGrid = MoveFrog(nextPos);
      if (!withinGrid)
      {
        //frog falls off level
      }

      GameObject arrow = arrowList.ElementAt(moveCounter);
      arrow.GetComponent<SpriteRenderer>().color = Color.green;

      moveCounter++;
      yield return new WaitForSeconds(frogMoveDelay);
    }
    FinishFrogMovingState();
    moveCounter = 0;
  }

  bool MoveFrog(Vector3 nextPos)
  {
    if (!Game.IsWithinGrid(nextPos)) return false;
    //transform.position = nextPos;
    SetFrogPos(nextPos);
    return true;
  }

  
  public void FinishFrogMovingState()
  {
    
    frogState = FrogState.Normal;
    foreach (var arrow in arrowList)
    {
      Destroy(arrow);
    }
    frogMoves = new List<KeyCode>();
    arrowList = new List<GameObject>();
    

    var flowers = transform.parent.GetComponentsInChildren<FlowerScript>();
    bool allEaten = true;
    foreach(var flower in flowers)
    {
      if (!flower.eaten)
      {
        if (flower.X == frogX && flower.Y == frogY)
        {
          flower.SetEaten(true);
          SetFrogFed();
        }
        else
        {
          allEaten = false;
        }
      }
    }
    if (allEaten)
    {
      print("Level Complete");

    }

    //give affordance so the player knows they are now controlling the player again.
  }

  public void SetFrogFed()
  {
    //frogFed = true;
    frogState = FrogState.Fed;
    SetFrogFace(2);
  }
}
