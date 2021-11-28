using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the position of a component linearly according to a music.
/// </summary>
public class PositionLinearController : LinearControllerBase {
	[Space(10)]
	[Header("Object Positioning")]
	public bool changeXPosition = true;
	public float xChangeMultiplier = 1.0f;
	public bool changeYPosition = true;
	public float yChangeMultiplier = 1.0f;
	public bool changeZPosition = true;
	public float zChangeMultiplier = 1.0f;
	private Vector3 originalPosition;

	protected override void Start() {
		base.Start();

		// Grab the initial position.
		originalPosition = transform.position;
	}

	protected override void Change(float value) {
		// Grab the original position.
		Vector3 position = originalPosition;

		// Set the values if needed.
		if (changeXPosition)
			position.x += value * xChangeMultiplier;
		if (changeYPosition)
			position.y += value * yChangeMultiplier;
		if (changeZPosition)
			position.z += value * zChangeMultiplier;

		// Apply new position values.
		transform.position = position;
	}
}
