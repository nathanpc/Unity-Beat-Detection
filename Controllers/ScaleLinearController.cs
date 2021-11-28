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

	protected override void Change(float value) {
		// Grab the current scale values.
		Vector3 scale = transform.localScale;

		// Set the values if needed.
		if (scaleX)
			scale.x = value * xScaleFactor;
		if (scaleY)
			scale.y = value * yScaleFactor;
		if (scaleZ)
			scale.z = value * zScaleFactor;

		// Apply new scaled values.
		transform.localScale = scale;
	}
}
