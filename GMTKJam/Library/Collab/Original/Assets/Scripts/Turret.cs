using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : GridObject {

	//Posicao do Turret de acordo com o Mapa
	public int x;
	public int y;

	//Tipo de Turret, se Ele é vermelho ou Azul
	public string type;

	//direção do tiro do Turret, como ele é baseado em Linha, será entre 1 e -1
	public int aimX;
	public int aimY;

	//Angulo salvo para saber qual direcao olhar
	public int angle;

	//Funcao para verificar se o Player está na linha do tiro do Turret
	public void verifyPlayerPosition(){

		//Verifica se o player esta na mira do alvo
		int verifyTileX = this.x;
		int verifyTileY = this.y;

		//Trocar numero de limite do loop 
		for(int index = 0; index<20; index++){

			//Vai pulando de tile em tile, de acordo com a mira, até achar o player
			verifyTileX += this.aimX;
			verifyTileY += this.aimY;

			//Verifica se o Tile verificado está com o player nele
			if (verifyTileX == GameManager.PlayerCharacterPosition.x &&
			   verifyTileY == GameManager.PlayerCharacterPosition.y) {

				//Atira no Tile onde está o player
				shoot (GameManager.PlayerCharacterPosition.x, GameManager.PlayerCharacterPosition.y);
				break;
			}

		}


	}

	//Turret se move de um tile para outro por conta propria
	public void move(){

	}

	//Giro do turret para que ele possa mirar em diferentes direçoes
	public void turnAim(bool clockwise, int snap){

		if (clockwise)
			this.angle -= 45;
		else
			this.angle += 45;

		if (this.angle == -45) {
			this.angle = 360 - 45;
		} else if(this.angle == 360) {
			this.angle = 0;
		}

		switch (this.angle) {
		case (0 || 360):
			this.aimX = 1;
			this.aimY = 0;
		break;
		case 45:
			this.aimX = 1;
			this.aimY = 1;
			
		break;
		case 90:
			this.aimX = 0;
			this.aimY = 1;
			
		break;
		case (90+45):
			this.aimX = -1;
			this.aimY = 1;
			
		break;
		case (180):
			this.aimX = -1;
			this.aimY = 0;
			break;
		case (180+45):
			this.aimX = -1;
			this.aimY = 1;

			break;
		case 270:
			this.aimX = 0;
			this.aimY = 1;

			break;
		case (270+45):
			this.aimX = 1;
			this.aimY = -1;

			break;
		}


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
