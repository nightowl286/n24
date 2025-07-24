export async function getChartData(id) {
	try {
		const response = await fetch(`data/${id}.json`);
		if (!response.ok) {
			throw new Error(`Failed to obtain chart data for (${id}) (result: ${response.status}).`)
		}

		const json = await response.json();
		return json;
	}
	catch (error) {
		console.error(error);
	}
}

export function getTimeText(hour) {
	if (hour % 1 !== 0) return null;
	if (hour < 0) return null;

	hour %= 24;

	if (hour == 12) return "12pm";
	if (hour == 0) return "12am";
	if (hour < 12) return `${hour}am`;
	return `${hour - 12}pm`;
}