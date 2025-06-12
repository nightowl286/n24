namespace N24.Format;

/// <summary>
/// 	Represents information about a single sleep entry.
/// </summary>
/// <param name="start">The start time of the sleep entry.</param>
/// <param name="end">The end time of the sleep entry.</param>
/// <param name="stages">The stages that have occurred during the sleep.</param>
/// <param name="quality">The quality of the sleep.</param>
/// <param name="mentalRecovery">The mental recovery that resulted from the sleep.</param>
/// <param name="physicalRecovery">The physical recovery that resulted from the sleep.</param>
/// <param name="cycles">The amount of sleep cycles that have occurred during the sleep.</param>
public sealed class SleepEntry(
	DateTimeOffset start,
	DateTimeOffset end,
	IReadOnlyList<SleepStage> stages,
	double? quality,
	double? mentalRecovery,
	double? physicalRecovery,
	double? cycles)
{
	#region Fields
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private SleepEntry? _previousEntry, _nextEntry;
	#endregion

	#region Properties
	/// <summary>The previous sleep entry in the data set.</summary>
	[JsonIgnore]
	public SleepEntry? PreviousEntry
	{
		get => _previousEntry;
		set
		{
			if (_previousEntry is not null)
				throw new InvalidOperationException("Overwriting the previous entry is not allowed.");

			_previousEntry = value;
		}
	}

	/// <summary>The next sleep entry in the data set.</summary>
	[JsonIgnore]
	public SleepEntry? NextEntry
	{
		get => _nextEntry;
		set
		{
			if (_nextEntry is not null)
				throw new InvalidOperationException("Overwriting the next entry is not allowed.");

			_nextEntry = value;
		}
	}

	/// <summary>The start time of the sleep entry.</summary>
	[JsonPropertyName("start")]
	public DateTimeOffset Start { get; } = start;

	/// <summary>The end time of the sleep entry.</summary>
	[JsonPropertyName("end")]
	public DateTimeOffset End { get; } = end;

	/// <summary>The quality of the sleep.</summary>
	/// <remarks>Might be missing from some data sets.</remarks>
	[JsonPropertyName("quality")]
	public double? Quality { get; } = quality;

	/// <summary>The mental recovery that resulted from the sleep.</summary>
	/// <remarks>Might be missing from some data sets.</remarks>
	[JsonPropertyName("mental_recovery")]
	public double? MentalRecovery { get; } = mentalRecovery;

	/// <summary>The physical recovery that resulted from the sleep.</summary>
	/// <remarks>Might be missing from some data sets.</remarks>
	[JsonPropertyName("physical_recovery")]
	public double? PhysicalRecovery { get; } = physicalRecovery;

	/// <summary>The amount of sleep cycles that have occurred during the sleep.</summary>
	/// <remarks>Might be missing from some data sets.</remarks>
	[JsonPropertyName("cycles")]
	public double? Cycles { get; } = cycles;

	/// <summary>The stages that have occurred during the sleep.</summary>
	[JsonPropertyName("stages")]
	public IReadOnlyList<SleepStage> Stages { get; } = stages;
	#endregion

	#region Computed properties
	/// <summary>The duration of the sleep.</summary>
	[JsonIgnore]
	public TimeSpan Duration => End.Subtract(Start);

	/// <summary>The amount of time that has passed since the last time sleep has occurred.</summary>
	/// <remarks>Aka the time spent awake after sleeping.</remarks>
	[JsonIgnore]
	public TimeSpan? SinceLastSleep => PreviousEntry is null ? null : Start.Subtract(PreviousEntry.End);

	/// <summary>The amount of time that until the next sleep will occur.</summary>
	/// <remarks>Aka the time spent awake before sleeping.</remarks>
	[JsonIgnore]
	public TimeSpan? UntilNextSleep => NextEntry?.Start.Subtract(End);
	#endregion
}
