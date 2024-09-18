import * as echarts from 'echarts/core';
import {
    GridComponent, GridComponentOption, TooltipComponent, TooltipComponentOption,
    VisualMapComponent,
    VisualMapComponentOption,
} from 'echarts/components';
import { UniversalTransition } from 'echarts/features';
import { PieChart, PieSeriesOption } from 'echarts/charts';
import { LabelLayout } from 'echarts/features';
import { CanvasRenderer } from 'echarts/renderers';
import { BarChart, BarSeriesOption } from 'echarts/charts';

echarts.use([GridComponent, BarChart, CanvasRenderer, UniversalTransition, TooltipComponent]);

type EChartsOption = echarts.ComposeOption<
    GridComponentOption | BarSeriesOption
>;

interface DataItem {
    name: string;
    value: [string, number];
}


export function setLineChart(id: string, xAxisData: any[], yAxisData: any[]) {

    const eChartForLineChart = echarts;
    eChartForLineChart.use([GridComponent, BarChart, CanvasRenderer, UniversalTransition, TooltipComponent]);

    var chartDom = document.getElementById(id)!;
    var myChart = eChartForLineChart.init(chartDom);
    var option: EChartsOption;

    option = {
        color: ["#0d9488", "#dc2626"],
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow',
            }
        },
        grid: {
            left: '4%',
            right: '4%',
            bottom: '0%',
            top: '10%',
            containLabel: true
        },
        xAxis: {
            type: 'category',
            data: xAxisData
        },
        yAxis: [
            {
                type: 'value',
                axisLabel: { show: false }
            }
        ],
        series: [
            {
                data: yAxisData.map(x => x.income),
                type: 'bar'
            },
            {
                data: yAxisData.map(x => x.expanse),
                type: 'bar'
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
                color: ['#2dd4bf', '#14b8a6', '#0d9488', '#0f766e'],
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

