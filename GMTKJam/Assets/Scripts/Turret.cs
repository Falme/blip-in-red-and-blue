using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : GridObject {

	//Posicao do Turret de acordo com o Mapa
	public int x, y;

	//direção do tiro do Turret, como ele é baseado em Linha, será entre 1 e -1
	private int aimX, aimY;

	//Angulo salvo para saber qual direcao olhar
	private int angle;

	//GameObject de Bullet
	public GameObject bulletGO;

	//Timer do tiro do Turret
	public float cooldown;
	private float nextFire = 0.0f;

	//É um Turret movel?
	public bool isMobile;



	//-----------------------------------------------------------------------------------

	public void Start()
	{
		reColor (this.color);
	}


	public void Update(){

		//Diminui o tempo do Cooldown
		nextFire -= Time.deltaTime;

		//Vira a mira no sentido horario
		turnAim (true, 0);

		//Procura pelo jogador
		bool foundPlayer = verifyPlayerPosition ();

		//Caso o timer tenha chegado a 0
		if (nextFire < 0f) {

			//Se encontrou o jogador
			if (foundPlayer){
				nextFire = cooldown;

				//Atira no Tile onde está o player
				shoot (GameManager.PlayerCharacterPosition.X, 
					GameManager.PlayerCharacterPosition.Y);
			}
		}
	}


	//-----------------------------------------------------------------------------------


	//Funcao para verificar se o Player está na linha do tiro do Turret
	public bool verifyPlayerPosition(){

		//Verifica se o player esta na mira do alvo
		int verifyTileX = Position.X;
		int verifyTileY = Position.Y;


		//Trocar numero de limite do loop 
		for(int index = 0; index<20; index++){

			//Vai pulando de tile em tile, de acordo com a mira, até achar o player
			verifyTileX += this.aimX;
			verifyTileY += this.aimY;

			//Verifica se o Tile verificado está com o player nele
			if (verifyTileX == GameManager.PlayerCharacterPosition.X &&
				verifyTileY == GameManager.PlayerCharacterPosition.Y) {

				return true;
			}

		}

		return false;
	}

	//Turret se move de um tile para outro por conta propria
	public void move(){
		if (isMobile) {
			//Se mover para outro Tile
			//Talvez adicionar Numero de casas a se pular
		}
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
		case 0:
		case 360:
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
			this.aimY = -1;

			break;
		case 270:
			this.aimX = 0;
			this.aimY = -1;

			break;
		case (270+45):
			this.aimX = 1;
			this.aimY = -1;

			break;
		}

		this.aimY *= -1;


	}

	//Turret recebe dano do Jogador e é jogado para outro Tile
	public void thrown(){

	}

	//Turret atira em um campo especifico
	public void shoot(int x, int y){
		GameObject go = (GameObject) Instantiate(bulletGO, transform.position, Quaternion.identity);
		go.transform.Rotate(0f,0f,(float)this.angle);
		go.GetComponent<Bullet> ().reColor (this.color);
		go.GetComponent<Bullet> ().setOriginTurret (gameObject);
	}

	//Turret morrendo
	public void die(){

	}

	//Após o jogador se mover, Turret irá se mover
	public override void doAfterPlayer() {
		this.move ();
	}


}
