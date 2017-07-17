using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GridObject {

	//Velocidade que o Bullet vai se mover
	public float velocity;

	//Origem do Bullet
	private GameObject turret;


	//-----------------------------------------------------------------------------------

	//Update padrao do Unity
	public void Update(){
		transform.position += (transform.rotation * Vector3.right) * Time.deltaTime * velocity;

		verifyDestroy ();
	}

	//-----------------------------------------------------------------------------------

	//Verifica se ele sai do campo do jogo (AutoDestroi)
	public void verifyDestroy(){
		if (transform.position.x < -10f ||
			transform.position.x > 10f ||
			transform.position.y < -10f ||
			transform.position.y > 10f)
			Destroy (gameObject);
	}

	//Se colidir com o player, fere ou mata o player
	public void collidePlayer(PlayerCharacter player){
		player.die ();
	}

	//Ao colidir com o Gemstone, trocar a cor da pedra
	public void collideGemstone(Gemstone gemstone){
		gemstone.reColor (this.color);
	}

	//Ao colidir com o Outro Turret, trocar a cor do Turret
	public void collideTurret(Turret turret){
		turret.reColor (this.color);
	}

	//Ao colidir com objeto chao/parede colori-lo levando a multiplas funcoes
	public void collideWall(Wall wall){
		wall.reColor (this.color);
	}

	public void setOriginTurret(GameObject turret){
		this.turret = turret;
	}


	//Ao colidir com a Porta, trocar cor da porta
	public void collideDoor(ExitDoor exitDoor){
		exitDoor.reColor (this.color);
	}

	void OnTriggerEnter2D(Collider2D col){

		switch (col.tag) {
		case "Wall":
			collideWall (col.gameObject.GetComponent<Wall> ());
			Destroy (gameObject);
			break;
		case "ExitDoor":
			collideDoor (col.gameObject.GetComponent<ExitDoor> ());
			Destroy (gameObject);
			break;
		case "Gemstone":
			collideGemstone (col.gameObject.GetComponent<Gemstone> ());
			Destroy (gameObject);
			break;
		case "Player":
			collidePlayer (col.gameObject.GetComponent<PlayerCharacter> ());
			Destroy (gameObject);
			break;
		case "Turret":
			if (col.gameObject != this.turret) {
				collideTurret (col.gameObject.GetComponent<Turret> ());
				Destroy (gameObject);
			}
			break;
		}

	}


}
