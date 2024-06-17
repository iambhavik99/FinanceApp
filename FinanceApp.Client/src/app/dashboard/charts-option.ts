import * as echarts from 'echarts/core';
import {
    GridComponent, GridComponentOption, TooltipComponent, TooltipComponentOption,
    VisualMapComponent,
    VisualMapComponentOption
} from 'echarts/components';
import { LineChart, LineSeriesOption } from 'echarts/charts';
import { UniversalTransition } from 'echarts/features';
import { PieChart, PieSeriesOption } from 'echarts/charts';
import { LabelLayout } from 'echarts/features';
import { CanvasRenderer } from 'echarts/renderers';

echarts.use([GridComponent, LineChart, CanvasRenderer, UniversalTransition, TooltipComponent]);

type EChartsOption = echarts.ComposeOption<
    GridComponentOption | LineSeriesOption
>;

interface DataItem {
    name: string;
    value: [string, number];
}


export function setLineChart(id: string, xAxisData: any[], yAxisData: any[]) {

    const eChartForLineChart = echarts;
    eChartForLineChart.use([GridComponent, LineChart, CanvasRenderer, UniversalTransition, TooltipComponent]);

    let data: DataItem[] = [];
    for (var i = 0; i < xAxisData.length; i++) {
        data.push({
            name: xAxisData[i],
            value: [xAxisData[i], yAxisData[i]]
        });
    }

    var chartDom = document.getElementById(id)!;
    var myChart = eChartForLineChart.init(chartDom);
    var option: EChartsOption;

    option = {
        color: ["#0d9488"],
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow',
                label: {
                    backgroundColor: '#6a7985'
                }
            }
        },
        grid: {
            left: '4%',
            right: '4%',
            bottom: '0%',
            top: '10%',
            containLabel: true
        },
        xAxis: [
            {
                type: 'time',
                show: false,
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: { show: false }
            }
        ],
        series: [
            {
                type: 'line',
                smooth: true,
                showSymbol: false,
                areaStyle: {
                    opacity: 0.06
                },
                data: data,
            }
        ]
    };

    option && myChart.setOption(option);
}


export function setPieChart(id: string, data: any) {
    const eChartForPieChart = echarts;
    eChartForPieChart.use([TooltipComponent, VisualMapComponent, PieChart, CanvasRenderer, LabelLayout]);

    type EChartsOption = echarts.ComposeOption<
        TooltipComponentOption | VisualMapComponentOption | PieSeriesOption
    >;

    var chartDom = document.getElementById(id)!;
    var myChart = eChartForPieChart.init(chartDom);
    var option: EChartsOption;

    option = {
        tooltip: {
            trigger: 'item'
        },
        series: [
            {
                type: 'pie',
                data: data.sort(function (a: any, b: any) {
                    return a.value - b.value;
                }),
                labelLine: {
                    smooth: 0.2,
                    length: 10,
                    length2: 20
                },
                color: ['#5eead4', '#2dd4bf', '#14b8a6', '#0d9488', '#0f766e'],
                animationType: 'scale',
                animationEasing: 'elasticOut',
                animationDelay: function (idx: number) {
                    return Math.random() * 200;
                }
            }
        ]
    };

    option && myChart.setOption(option);

}

