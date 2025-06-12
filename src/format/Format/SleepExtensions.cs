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
	public static IEnumerable<IGrouping<DayOfWeek, SleepEntry>> ByDayOfWeek(this IEnumerable<SleepEntry> entries) => entries.GroupBy(e => e.Start.DayOfWeek);

	/// <summary>Groups the sleep entries by the month that they start on.</summary>
	/// <param name="entries">The sleep entries to group.</param>
	/// <returns>The sleep <paramref name="entries"/> grouped by the month that they start on.</returns>
	public static IEnumerable<IGrouping<Month, SleepEntry>> ByMonth(this IEnumerable<SleepEntry> entries) => entries.GroupBy(e => (Month)e.Start.Month);

	/// <summary>Groups the sleep stage entries by their sleep stage kind.</summary>
	/// <param name="stages">The sleep stage entries to group.</param>
	/// <returns>The sleep stage entries grouped by their sleep stage kind.</returns>
	public static IEnumerable<IGrouping<SleepStageKind, SleepStage>> ByKind(this IEnumerable<SleepStage> stages) => stages.GroupBy(s => s.Kind);
	#endregion
}
