using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
  ControlPlayer,
  ControlFrog,
}

public class Game : MonoBehaviour {
  
  public static GameState state = GameState.ControlPlayer;

  public static Dictionary<KeyCode, Vector3> keyCodeVects = new Dictionary<KeyCode, Vector3>()
  {
    { KeyCode.UpArrow, Vector3.up },
    { KeyCode.DownArrow, Vector3.down },
    { KeyCode.RightArrow, Vector3.right },
    { KeyCode.LeftArrow, Vector3.left },
  };
  public static GameObject floor, arrowkeys, upArrowPrefab;
  public static float halfWidth, halfHeight;

  public static FrogScript currentFrogControlling;

  //public static List<MonoBehaviour>[,] grid;
  
  // Use this for initialization
  void Start () {
    floor = GameObject.Find("Floor");
    halfWidth = floor.transform.localScale.x / 2;
    halfHeight = floor.transform.localScale.y / 2;

    //grid = new List<MonoBehaviour>[(int)floor.transform.localScale.x, (int)floor.transform.localScale.y];

    GameObject arrowkeysPrefab = Resources.Load<GameObject>("ArrowkeysPrefab");
    arrowkeys = (GameObject)Instantiate(arrowkeysPrefab, new Vector3(0f, -3f, -3f), Quaternion.identity);

    upArrowPrefab = Resources.Load<GameObject>("UpArrowPrefab");

  }

  static float totalTime = 0f;
  private static bool showArrowKeys = true;
	// Update is called once per frame
	void Update () {

    if (showArrowKeys)
    {
      float brightness = Mathf.Sin(totalTime * 2f) / 2f + 0.5f;
      arrowkeys.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, brightness);
      totalTime += Time.deltaTime;
    }
	}
  public static void ActivateArrowKeys()
  {
    totalTime = 0f;
    showArrowKeys = true;
  }
  public static void DeactivateArrowKeys()
  {
    arrowkeys.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
    showArrowKeys = false;
  }
  public static bool IsWithinGrid(Vector3 nextPos)
  {
    return nextPos.x >= -halfWidth && nextPos.x <= halfWidth && nextPos.y >= -halfHeight && nextPos.y <= halfHeight;
  }

  
}
