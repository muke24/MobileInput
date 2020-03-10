using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

	public RotationAxes axes = RotationAxes.MouseXAndY;

	public JoyStick cameraMovement;

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;

	private float rotationX = 0F;
	private float rotationY = 0F;

	Quaternion originalRotation;

	void Start()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
		originalRotation = transform.localRotation;
	}

	void Update()
	{
		if (axes == RotationAxes.MouseXAndY)
		{
			// Read the mouse input axis
			rotationX += cameraMovement.input.x * sensitivityX;
			rotationY += cameraMovement.input.y * sensitivityY;
			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			rotationY = ClampAngle(rotationY, minimumY, maximumY);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
			transform.localRotation = originalRotation * xQuaternion * yQuaternion;
		}
		else if (axes == RotationAxes.MouseX)
		{
			rotationX += cameraMovement.input.x * sensitivityX;
			rotationX = ClampAngle(rotationX, minimumX, maximumX);
			Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}
		else
		{
			rotationY += cameraMovement.input.y * sensitivityY;
			rotationY = ClampAngle(rotationY, minimumY, maximumY);
			Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}
		
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
		{
			angle += 360F;
		}


		if (angle > 360F)
		{
			angle -= 360F;
		}

		return Mathf.Clamp(angle, min, max);
	}
}