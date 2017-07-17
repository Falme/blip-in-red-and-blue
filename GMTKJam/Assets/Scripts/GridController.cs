using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridController : MonoBehaviour {

    public GameObject player, empty, wall, turret, door, gem;

    public GridPosition PlayerCharacterPosition
    {
        get { return playerCharacter.Position; }
    }

	public float originX, originY;

	public int actualLevel;
    public int level_width, level_height;
    public GridObject[,] level_objects;
    private PlayerCharacter playerCharacter;

	private bool loaded = false;

	private GameObject savedDoor;
    
	public TextAsset[] levelMap;
	public TextAsset[] levelDoor;

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
					playerCharacter.transform.position = new Vector2(originX + (1 * x), originY + (-1 * y));
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
						level_objects [x, y] = Instantiate (door).GetComponent<ExitDoor> ();
					savedDoor = level_objects [x, y].gameObject;
						level_objects [x, y].color = GridObject.Colors.blue;
						level_objects [x, y].Position = new GridPosition (x, y);
						level_objects [x, y].GetComponent<ExitDoor> ().turnsLeft = Int32.Parse(levelDoor [actualLevel].text);
                        break;
                    case ("RD"): //Red Door
                        level_objects[x, y] = Instantiate(door).GetComponent<ExitDoor>();
					savedDoor = level_objects [x, y].gameObject;
                        level_objects[x, y].color = GridObject.Colors.red;
						level_objects[x, y].Position = new GridPosition(x, y);
						level_objects [x, y].GetComponent<ExitDoor> ().turnsLeft = Int32.Parse(levelDoor [actualLevel].text);
					break;
				case ("RU"): //Red Unlocked Door
					level_objects [x, y] = Instantiate (door).GetComponent<ExitDoor> ();
					savedDoor = level_objects [x, y].gameObject;
						level_objects [x, y].GetComponent<ExitDoor> ().locked = false;
						level_objects [x, y].GetComponent<ExitDoor> ().refreshStatus();
						level_objects[x, y].color = GridObject.Colors.red;
						level_objects[x, y].Position = new GridPosition(x, y);
						level_objects [x, y].GetComponent<ExitDoor> ().turnsLeft = Int32.Parse(levelDoor [actualLevel].text);
					break;
                    default: //Empty
                        level_objects[x, y] = Instantiate(empty).GetComponent<EmptySpace>();
                        level_objects[x, y].Position = new GridPosition(x, y);
                        break;
                }

				level_objects[x, y].transform.position = new Vector2(originX + (1 * x),originY + (-1 * y));

            }
        }

		loaded = true;
    }

    static public string[] SplitCsvLine(string line)
    {
        return line.Split(';');
    }

    public void playerHasMoved()
    {
		if(loaded){
			//Diminui o Numero das portas
			GameObject[] doors = GameObject.FindGameObjectsWithTag ("ExitDoor");

			//Do it to All Doors
			foreach (GameObject door in doors) {
				if(!door.GetComponent<ExitDoor> ().jammed)
					door.GetComponent<ExitDoor> ().decrementNumber ();
			}

			//-----------------------------------------------------
		}
    }

	public void nextLevel(){
		actualLevel++;
		if (actualLevel != 15) {
			loaded = false;
			loadLevel ();
			savedDoor.GetComponent<ExitDoor> ().turnsLeft++;
		} else {
			SceneManager.LoadScene (0);
		}
	}

	public void loadLevel(){

		Destroy (playerCharacter.gameObject);
		for (int y = 0; y < level_height; y++)
		{
			for (int x = 0; x < level_width; x++)
			{
				Destroy (level_objects [x, y].gameObject);
				//setEmpty (x, y);
			}
		}

		//Limpar Bullets
		GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

		foreach (GameObject bullet in bullets) {
			Destroy (bullet);
		}


		setMapUp(levelMap[actualLevel].text);
	}



    // Use this for initialization
    void Start () {
        //level_objects = new GridObject[level_height, level_width];
        level_objects = new GridObject[level_width, level_height];
		setMapUp(levelMap[actualLevel].text);
    }
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) {
			for (int y = 0; y < level_height; y++)
			{
				for (int x = 0; x < level_width; x++)
				{
					Debug.Log (level_objects [x, y].gameObject);
					//setEmpty (x, y);
				}
			}
		}


	}

	//-------------------------------------------------------------------

	public void setEmpty(int x, int y){
		level_objects[x, y] = Instantiate(empty).GetComponent<EmptySpace>();
		level_objects[x, y].Position = new GridPosition(x, y);
	}
}
