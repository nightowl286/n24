namespace N24.Analysis;

partial class GeneralExtensions
{
	#region Methods
	/// <summary>Calculates the <paramref name="sum"/> of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to sum.</typeparam>
	/// <param name="values">The values to sum.</param>
	/// <param name="sum">The calculated sum.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<T> Sum<T>(this IEnumerable<T> values, out T sum)
		where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
	{
		sum = T.AdditiveIdentity;
		List<T> collected = [];

		foreach (T value in values)
		{
			sum = checked(sum + value);
			collected.Add(value);
		}

		return collected;
	}

	/// <summary>Calculates the sum of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to sum.</typeparam>
	/// <param name="values">The values to sum.</param>
	/// <returns>The calculated sum.</returns>
	public static T Sum<T>(this IEnumerable<T> values)
		where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
	{
		T sum = T.AdditiveIdentity;

		foreach (T value in values)
			sum = checked(sum + value);

		return sum;
	}

	/// <summary>Calculates the <paramref name="sum"/> of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to <paramref name="sum"/>.</typeparam>
	/// <typeparam name="TNumber">The type to convert the <paramref name="values"/> to for performing the mathematical operations.</typeparam>
	/// <param name="values">The values to <paramref name="sum"/>.</param>
	/// <param name="sum">The calculated sum.</param>
	/// <param name="convertDelegate">A delegate used to convert the values of the type <typeparamref name="T"/> to the numeric type <typeparamref name="TNumber"/>.</param>
	/// <param name="convertBackDelegate">A delegate used to convert back from the numeric type <typeparamref name="TNumber"/> to the regular type <typeparamref name="T"/>.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<T> Sum<T, TNumber>(this IEnumerable<T> values, out T sum, Func<T, TNumber> convertDelegate, Func<TNumber, T> convertBackDelegate)
		where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>, IAdditiveIdentity<TNumber, TNumber>
	{
		TNumber sum2 = TNumber.AdditiveIdentity;
		List<T> collected = [];

		foreach (T value in values)
		{
			sum2 = checked(sum2 + convertDelegate.Invoke(value));
			collected.Add(value);
		}

		sum = convertBackDelegate.Invoke(sum2);

		return collected;
	}

	/// <summary>Calculates the sum of the given <paramref name="values"/>.</summary>
	/// <typeparam name="T">The type of the <paramref name="values"/> to sum.</typeparam>
	/// <typeparam name="TNumber">The type to convert the <paramref name="values"/> to for performing the mathematical operations.</typeparam>
	/// <param name="values">The values to sum.</param>
	/// <param name="convertDelegate">A delegate used to convert the values of the type <typeparamref name="T"/> to the numeric type <typeparamref name="TNumber"/>.</param>
	/// <param name="convertBackDelegate">A delegate used to convert back from the numeric type <typeparamref name="TNumber"/> to the regular type <typeparamref name="T"/>.</param>
	/// <returns>The calculated sum.</returns>
	public static T Sum<T, TNumber>(this IEnumerable<T> values, Func<T, TNumber> convertDelegate, Func<TNumber, T> convertBackDelegate)
		where TNumber : IAdditionOperators<TNumber, TNumber, TNumber>, IAdditiveIdentity<TNumber, TNumber>
	{
		TNumber sum = TNumber.AdditiveIdentity;
		List<T> collected = [];

		foreach (T value in values)
		{
			sum = checked(sum + convertDelegate.Invoke(value));
			collected.Add(value);
		}

		return convertBackDelegate.Invoke(sum);
	}
	#endregion
}

partial class TimeSpanExtensions
{
	#region TimeSpan methods
	/// <summary>Calculates the sum of the given <paramref name="values"/>.</summary>
	/// <param name="values">The values to sum.</param>
	/// <returns>The calculated sum.</returns>
	public static TimeSpan Sum(this IEnumerable<TimeSpan> values) => values.Sum(FromTimeSpan, ToTimeSpan);

	/// <summary>Calculates the <paramref name="sum"/> of the given <paramref name="values"/>.</summary>
	/// <param name="values">The values to <paramref name="sum"/>.</param>
	/// <param name="sum">The calculated sum.</param>
	/// <returns>The given <paramref name="values"/>.</returns>
	public static IEnumerable<TimeSpan> Sum(this IEnumerable<TimeSpan> values, out TimeSpan sum) => values.Sum(out sum, FromTimeSpan, ToTimeSpan);
	#endregion
}
