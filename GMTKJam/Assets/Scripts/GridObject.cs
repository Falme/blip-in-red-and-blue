using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{

	//---------------------------------------------------
	//					Enums
	//---------------------------------------------------

	public enum Colors
	{
		red,blue
	}

	//---------------------------------------------------

	public Colors color;

    public GridController GameManager;

    public GridPosition Position { get; set; }

    public void Awake()
    {
        GameManager = FindObjectOfType<GridController>();
    }

    public virtual void doAfterPlayer() { }

	public virtual void doTimed(){ }

    public virtual void playerCollide() { }

    public virtual string getPlayerInteraction() { return ""; }

	//Mudar Posicao
	public virtual Vector2 refreshPosition(int x, int y){
		return new Vector2 (GameManager.originX + (1 * x), GameManager.originY + (-1 * y));
	}

	//Colorir o Objeto
	public virtual void reColor(Colors color){
		this.color = color;
		if(color == Colors.red)
			this.transform.GetComponent<SpriteRenderer> ().color = Color.red;
		if(color == Colors.blue)
			this.transform.GetComponent<SpriteRenderer> ().color = Color.blue;
	}


}