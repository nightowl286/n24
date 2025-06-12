namespace Converter.Samsung;

public readonly struct SleepEntry
{
	#region Properties
	public readonly double MentalRecovery { get; init; }
	public readonly double PhysicalRecovery { get; init; }
	public readonly double Cycles { get; init; }
	public readonly double Score { get; init; }
	public readonly DateTime Start { get; init; }
	public readonly TimeSpan Offset { get; init; }
	public readonly DateTime End { get; init; }
	public readonly Guid Id { get; init; }
	#endregion

	#region Functions
	public static IEnumerable<SleepEntry> ParseFromDirectory(string directory)
	{
		string[] files = Directory.GetFiles(directory, "com.samsung.shealth.sleep.*.csv");

		if (files.Length is not 1)
			throw new InvalidOperationException($"Expected exactly one file matching the 'com.samsung.shealth.sleep.*.csv' pattern.");

		return Parse(files[0]);
	}
	public static IEnumerable<SleepEntry> Parse(string path)
	{
		CsvFile file = CsvFile.Load(path, 1);

		for (int row = 0; row < file.RowCount; row++)
		{
			SleepEntry entry = new()
			{
				MentalRecovery = double.Parse(file["mental_recovery", row]),
				PhysicalRecovery = double.Parse(file["physical_recovery", row]),
				Cycles = double.Parse(file["sleep_cycle", row]),
				Score = double.Parse(file["sleep_score", row]),
				Start = DateTime.Parse(file["com.samsung.health.sleep.start_time", row]),
				Offset = file["com.samsung.health.sleep.time_offset", row].ParseOffset(),
				End = DateTime.Parse(file["com.samsung.health.sleep.end_time", row]),
				Id = Guid.Parse(file["com.samsung.health.sleep.datauuid", row])
			};

			yield return entry;
		}
	}
	#endregion
}
