using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for a linear audio controller.
/// </summary>
public abstract class LinearControllerBase : MonoBehaviour {
	[Header("Audio Processor")]
	[SerializeField] protected AudioProcessor _processor = null;
	[SerializeField] protected float _localGain = 1.0f;
	[Header("Frequency Selector")]
	[SerializeField] protected uint _frequencyBin = 0;
	[SerializeField] protected bool _useFullSpectrumAverage = false;
	[Header("Change Range")]
	[SerializeField] protected float _minimumChangeValue = 0.0f;
	[SerializeField] protected float _maximumChangeValue = 1.0f;

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

		// Send the value to our change event function.
		Change(value);
	}

	/// <summary>
	/// Function that will be called whenever we have to change the controlled
	/// object.
	/// </summary>
	/// <param name="value">New value that should be changed in the controlled
	/// object.</param>
	abstract protected void Change(float value);

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
	/// Audio gain for this specific script. Used to amplify or attenuate the
	/// desired effect.
	/// </summary>
	public float Gain {
		get { return _localGain; }
		set { _localGain = value; }
	}
}
