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
	private Vector3 targetPosition;

	protected override void Start() {
		base.Start();

		// Grab the initial position.
		originalPosition = transform.position;
		targetPosition = transform.position;
	}

	protected override void Update() {
		base.Update();

		// Smoothly transition to the target position.
		transform.position = Vector3.Lerp(transform.position, targetPosition,
			Time.deltaTime * ChangeSpeed);
	}

	protected override void Change(float value) {
		// Grab the original position.
		targetPosition = originalPosition;

		// Set the values if needed.
		if (changeXPosition)
			targetPosition.x += value * xChangeMultiplier;
		if (changeYPosition)
			targetPosition.y += value * yChangeMultiplier;
		if (changeZPosition)
			targetPosition.z += value * zChangeMultiplier;
	}
}
