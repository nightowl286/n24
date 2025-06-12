namespace N24.Format;

/// <summary>
/// 	Represents a sleep data for a single individual.
/// </summary>
/// <param name="label">The label for the data set.</param>
/// <param name="entries">The sleep entries in the data set.</param>
public sealed class SleepData(string label, IReadOnlyList<SleepEntry> entries)
{
	#region Properties
	/// <summary>The label for the data set.</summary>
	[JsonIgnore]
	public string Label { get; } = label;

	/// <summary>The sleep entries in the data set.</summary>
	[JsonPropertyName("entries")]
	public IReadOnlyList<SleepEntry> Entries { get; } = entries;
	#endregion
}
