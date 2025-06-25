Chart.defaults.color = "#ffffff";
Chart.defaults.borderColor = "#a0a0a0";

async function getChartData(id) {
	try {
		const response = await fetch(`/data/${id}.json`);
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

	const regularCanvas = document.getElementById("regular-chart");
	const dspdCanvas = document.getElementById("dspd-chart");
	const aspdCanvas = document.getElementById("aspd-chart");

	console.log(regularData);

	const tooltip = {
		callbacks: {
			label: item => `Asleep from ${getTimeText(item.raw[0])} to ${getTimeText(item.raw[1])}.`,
			footer: item => `Asleep for ${item[0].raw[1] - item[0].raw[0]} hours`,
		}
	};
	const legend = { position: "top" };
	const xAxis = {
		title: { display: true, text: "Day of the week", },
		labels: [["Monday", "Tuesday"], ["Tuesday", "Wednesday"], ["Wednesday", "Thursday"], ["Thursday", "Friday"], ["Thursday", "Saturday"], ["Saturday", "Sunday"], ["Sunday", "Monday"]]
	};
	const yAxis = {
		beginAtZero: false,
		title: { display: true, text: "Time of the day" },
		ticks: { callback: (value, _1, _2) => getTimeText(value) },
	}
	const scales = { x: xAxis, y: yAxis };

	new Chart(regularCanvas, {
		type: "bar",
		data: { datasets: [regularData] },
		options: {
			responsive: true,
			plugins: {
				legend: legend,
				title: { display: true, text: "'Regular' sleep times" },
				tooltip: tooltip
			},
			scales: scales
		}
	});

	new Chart(dspdCanvas, {
		type: "bar",
		data: { datasets: [regularData, dspdData] },
		options: {
			responsive: true,
			plugins: {
				legend: legend,
				title: { display: true, text: "'Regular' / DSPD sleep times" },
				tooltip: tooltip
			},
			scales: scales
		}
	});

	new Chart(aspdCanvas, {
		type: "bar",
		data: { datasets: [regularData, dspdData, aspdData] },
		options: {
			responsive: true,
			plugins: {
				legend: legend,
				title: { display: true, text: "'Regular' / DSPD / ASPD sleep times" },
				tooltip: tooltip
			},
			scales: scales
		}
	});
}

document.addEventListener("DOMContentLoaded", async (event) => {

	await Promise.all([
		sleepTimeCharts()
	]);

})
