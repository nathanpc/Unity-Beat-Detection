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
	private Quaternion targetRotation;

	protected override void Start() {
		base.Start();

		// Grab the initial rotation.
		originalRotation = transform.rotation;
		targetRotation = transform.rotation;
	}

	protected override void Update() {
		base.Update();

		// Smoothly transition to the target rotation.
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,
			Time.deltaTime * ChangeSpeed);
	}

	protected override void Change(float value) {
		// Grab the original rotation.
		targetRotation = originalRotation;

		// Set the values if needed.
		if (changeXRotation)
			targetRotation.x += value * xChangeMultiplier;
		if (changeYRotation)
			targetRotation.y += value * yChangeMultiplier;
		if (changeZRotation)
			targetRotation.z += value * zChangeMultiplier;
	}
}
