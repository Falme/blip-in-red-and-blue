using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : GridObject {

	//Posicao do Turret de acordo com o Mapa
	public int x;
	public int y;



	//Funcao para verificar se o Player está na linha do tiro do Turret
	public void verifyPlayerPosition(){
		shoot (GameManager.PlayerCharacterPosition.X, GameManager.PlayerCharacterPosition.Y);
	}

	//Turret se move de um tile para outro por conta propria
	public void move(){

	}

	//Turret recebe dano do Jogador e é jogado para outro Tile
	public void thrown(){

	}

	//Turret atira em um campo especifico
	public void shoot(int x, int y){

	}

	//Turret morrendo
	public void die(){

	}
}
