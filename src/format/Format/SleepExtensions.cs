namespace N24.Format;

/// <summary>
/// 	Contains various extension methods related to the sleep data formats.
/// </summary>
public static class SleepExtensions
{
	#region Methods
	/// <summary>Groups the sleep entries by the day of the week that they start on.</summary>
	/// <param name="entries">The sleep entries to group.</param>
	/// <returns>The sleep <paramref name="entries"/> grouped by the day of the week that they start on.</returns>
	public static IEnumerable<IGrouping<DayOfWeek, SleepEntry>> ByDayOfWeek(this IEnumerable<SleepEntry> entries) => entries.GroupBy(e => e.Start.DayOfWeek).OrderBy(g => SaneWeekDay(g.Key));

	/// <summary>Groups the sleep entries by the month that they start on.</summary>
	/// <param name="entries">The sleep entries to group.</param>
	/// <returns>The sleep <paramref name="entries"/> grouped by the month that they start on.</returns>
	public static IEnumerable<IGrouping<Month, SleepEntry>> ByMonth(this IEnumerable<SleepEntry> entries) => entries.GroupBy(e => (Month)e.Start.Month).Order();

	/// <summary>Groups the sleep stage entries by their sleep stage kind.</summary>
	/// <param name="stages">The sleep stage entries to group.</param>
	/// <returns>The sleep stage entries grouped by their sleep stage kind.</returns>
	public static IEnumerable<IGrouping<SleepStageKind, SleepStage>> ByKind(this IEnumerable<SleepStage> stages) => stages.GroupBy(s => s.Kind).OrderBy(g => g.Key);

	/// <summary>Selects the sleep durations from the given <paramref name="entries"/>.</summary>
	/// <param name="entries">The entries to get the sleep durations from.</param>
	/// <returns>The sleep durations from the given <paramref name="entries"/>.</returns>
	public static IEnumerable<TimeSpan> SleepDurations(this IEnumerable<SleepEntry> entries) => entries.Select(e => e.Duration);

	/// <summary>Selects the awake durations from the given <paramref name="entries"/>.</summary>
	/// <param name="entries">The entries to get the awake durations from.</param>
	/// <returns>The awake durations from the given <paramref name="entries"/>.</returns>
	public static IEnumerable<TimeSpan> AwakeDurations(this IEnumerable<SleepEntry> entries) => entries.Where(e => e.UntilNextSleep is not null).Select(e => e.UntilNextSleep!.Value);

	/// <summary>Selects the durations from the given sleep <paramref name="stages"/>.</summary>
	/// <param name="stages">The sleep stages to get the durations from.</param>
	/// <returns>The durations from the given sleep <paramref name="stages"/>.</returns>
	public static IEnumerable<TimeSpan> Durations(this IEnumerable<SleepStage> stages) => stages.Select(e => e.Duration);

	/// <summary>Selects the sleep stages for each entry in the given <paramref name="entries"/>.</summary>
	/// <param name="entries">The entries to select the sleep stages from.</param>
	/// <returns>The sleep stages selected for each entry in the given <paramref name="entries"/>.</returns>
	public static IEnumerable<IGrouping<SleepEntry, SleepStage>> StagesByEntry(this IEnumerable<SleepEntry> entries)
	{
		return entries
			.SelectMany(e => e.Stages.Select<SleepStage, (SleepEntry entry, SleepStage stage)>(s => new(e, s)))
			.GroupBy(p => p.entry, p => p.stage);
	}

	/// <summary>Selects the sleep stages from the given <paramref name="entries"/>.</summary>
	/// <param name="entries">The entries to select the sleep stages from.</param>
	/// <returns>The sleep stages from the given <paramref name="entries"/>.</returns>
	public static IEnumerable<SleepStage> Stages(this IEnumerable<SleepEntry> entries) => entries.SelectMany(e => e.Stages);
	#endregion

	#region Helpers
	private static int SaneWeekDay(DayOfWeek day)
	{
		return day switch
		{
			DayOfWeek.Monday => 1,
			DayOfWeek.Tuesday => 2,
			DayOfWeek.Wednesday => 3,
			DayOfWeek.Thursday => 4,
			DayOfWeek.Friday => 5,
			DayOfWeek.Saturday => 6,
			DayOfWeek.Sunday => 7,

			_ => throw new InvalidEnumArgumentException(nameof(day), (int)day, typeof(DayOfWeek))
		};
	}
	#endregion
}
