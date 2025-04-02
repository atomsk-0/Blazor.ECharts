using Blazor.ECharts.Options;
using Blazor.ECharts.Options.Enum;
using Blazor.ECharts.Options.Series;
using Blazor.ECharts.Server.Demo.Data;
using Microsoft.AspNetCore.Components;
using L = Blazor.ECharts.Options.Series.Line;

namespace Blazor.ECharts.Server.Demo.Pages
{
    public partial class Weather
    {
        [Inject] public required IHttpClientFactory ClientFactory { get; set; }
        private Forecast24h[] forecasts = [];
        private EChartsOption<L.Line> Option = null!;
        protected override async Task OnInitializedAsync()
        {
            var client = ClientFactory.CreateClient("WeatherAPI");
            var weather = await client.GetFromJsonAsync<WeatherJSON>("weather/common?source=pc&province=广西&city=梧州&weather_type=forecast_24h");
            forecasts = [.. weather!.data!.forecast_24h!.Values];

            Option = new()
            {
                Title = new Title() { Text = "广西梧州市未来一周气温变化", Subtext = "本页面数据来源腾讯天气API（wis.qq.com）仅作学习演示使用" },
                Tooltip = new Tooltip() { Trigger = TooltipTrigger.Axis },
                Legend = new Legend() { Data = new[] { "最高气温", "最低气温" } },
                Toolbox = new Toolbox()
                {
                    Show = true,
                    Feature = new Feature()
                    {
                        DataZoom = new FeatureDataZoom() { YAxisIndex = "none" },
                        Restore = new Restore(),
                        SaveAsImage = new SaveAsImage(),
                        DataView = new DataView() { ReadOnly = false },
                        MagicType = new MagicType() { Type = [MagicTypeType.Line, MagicTypeType.Bar] }
                    }
                },
                XAxis =
                [
                    new XAxis(){Type=AxisType.Category,BoundaryGap=false,Data=forecasts.Select(f=>f.time).ToArray() }
                ],
                YAxis =
                [
                    new YAxis(){Type=AxisType.Value,AxisLabel=new AxisLabel(){Formatter="{value} °C" } }
                ],
                Series =
                [
                    new L.Line()
                    {
                        Name="最高气温",
                        Data=forecasts.Select(f=>f.max_degree).ToArray(),
                        MarkLine=new MarkLine(){Data=[new MarkLineData() {Name="平均值",Type=Sampling.Average }] },
                        MarkPoint=new MarkPoint()
                        {
                            Data=
                            [
                                new MarkPointData(){Name="最大值",Type=MarkPointDataType.Max },
                                new MarkPointData(){Name="最小值",Type=MarkPointDataType.Min }
                            ]
                        }
                    },
                    new L.Line()
                    {
                        Name="最低气温",
                        Data=forecasts.Select(f=>f.min_degree).ToArray(),
                        MarkPoint=new MarkPoint()
                        {
                            Data=
                            [
                                new MarkPointData(){Name="周最低",Type=MarkPointDataType.Min }
                            ]
                        },
                        MarkLine=new MarkLine()
                        {
                            Data=
                            [
                                new MarkLineData()
                                {
                                    Type=Sampling.Average,
                                    Name="平均值"
                                },
                                new List<MarkLineData>()
                                {
                                    new(){Symbol="none",X="90%",YAxis="max" },
                                    new(){Symbol="circle",Type=Sampling.Max,Name="最高点",Label=new MarkLineDataLabel(){Position=Location.Start,Formatter="最大值" } }
                                }
                            ]
                        }
                    }
                ]
            };
        }
    }
}