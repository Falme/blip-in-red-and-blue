using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : GridObject
{

    private bool isCharging, wasChargingLastFrame, canAddCharge, isMoving;
    public int currentForce;
    private int direction_x, direction_y;
	private Rigidbody2D rb;

	public GameObject numberForce;

    public void Start()
    {
		direction_x = 0;
		direction_y = 0;
        currentForce = 0;
        isCharging = false;
        wasChargingLastFrame = false;
		canAddCharge = true;
		numberForce.SetActive (false);
        isMoving = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void reverse()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
    }

    public void Update()
    {
        direction_x = Mathf.CeilToInt(Input.GetAxis("Horizontal"));
        direction_y = Mathf.CeilToInt(Input.GetAxis("Vertical"));

        if (direction_x != 0 || direction_y != 0)
        {
            showArrow(direction_x, direction_y);
        }
            else
            {
                hideArrow();
            }

			if((direction_x != 0 || direction_y != 0))
            	isCharging = Input.GetButton("Jump");

            if (isCharging && canAddCharge)
            {
                canAddCharge = false;
                Invoke("nextCharge", 2);
                currentForce++;
				Debug.Log("Current force: " + currentForce);

				numberForce.SetActive (true);
				numberForce.GetComponent<TextMesh> ().text = ""+currentForce;
            }
            if (!isCharging && wasChargingLastFrame)
            {
				dash();
				numberForce.SetActive (false);
            }

            wasChargingLastFrame = isCharging;
    }

    private void stop()
    {
        rb.velocity = new Vector2(0, 0);
        isMoving = false;
        canAddCharge = true;
    }

    private void nextCharge()
    {
        canAddCharge = true;
    }

    private void dash()
    {
        /*isMoving = true;
        rb.velocity = new Vector2(direction_x, direction_y);*/
        while(currentForce > 0)
        {
            GridPosition nextPosition = new GridPosition(Position.X + direction_x, Position.Y - direction_y);
            GridObject nextPositionObject = GameManager.level_objects[nextPosition.X, nextPosition.Y];

			if (nextPositionObject.GetType () == typeof(EmptySpace)) {
				Position = nextPosition;
			} else if (nextPositionObject.GetType () == typeof(ExitDoor)) {
				if (!nextPositionObject.GetComponent<ExitDoor> ().locked && !nextPositionObject.GetComponent<ExitDoor> ().jammed) {
					//Position = nextPosition;
					currentForce = 0;
					GameManager.nextLevel ();
					break;
				}
				else
					nextPositionObject.playerCollide();
			}
            else
            {
				//Position = nextPosition;
				if (nextPositionObject.GetType () == typeof(Gemstone))
					nextPositionObject.GetComponent<Gemstone>().thrown(direction_x,direction_y,currentForce);	
				nextPositionObject.playerCollide();
				currentForce = 0;

            }
            currentForce--;
			transform.position = refreshPosition (Position.X, Position.Y);
        }
        canAddCharge = true;
        GameManager.playerHasMoved();
        //Debug.Log("Current position x:" + Position.X + " y: " + Position.Y);
    }

    private void showArrow(float x, float y)
    {
        float angle = Mathf.Atan2(y, x);
        angle = (angle * 180f) / Mathf.PI;
        GameObject arrow = transform.GetChild(0).GetChild(0).gameObject;
        arrow.SetActive(true);
        transform.GetChild(0).transform.eulerAngles = new Vector3(
            transform.GetChild(0).transform.eulerAngles.x,
            transform.GetChild(0).transform.eulerAngles.y,
            angle);
    }

    private void hideArrow()
    {
        GameObject arrow = transform.GetChild(0).GetChild(0).gameObject;
        arrow.SetActive(false);
    }

    public void die()
    {
        GameManager.loadLevel();

    }

}