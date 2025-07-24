import { getCharts } from '../site/index.charts.js';
import { Canvas } from 'skia-canvas';
import fsp from "node:fs/promises";
import {
	Chart,
	BarController, BubbleController, DoughnutController, LineController, PieController, PolarAreaController, RadarController, ScatterController,
	CategoryScale, LinearScale, LogarithmicScale, RadialLinearScale, TimeScale, TimeSeriesScale,
	ArcElement, BarElement, LineElement, PointElement,
	Legend, Title
} from 'chart.js';

Chart.defaults.color = "#ffffff";
Chart.defaults.borderColor = "#808080";

Chart.register([
	BarController, BubbleController, DoughnutController, LineController, PieController, PolarAreaController, RadarController, ScatterController,
	CategoryScale, LinearScale, LogarithmicScale, RadialLinearScale, TimeScale, TimeSeriesScale,
	ArcElement, BarElement, LineElement, PointElement,
	Legend, Title
]);

(async () => {
	fsp.mkdir("charts", { recursive: true });

	const charts = await getCharts(getChartData);

	for (const key in charts) {
		if (Object.prototype.hasOwnProperty.call(charts, key)) {
			const config = charts[key];

			const width = 600;
			const height = width / (config?.options?.aspectRatio ?? 2);

			const canvas = new Canvas(width, height);
			const chart = new Chart(canvas, config);

			const pngBuffer = await canvas.toBuffer("png");
			const path = `charts/${key}.png`;

			console.log(`writing ${pngBuffer.byteLength} bytes to ${path}`);
			await fsp.writeFile(path, pngBuffer);

			chart.destroy();
		}
	}
})();

async function getChartData(id) {
	const buffer = await fsp.readFile(`../site/data/${id}.json`);

	const json = buffer.toString();
	const value = JSON.parse(json);

	return value;
}