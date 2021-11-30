using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls particle systems linearly according to a music.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class ParticleLinearController : LinearControllerBase {
	[Space(10)]
	[Header("Particle System Control")]
	public bool controlStartLifetime = true;
	public float startLifetimeMultiplier = 1.0f;
	public bool controlStartSpeed = true;
	public float startSpeedMultiplier = 1.0f;
	public bool controlRateOverTime = true;
	public float rateOverTimeMultiplier = 1.0f;
	private ParticleSystem particles;
	private float originalStartLifetime;
	private float targetStartLifetime;
	private float originalStartSpeed;
	private float targetStartSpeed;
	private float originalRateOverTime;
	private float targetRateOverTime;

	protected override void Start() {
		base.Start();

		// Get the associated particle system.
		particles = this.GetComponent<ParticleSystem>();

		// Reset the original and target values.
		originalStartLifetime = particles.main.startLifetime.constant;
		targetStartLifetime = particles.main.startLifetime.constant;
		originalStartSpeed = particles.main.startSpeed.constant;
		targetStartSpeed = particles.main.startSpeed.constant;
		originalRateOverTime = particles.emission.rateOverTime.constant;
		targetRateOverTime = particles.emission.rateOverTime.constant;
	}

	protected override void Update() {
		base.Update();

		// I don't know who designed this API, but it's clearly the worst piece
		// of shit I've ever encountered in my life. Whoever decided this is
		// fine needs to be fired and never write an API again.

		// Change start lifetime.
		if (controlStartLifetime) {
			ParticleSystem.MainModule main = particles.main;
			ParticleSystem.MinMaxCurve startLifetime = main.startLifetime;

			startLifetime.constant = targetStartLifetime;
			main.startLifetime = startLifetime;
		}

		// Change start speed.
		if (controlStartSpeed) {
			ParticleSystem.MainModule main = particles.main;
			ParticleSystem.MinMaxCurve startSpeed = main.startSpeed;

			startSpeed.constant = targetStartSpeed;
			main.startSpeed = startSpeed;
		}

		// Change the rate over time.
		if (controlRateOverTime) {
			ParticleSystem.EmissionModule emission = particles.emission;
			ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;

			rateOverTime.constant = targetRateOverTime;
			emission.rateOverTime = rateOverTime;
		}
	}

	protected override void Change(float value) {
		// Change start lifetime.
		if (controlStartLifetime) {
			targetStartLifetime = originalStartLifetime +
				(value * startLifetimeMultiplier);
		}

		// Change start speed.
		if (controlStartSpeed) {
			targetStartSpeed = originalStartSpeed +
				(value * startSpeedMultiplier);
		}

		// Change the rate over time.
		if (controlRateOverTime) {
			targetRateOverTime = originalRateOverTime +
				(value * rateOverTimeMultiplier);
		}
	}
}
