namespace N24.Format;

/// <summary>
///	Represents the different sleep stages.
/// </summary>
public enum SleepStageKind : byte
{
	/// <summary>The 1st and lightest stage of sleep.</summary>
	N1 = 1,

	/// <summary>The 2nd stage of sleep.</summary>
	N2 = 2,

	/// <summary>The 3rd and deepest stage of sleep.</summary>
	N3 = 3,

	/// <summary>The 4th stage of sleep (the rapid eye movement stage).</summary>
	REM = 4,
}

/// <summary>
/// 	Contains various extension methods related to the <see cref="SleepStageKind"/> <see langword="enum"/>.
/// </summary>
public static class SleepStageKindExtensions
{
	#region Methods
	/// <summary>Gets the colloquial name for the given sleep stage <paramref name="kind"/>.</summary>
	/// <param name="kind">The kind of the sleep stage.</param>
	/// <returns>The colloquial name for the given sleep stage <paramref name="kind"/>.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if the given <paramref name="kind"/> is unknown.</exception>
	public static string GetColloquialName(this SleepStageKind kind)
	{
		return kind switch
		{
			SleepStageKind.N1 => "Awake",
			SleepStageKind.N2 => "Light",
			SleepStageKind.N3 => "Deep",
			SleepStageKind.REM => "REM",

			_ => throw new ArgumentOutOfRangeException(nameof(kind), kind, $"The sleep kind ({kind}) was unknown.")
		};
	}
	#endregion
}