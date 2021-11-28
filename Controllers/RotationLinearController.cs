using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the rotation of a component linearly according to a music.
/// </summary>
public class RotationLinearController : LinearControllerBase {
	[Space(10)]
	[Header("Object Rotation")]
	public bool changeXRotation = true;
	public float xChangeMultiplier = 1.0f;
	public bool changeYRotation = true;
	public float yChangeMultiplier = 1.0f;
	public bool changeZRotation = true;
	public float zChangeMultiplier = 1.0f;
	private Quaternion originalRotation;

	protected override void Start() {
		base.Start();

		// Grab the initial position.
		originalRotation = transform.localRotation;
	}

	protected override void Change(float value) {
		// Grab the original position.
		Quaternion rotation = originalRotation;

		// Set the values if needed.
		if (changeXRotation)
			rotation.x += value * xChangeMultiplier;
		if (changeYRotation)
			rotation.y += value * yChangeMultiplier;
		if (changeZRotation)
			rotation.z += value * zChangeMultiplier;

		// Apply new position values.
		transform.localRotation = rotation;
	}
}
