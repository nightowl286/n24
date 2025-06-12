using System.IO;

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

	#region Functions
	/// <summary>Loads in the sleep data from the given <paramref name="path"/>.</summary>
	/// <param name="path">The path to the sleep data.</param>
	/// <returns>The loaded in sleep data.</returns>
	/// <exception cref="InvalidDataException">Thrown if the sleep data couldn't be de-serialised.</exception>
	public static SleepData Load(string path)
	{
		using FileStream file = File.OpenRead(path);
		SleepData data = JsonSerializer.Deserialize<SleepData>(file) ?? throw new InvalidDataException("Failed to de-serialised the sleep data.");

		SleepEntry? last = null;

		foreach (SleepEntry entry in data.Entries)
		{
			if (last is not null)
			{
				last.NextEntry = entry;
				entry.PreviousEntry = last;
			}

			last = entry;
		}

		return data;
	}
	#endregion
}
