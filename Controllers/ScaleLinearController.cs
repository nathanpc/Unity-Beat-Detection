using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the scale of a component linearly according to a music.
/// </summary>
public class ScaleLinearController : LinearControllerBase {
	[Space(10)]
	[Header("Object Scaling")]
	public bool scaleX = true;
	public float xScaleFactor = 1.0f;
	public bool scaleY = true;
	public float yScaleFactor = 1.0f;
	public bool scaleZ = true;
	public float zScaleFactor = 1.0f;
	private Vector3 targetScale;

	protected override void Start() {
		base.Start();
		targetScale = new Vector3();
	}

	protected override void Update() {
		base.Update();

		// Smoothly transition to the target position.
		transform.localScale = Vector3.Lerp(transform.localScale, targetScale,
			Time.deltaTime * ChangeSpeed);
	}

	protected override void Change(float value) {
		// Grab the current scale values.
		targetScale = transform.localScale;

		// Set the values if needed.
		if (scaleX)
			targetScale.x = value * xScaleFactor;
		if (scaleY)
			targetScale.y = value * yScaleFactor;
		if (scaleZ)
			targetScale.z = value * zScaleFactor;
	}
}
