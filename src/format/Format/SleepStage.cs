namespace N24.Format;

/// <summary>
/// 	Represents information about a stage of sleep.
/// </summary>
/// <param name="kind">The kind of the sleep stage.</param>
/// <param name="start">The start time of the sleep stage.</param>
/// <param name="end">The end time of the sleep stage.</param>
/// <summary>
/// 	Represents information about a stage of sleep.
/// </summary>
[method: JsonConstructor]
[DebuggerDisplay($"{{{nameof(DebuggerDisplay)}(), nq}}")]
public readonly struct SleepStage(SleepStageKind kind, DateTimeOffset start, DateTimeOffset end)
{
	#region Properties
	/// <summary>The kind of the sleep stage.</summary>
	[JsonPropertyName("kind")]
	public readonly SleepStageKind Kind { get; } = kind;

	/// <summary>The start time of the sleep stage.</summary>
	[JsonPropertyName("start")]
	public readonly DateTimeOffset Start { get; } = start;

	/// <summary>The end time of the sleep stage.</summary>
	[JsonPropertyName("end")]
	public readonly DateTimeOffset End { get; } = end;

	/// <summary>The duration of the sleep stage.</summary>
	[JsonIgnore]
	public readonly TimeSpan Duration => End.Subtract(Start);
	#endregion

	#region Methods
	private readonly string DebuggerDisplay() => $"{{ Kind = ({Kind}), Duration => ({Duration}) }}";
	#endregion
}
