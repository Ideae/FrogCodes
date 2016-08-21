using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {
  public static int currentLevelIndex = 1;
  public static GameObject currentLevelParent;

  public static List<FrogScript> currentFrogs = new List<FrogScript>();

  public bool randomizeLevels = false;
	// Use this for initialization
	void Start () {
    if (randomizeLevels)
    {
      RandomizeLevel(1);
    }
    else {
      LoadLevelByIndex(1);
    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void LoadLevelByIndex(int index)
  {
    Transform nextLevelTransform = transform.FindChild("Level" + index);
    if (nextLevelTransform == null)
    {
      print("Game Complete");
      return;
    }
    if (currentLevelParent != null)
    {
      currentLevelParent.SetActive(false);
    }
    GameObject nextLevel = nextLevelTransform.gameObject;
    nextLevel.SetActive(true);
    currentLevelParent = nextLevel;

    currentFrogs = nextLevel.GetComponentsInChildren<FrogScript>().ToList();

    foreach(FrogScript frogScript in currentFrogs)
    {
      var v = frogScript.transform.position;
      frogScript.SetFrogPos(new Vector3(v.x, v.y, -3f));
      print(frogScript.transform.position);
    }
  }

  public void RandomizeLevel(int seed)
  {
    Transform nextLevelTransform = transform.FindChild("Level1");
    if (nextLevelTransform == null)
    {
      print("Game Complete");
      return;
    }
    if (currentLevelParent != null)
    {
      currentLevelParent.SetActive(false);
    }
    GameObject nextLevel = nextLevelTransform.gameObject;
    nextLevel.SetActive(true);
    currentLevelParent = nextLevel;

    currentFrogs = nextLevel.GetComponentsInChildren<FrogScript>().ToList();
    foreach (FrogScript frogScript in currentFrogs)
    {
      Destroy(frogScript.gameObject);
    }
    var currentFlowers = nextLevel.GetComponentsInChildren<FlowerScript>().ToList();
    foreach (FlowerScript currentFlower in currentFlowers)
    {
      Destroy(currentFlower.gameObject);
    }

    GameObject frogPrefab = Resources.Load<GameObject>("Frog");
    GameObject flowerPrefab = Resources.Load<GameObject>("Flower");
    
    Random.seed = seed;
    int frogCount = Random.Range(1, 5);
    HashSet<Point> points = new HashSet<Point>();

    for(int i = 0; i < frogCount; i++)
    {
      Point p = new Point(0, 0);
      //do
      //{
      //  p.x = (int)Random.Range(-Game.halfWidth, Game.halfWidth);
      //  p.y = (int)Random.Range(-Game.halfHeight, Game.halfHeight);
      //  print(p.x + " : " + p.y);
      //} while (points.Contains(p));
      points.Add(p);
    }
    int counter = 0;
    foreach(Point p in points)
    {
      if (counter++ < points.Count/2)
      {
        GameObject froggy = (GameObject)Instantiate(frogPrefab, new Vector3(p.x, p.y, -3f), Quaternion.identity);
      }
      else
      {
        GameObject flowery = (GameObject)Instantiate(flowerPrefab, new Vector3(p.x, p.y, -4f), Quaternion.identity);
      }
    }
  }

  
}

public struct Point
{
  public int x;
  public int y;
  public Point(int x, int  y)
  {
    this.x = x;
    this.y = y;
  }
}
