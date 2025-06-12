namespace Converter.Samsung;

public readonly struct SleepStageEntry
{
	#region Properties
	public readonly DateTime Start { get; init; }
	public readonly Guid SleepId { get; init; }
	public readonly int Stage { get; init; }
	public readonly TimeSpan Offset { get; init; }
	public readonly DateTime End { get; init; }
	#endregion

	#region Functions
	public static IEnumerable<SleepStageEntry> ParseFromDirectory(string directory)
	{
		string[] files = Directory.GetFiles(directory, "com.samsung.health.sleep_stage.*.csv");

		if (files.Length is not 1)
			throw new InvalidOperationException($"Expected exactly one file matching the 'com.samsung.shealth.sleep_stage.*.csv' pattern.");

		return Parse(files[0]);
	}
	public static IEnumerable<SleepStageEntry> Parse(string path)
	{
		CsvFile file = CsvFile.Load(path, 1);

		for (int row = 0; row < file.RowCount; row++)
		{
			SleepStageEntry entry = new()
			{
				Start = DateTime.Parse(file["start_time", row]),
				SleepId = Guid.Parse(file["sleep_id", row]),
				Stage = int.Parse(file["stage", row]),
				Offset = file["time_offset", row].ParseOffset(),
				End = DateTime.Parse(file["end_time", row]),
			};

			yield return entry;
		}
	}
	#endregion
}
