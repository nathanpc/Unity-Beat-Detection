using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers an animation according to a music.
/// </summary>
public class AnimationTriggerController : TriggerControllerBase {
	[Space(10)]
	[Header("Animation")]
	public Animator animator;
	public string triggeredAnimationName;

	protected override void Start() {
		base.Start();

		// Try to get the animator if one wasn't set.
		if (animator == null) {
			animator = GetComponent<Animator>();
			if (animator == null)
				Debug.LogError("Couldn't find the associated animator component.");
		}
	}

	protected override void Triggered(float value) {
		animator.SetTrigger(triggeredAnimationName);
	}
}
