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
