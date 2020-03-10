﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Region/Player/Player Movement")]
public class Movement : MonoBehaviour
{
	#region Variables
	[Header("Player Movement")]
	[Space(10)]
	[Header("Character Move Direction")]
	public JoyStick playerMovement;
	
	//vector3 called moveDirection
	public Vector3 moveDirection;

	//we will use this to apply movement in worldspace
	private CharacterController charCtrl;

	[Header("Character Variables")]

	//public float variables jumpSpeed, speed, gravity
	public float jumpSpeed;
	public float walkSpeed, gravity;
	public float runSpeed;
	public float airSpeed;
	public static bool canMove = true;

	#endregion
	#region Start
	void Start()
	{
		canMove = true;

		//charc is on this game object we need to get the character controller that is attached to it
		charCtrl = this.GetComponent<CharacterController>();
	}

	#endregion
	#region Update
	void Update()
	{
		if (canMove)
		{
			//if our character is grounded
			if (charCtrl.isGrounded)      // Running
			{
				//moveDir has the value of Input.Get Axis.. Horizontal, 0, Vertical
				moveDirection = new Vector3(playerMovement.input.x, 0, playerMovement.input.y);
				//moveDir is transformed in the direction of our moveDir
				moveDirection = transform.TransformDirection(moveDirection);
				//our moveDir is then multiplied by our speed
				moveDirection *= walkSpeed;

				//we can also jump if we are grounded so
				//if the input button for jump is pressed then
				if (Input.GetButton("Jump"))
				{
					//our moveDir.y is equal to our jump speed
					moveDirection.y = jumpSpeed;
				}
			}

			if (charCtrl.isGrounded && Input.GetKey("left shift"))      // Running
			{
				//moveDir has the value of Input.Get Axis.. Horizontal, 0, Vertical
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				//moveDir is transformed in the direction of our moveDir
				moveDirection = transform.TransformDirection(moveDirection);
				//our moveDir is then multiplied by our speed
				moveDirection *= runSpeed;

				if (Input.GetButton("Jump"))
				{
					//our moveDir.y is equal to our jump speed
					moveDirection.y = jumpSpeed;
				}
			}
		}
		//regardless of if we are grounded or not the players moveDir.y is always affected by gravity timesed my time.deltaTime to normalize it
		moveDirection.y -= gravity * Time.deltaTime;
		//we then tell the character Controller that it is moving in a direction timesed Time.deltaTime
		charCtrl.Move(moveDirection * Time.deltaTime);
	}
	#endregion
}
