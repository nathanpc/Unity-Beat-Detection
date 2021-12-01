using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls an light component linearly according to a music.
/// </summary>
public class LightLinearController : LinearControllerBase {
	[Space(10)]
	[Header("Light Settings")]
	public Light controlledLight;
	public bool changeIntensity = true;
	public float intensityChangeMultiplier = 1.0f;
	private float originalIntensity;
	private float targetIntensity;

	protected override void Start() {
		base.Start();

		// Get our light component.
		if (controlledLight == null)
			controlledLight = GetComponent<Light>();

		// Grab the initial intensity.
		originalIntensity = controlledLight.intensity;
		targetIntensity = controlledLight.intensity;
	}

	protected override void Update() {
		base.Update();

		// Smoothly transition to the target intensity.
		controlledLight.intensity = Mathf.Lerp(controlledLight.intensity,
			targetIntensity, Time.deltaTime * ChangeSpeed);
	}

	protected override void Change(float value) {
		// Grab the original position.
		targetIntensity = originalIntensity;

		// Set the intensity if required.
		if (changeIntensity)
			targetIntensity += value * intensityChangeMultiplier;
	}
}
