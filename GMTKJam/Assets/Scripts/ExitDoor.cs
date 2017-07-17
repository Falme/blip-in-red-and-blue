using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : GridObject {

	//Posicao da Porta de Saida
	public int x, y;

	//Sprites de status da porta (Destrancado ou Trancado)
	public Sprite unlockedSprite, lockedSprite;

	//Status da porta, precisa de uma Gemstone da mesma cor para destravar
	public bool locked;

	//Se a porta está fechada para sempre
	public bool jammed;

	//Quantos turnos restantes até a porta ficar fechada para sempre
	public int turnsLeft;

	//-----------------------------------------------------------------------------------

	public void Start(){
		jammed = false;
		refreshText ();
		refreshStatus ();
		reColor (this.color);
	}


	/*public void Update(){
		if (Input.GetKeyDown (KeyCode.R)) {
			
		}
	}*/


	//função para diminuir o numer ode turnos restantes
	public void decrementNumber(){
		this.turnsLeft--;
		refreshText ();
		verifyTurnsLeft ();
	}

	//verifica quantos turnos restaram para dar GameOver
	public void verifyTurnsLeft(){
		//Se faltaram 0 turnos, o jogo termina
		if (this.turnsLeft == 0) {
			jammed = true;
			GameManager.loadLevel ();
		}
	}



	//Se a porta não precisa da Gema ou outra ação para poder passar
	//Ou seja, se está liberado para ir a próxima fase
	public bool unlockDoor(Colors color){
		if (!jammed && locked) {
			if (this.color == color) {
				GetComponent<SpriteRenderer> ().sprite = unlockedSprite;
				this.locked = false;
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                return true;
			} else {
				//Adicionar aqui animação de "Opa, Cor errada"
			}
		}

		return false;
	}

	//Se a porta não precisa da Gema ou outra ação para poder passar
	//Ou seja, se está liberado para ir a próxima fase
	public void lockDoor(){
		GetComponent<SpriteRenderer> ().sprite = lockedSprite;
		this.locked = true;
	}

	//Ao colidir com a porta, jogador irá trocar de fase/Level
	public void openDoor(){
		
	}

	//Atualiza o numero da porta, que indica 
	//quantos turnos faltam para o fim do jogo
	public void refreshText(){

		//Pega o primeiro filho e muda o texto (Verificar) 
		transform.GetChild(0).GetComponent<TextMesh> ().text = "" + this.turnsLeft;
	}

	public void refreshStatus(){
		if(this.locked)
			GetComponent<SpriteRenderer> ().sprite = lockedSprite;
		else
			GetComponent<SpriteRenderer> ().sprite = unlockedSprite;
			
	}

}
