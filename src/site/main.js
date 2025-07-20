Chart.defaults.color = "#ffffff";
Chart.defaults.borderColor = "#808080";

async function getChartData(id) {
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


function getTimeText(hour) {
	if (hour % 1 !== 0) return null;
	if (hour < 0) return null;

	hour %= 24;

	if (hour == 12) return "12pm";
	if (hour == 0) return "12am";
	if (hour < 12) return `${hour}am`;
	return `${hour - 12}pm`;
}

async function sleepTimeCharts() {
	let regularData = await getChartData("sleep-times/regular");
	let dspdData = await getChartData("sleep-times/dspd");
	let aspdData = await getChartData("sleep-times/aspd");
	let n24Data = await getChartData("sleep-times/n24");

	const regularCanvas = document.getElementById("regular-chart");
	const dspdCanvas = document.getElementById("dspd-chart");
	const aspdCanvas = document.getElementById("aspd-chart");
	const n24Canvas = document.getElementById("n24-chart");

	const dayLabels = [["Monday", "Tuesday"], ["Tuesday", "Wednesday"], ["Wednesday", "Thursday"], ["Thursday", "Friday"], ["Friday", "Saturday"], ["Saturday", "Sunday"], ["Sunday", "Monday"]];

	const tooltip = {
		callbacks: {
			title: items => { const parts = items[0].label.split(','); return `${parts[0]} evening to ${parts[1]} morning` },
			label: item => `Asleep from ${getTimeText(item.raw[0])} to ${getTimeText(item.raw[1])}.`,
			footer: item => `Asleep for ${item[0].raw[1] - item[0].raw[0]} hours`,
		}
	};
	const legend = { position: "top" };
	const dayAxis = {
		title: { display: true, text: "Day of the week", },
		ticks: {
			autoSkip: false,
			callback: (_1, index, _2) => dayLabels[index][0]
		},
		labels: dayLabels
	};
	const timeAxis = {
		beginAtZero: false,
		title: { display: true, text: "Time of the day" },
		ticks: { callback: (value, _1, _2) => getTimeText(value) },
	}
	const scales = { y: dayAxis, x: timeAxis };

	new Chart(regularCanvas, {
		type: "bar",
		data: { datasets: [regularData] },
		options: {
			indexAxis: 'y',
			responsive: true,
			plugins: {
				legend: legend,
				title: { display: true, text: "Average sleep times for each day of the week" },
				tooltip: tooltip
			},
			scales: scales
		}
	});

	new Chart(dspdCanvas, {
		type: "bar",
		data: { datasets: [regularData, dspdData] },
		options: {
			indexAxis: 'y',
			responsive: true,
			aspectRatio: 1.5,
			plugins: {
				legend: legend,
				title: { display: true, text: "Average sleep times for each day of the week" },
				tooltip: tooltip
			},
			scales: scales
		}
	});

	new Chart(aspdCanvas, {
		type: "bar",
		data: { datasets: [regularData, dspdData, aspdData] },
		options: {
			indexAxis: 'y',
			responsive: true,
			aspectRatio: 1,
			plugins: {
				legend: legend,
				title: { display: true, text: "Average sleep times for each day of the week" },
				tooltip: tooltip
			},
			scales: scales
		}
	});

	new Chart(n24Canvas, {
		type: "bar",
		data: { datasets: [regularData, dspdData, aspdData, n24Data] },
		options: {
			indexAxis: 'y',
			responsive: true,
			aspectRatio: 1,
			plugins: {
				legend: legend,
				title: { display: true, text: "Example sleep times during a single week" },
				tooltip: tooltip
			},
			scales: scales
		}
	});
}

await Promise.all([
	sleepTimeCharts()
]);