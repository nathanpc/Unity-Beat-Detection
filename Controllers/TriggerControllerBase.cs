using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for a trigger audio controller.
/// </summary>
public abstract class TriggerControllerBase : MonoBehaviour {
	[Header("Audio Processor")]
	[SerializeField] protected AudioProcessor _processor = null;
	[SerializeField] protected float _localGain = 1.0f;
	[Header("Frequency Selector")]
	[SerializeField] protected uint _frequencyBin = 0;
	[SerializeField] protected bool _useFullSpectrumAverage = false;
	[Header("Change Range and Threshold")]
	[SerializeField] protected float _minimumChangeValue = 0.0f;
	[SerializeField] protected float _maximumChangeValue = 1.0f;
	[SerializeField] protected float _threshold = 1.0f;
	[Header("Debugging")]
	[SerializeField] protected bool _showValue = false;

	/// <summary>
	/// Sets things up internally. MUST ALWAYS BE CALLED FROM INHERITED CLASES.
	/// </summary>
	protected virtual void Start() {
		// Check if we actually have an Audio Processor set. Otherwise try to find it.
		if (Processor == null)
			Processor = FindObjectOfType<AudioProcessor>();

		// Check if the frequency bin is out of bounds.
		if (FrequencyBin >= Processor.NumberOfBands) {
			Debug.LogError("Frequency bin is greater than the number of " +
				"available bins in the Audio Processor.");
		}

		// Attach ourselves to the audio processor events.
		Processor.onSpectrum.AddListener(OnSpectrumEventHandler);
	}

	/// <summary>
	/// Does things on every update.
	/// </summary>
	protected virtual void Update() {
		// For now do nothing...
	}

	/// <summary>
	/// Does things on every frame.
	/// </summary>
	protected virtual void FixedUpdate() {
		// For now do nothing...
	}

	/// <summary>
	/// Handles the <see cref="AudioProcessor.onSpectrum"/> events.
	/// </summary>
	/// <param name="spectrum">Audio spectrum in frequency bins.</param>
	private void OnSpectrumEventHandler(float[] spectrum) {
		// Get the value of our specified frequency bin.
		float value = spectrum[FrequencyBin];

		// Do we actually want the average of everything?
		if (UseFullSpectrumAverage) {
			// Sum up all the frequency bin values.
			value = 0.0f;
			foreach (float freqValue in spectrum)
				value += freqValue;

			// Take the average.
			value /= Processor.NumberOfBands;
		}

		// Apply our local gain.
		value *= Gain;

		// Cap the value if needed.
		if (value < MinimumValue) {
			value = MinimumValue;
		} else if (value > MaximumValue) {
			value = MaximumValue;
		}

		// Show the computed value?
		if (ShowValue)
			Debug.Log(value);

		// Trigger the event.
		if (value >= Threshold)
			Triggered(value);
	}

	/// <summary>
	/// Function that will be called whenever we have a trigger event fired.
	/// </summary>
	/// <param name="value">Value that triggered this event.</param>
	abstract protected void Triggered(float value);

	/// <summary>
	/// Audio processor to get our spectrum analysis from.
	/// </summary>
	public AudioProcessor Processor {
		get { return _processor; }
		set { _processor = value; }
	}

	/// <summary>
	/// Frequency bin number to react to.
	/// </summary>
	public uint FrequencyBin {
		get { return _frequencyBin; }
		set { _frequencyBin = value; }
	}

	/// <summary>
	/// Use the full spectrum average value instead of a single frequency bin
	/// value? If True, this option will just average out all of the bins and
	/// ignore the specified <see cref="FrequencyBin"/> value.
	/// </summary>
	public bool UseFullSpectrumAverage {
		get { return _useFullSpectrumAverage; }
		set { _useFullSpectrumAverage = value; }
	}

	/// <summary>
	/// Minimum value that will be capped for the lowest spectral point.
	/// </summary>
	public float MinimumValue {
		get { return _minimumChangeValue; }
		set { _minimumChangeValue = value; }
	}

	/// <summary>
	/// Maximum value that will be capped for the lowest spectral point.
	/// </summary>
	public float MaximumValue {
		get { return _maximumChangeValue; }
		set { _maximumChangeValue = value; }
	}

	/// <summary>
	/// Threshold to trigger an event.
	/// </summary>
	public float Threshold {
		get { return _threshold; }
		set { _threshold = value; }
	}

	/// <summary>
	/// Audio gain for this specific script. Used to amplify or attenuate the
	/// desired effect.
	/// </summary>
	public float Gain {
		get { return _localGain; }
		set { _localGain = value; }
	}

	/// <summary>
	/// Show the change value for debugging purposes.
	/// </summary>
	public bool ShowValue {
		get { return _showValue; }
		set { _showValue = value; }
	}
}
