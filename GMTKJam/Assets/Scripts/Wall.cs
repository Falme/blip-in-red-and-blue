using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : GridObject {

	//Posicao do Wall de acordo com o grid
	public int x, y;

	//-----------------------------------------------------------------------------------

	public void Start()
	{
		reColor (this.color);
	}

	// Update is called once per frame
	void Update () {
		
	}

	//-----------------------------------------------------------------------------------

	//retorna a cor do Wall (Verificar)
	public Colors getColor(){
		return this.color;
	}


	void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player")
        {
            //Debug.Log("Player collided with wall colored: " + this.color);
            col.gameObject.SendMessage("reverse");
        }
	}


}
