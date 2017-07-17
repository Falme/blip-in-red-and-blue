using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

    public GameObject player, empty, wall, turret, door, gem;

    public GridPosition PlayerCharacterPosition
    {
        get { return playerCharacter.Position; }
    }

    public int level_width, level_height;
    public GridObject[,] level_objects;
    private PlayerCharacter playerCharacter;
    public TextAsset levelMap;

    public void setMapUp(string csvText)
    {
        string[] lines = csvText.Split('\n');

        for (int y = 0; y < level_height; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < level_width; x++)
            {
				
                switch (row[x])
                {
                    case ("PL"): //Player
                        level_objects[x, y] = Instantiate(empty).GetComponent<EmptySpace>();
                        level_objects[x, y].Position = new GridPosition(x, y);
                        playerCharacter = Instantiate(player).GetComponent<PlayerCharacter>();
                        playerCharacter.Position = new GridPosition(x, y);
                        playerCharacter.transform.position = new Vector2(1 * x, -1 * y);
                        break;
                    case ("BW"): //Blue Wall
                        level_objects[x, y] = Instantiate(wall).GetComponent<Wall>();
                        level_objects[x, y].color = GridObject.Colors.blue;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    case ("RW"): //Red Wall
                        level_objects[x, y] = Instantiate(wall).GetComponent<Wall>();
                        level_objects[x, y].color = GridObject.Colors.red;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    case ("BT"): //Blue Turret
                        level_objects[x, y] = Instantiate(turret).GetComponent<Turret>();
                        level_objects[x, y].color = GridObject.Colors.blue;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    case ("RT"): //Red Turret
                        level_objects[x, y] = Instantiate(turret).GetComponent<Turret>();
                        level_objects[x, y].color = GridObject.Colors.red;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    case ("BG"): //Blue Gem
                        level_objects[x, y] = Instantiate(gem).GetComponent<Gemstone>();
                        level_objects[x, y].color = GridObject.Colors.blue;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    case ("RG"): //Red Gem
                        level_objects[x, y] = Instantiate(gem).GetComponent<Gemstone>();
                        level_objects[x, y].color = GridObject.Colors.red;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    case ("BD"): //Blue Door
                        level_objects[x, y] = Instantiate(door).GetComponent<ExitDoor>();
                        level_objects[x, y].color = GridObject.Colors.blue;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    case ("RD"): //Red Door
                        level_objects[x, y] = Instantiate(door).GetComponent<ExitDoor>();
                        level_objects[x, y].color = GridObject.Colors.red;
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                    default: //Empty
                        level_objects[x, y] = Instantiate(empty).GetComponent<EmptySpace>();
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                }

                level_objects[x, y].transform.position = new Vector2(1 * x, -1 * y);

            }
        }
    }

    static public string[] SplitCsvLine(string line)
    {
        return line.Split(';');
    }

    public void playerHasMoved()
    {
		//Diminui o Numero das portas
		GameObject[] doors = GameObject.FindGameObjectsWithTag ("ExitDoor");

		//Do it to All Doors
		foreach (GameObject door in doors) {
			if(!door.GetComponent<ExitDoor> ().jammed && door.GetComponent<ExitDoor> ().locked)
				door.GetComponent<ExitDoor> ().decrementNumber ();
		}

		//-----------------------------------------------------
    }

    // Use this for initialization
    void Start () {
        level_objects = new GridObject[level_width, level_height];
        setMapUp(levelMap.text);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
