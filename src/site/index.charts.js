import { getChartData, getTimeText } from "./utils.js";

export let charts = {};

/* sleep time charts */
{
	const regularData = await getChartData("sleep-times/regular");
	const dspdData = await getChartData("sleep-times/dspd");
	const aspdData = await getChartData("sleep-times/aspd");
	const n24Data = await getChartData("sleep-times/n24");

	const dayLabels = [
		["Monday", "Tuesday"],
		["Tuesday", "Wednesday"],
		["Wednesday", "Thursday"],
		["Thursday", "Friday"],
		["Friday", "Saturday"],
		["Saturday", "Sunday"],
		["Sunday", "Monday"]
	];

	const tooltip = {
		callbacks: {
			title: items => { const parts = items[0].label.split(','); return `${parts[0]} evening to ${parts[1]} morning` },
			label: item => `Asleep from ${getTimeText(item.raw[0])} to ${getTimeText(item.raw[1])}.`,
			footer: item => `Asleep for ${item[0].raw[1] - item[0].raw[0]} hours`,
		}
	};
	const legend = { position: "top" };
	const title = { display: true, text: "Average sleep times for each day of the week" };

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

	charts["regular"] = {
		type: "bar",
		data: { datasets: [regularData] },
		options: {
			indexAxis: 'y',
			responsive: true,
			aspectRatio: 1.5,
			plugins: {
				legend: legend,
				title: title,
				tooltip: tooltip
			},
			scales: scales
		}
	};

	charts["dspd"] = {
		type: "bar",
		data: { datasets: [regularData, dspdData] },
		options: {
			indexAxis: 'y',
			responsive: true,
			aspectRatio: 1.25,
			plugins: {
				legend: legend,
				title: title,
				tooltip: tooltip
			},
			scales: scales
		}
	};

	charts["aspd"] = {
		type: "bar",
		data: { datasets: [regularData, dspdData, aspdData] },
		options: {
			indexAxis: 'y',
			responsive: true,
			aspectRatio: 1,
			plugins: {
				legend: legend,
				title: title,
				tooltip: tooltip
			},
			scales: scales
		}
	};

	charts["n24"] = {
		type: "bar",
		data: { datasets: [regularData, dspdData, aspdData, n24Data] },
		options: {
			indexAxis: 'y',
			responsive: true,
			aspectRatio: 0.75,
			plugins: {
				legend: legend,
				title: { display: true, text: "Example sleep times during a single week" },
				tooltip: tooltip
			},
			scales: scales
		}
	};
}

charts["day-length"] =
{
	type: "bar",
	data: { datasets: await getChartData("day-length") },
	options: {
		responsive: true,
		aspectRatio: 1.5,
		plugins: {
			legend: {},
			title: { display: true, text: "Day length comparison" },
			tooltip: {
				callbacks: {
					title: items => { console.log(items); const item = items[0]; return `${item.label} ${item.dataset.label.toLowerCase()}` },
					label: item => `${item.raw} hours`
				}
			}
		},
		scales: {
			x: {
				ticks: {
					autoSkip: false
				},
				labels: ["'Regular'", "DSPD", "ASPD", "N24"],
				stacked: true
			},
			y: {
				title: { display: true, text: "Hours" },
				stacked: true,
				ticks: {
					stepSize: 4,
				}
			}
		}
	}
}