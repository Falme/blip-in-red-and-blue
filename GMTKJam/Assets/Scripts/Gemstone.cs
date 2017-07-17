using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemstone : GridObject {

	//Posicao da Gemstone
	public int x, y;


	//-----------------------------------------------------------------------------------

	public void Start()
	{
		reColor (this.color);
	}

	//-----------------------------------------------------------------------------------

	//Chama um objeto Porta para destrancar
	public void collideDoor(ExitDoor door){
		bool unlocked = door.unlockDoor (this.color);
		if (unlocked)
			Destroy (gameObject);
	}

	//Gemstone recebe impacto do Jogador e é jogado para outro Tile (movement)
	public void thrown(int dirX, int dirY, int force){
		while (force > 0) {
			
			GridPosition nextPosition = new GridPosition (Position.X + dirX, Position.Y - dirY);
			GridObject nextPositionObject = GameManager.level_objects [nextPosition.X, nextPosition.Y];

			if (nextPositionObject.GetType () == typeof(EmptySpace)) {
				
				invertObjects (Position.X, Position.Y, nextPosition.X, nextPosition.Y);
				Position = nextPosition;

			} else if(nextPositionObject.GetType () == typeof(ExitDoor)){
				if (nextPositionObject.GetComponent<ExitDoor> ().locked &&
				   !nextPositionObject.GetComponent<ExitDoor> ().jammed &&
				   nextPositionObject.GetComponent<ExitDoor> ().color == this.color) {
					GameManager.setEmpty (Position.X, Position.Y);
					Position = nextPosition;
				}
				force = 0;
			} else {
				force = 0;
			}
			force--;
			transform.position = refreshPosition (Position.X, Position.Y);
		}
	}

	void invertObjects(int objAX,int objAY,int objBX, int objBY){
		GridObject save = GameManager.level_objects [objAX, objAY];
		GameManager.level_objects[objAX, objAY] = GameManager.level_objects [objBX, objBY];
		GameManager.level_objects[objBX, objBY] = save;
	}


	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "ExitDoor") {
			collideDoor(col.gameObject.GetComponent<ExitDoor> ());
		}
	}


}
