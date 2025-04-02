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
            var weather = await client.GetFromJsonAsync<WeatherJSON>("weather/common?source=pc&province=����&city=����&weather_type=forecast_24h");
            forecasts = [.. weather!.data!.forecast_24h!.Values];

            Option = new()
            {
                Title = new Title() { Text = "����������δ��һ�����±仯", Subtext = "��ҳ��������Դ��Ѷ����API��wis.qq.com������ѧϰ��ʾʹ��" },
                Tooltip = new Tooltip() { Trigger = TooltipTrigger.Axis },
                Legend = new Legend() { Data = new[] { "�������", "�������" } },
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
                    new YAxis(){Type=AxisType.Value,AxisLabel=new AxisLabel(){Formatter="{value} ��C" } }
                ],
                Series =
                [
                    new L.Line()
                    {
                        Name="�������",
                        Data=forecasts.Select(f=>f.max_degree).ToArray(),
                        MarkLine=new MarkLine(){Data=[new MarkLineData() {Name="ƽ��ֵ",Type=Sampling.Average }] },
                        MarkPoint=new MarkPoint()
                        {
                            Data=
                            [
                                new MarkPointData(){Name="���ֵ",Type=MarkPointDataType.Max },
                                new MarkPointData(){Name="��Сֵ",Type=MarkPointDataType.Min }
                            ]
                        }
                    },
                    new L.Line()
                    {
                        Name="�������",
                        Data=forecasts.Select(f=>f.min_degree).ToArray(),
                        MarkPoint=new MarkPoint()
                        {
                            Data=
                            [
                                new MarkPointData(){Name="�����",Type=MarkPointDataType.Min }
                            ]
                        },
                        MarkLine=new MarkLine()
                        {
                            Data=
                            [
                                new MarkLineData()
                                {
                                    Type=Sampling.Average,
                                    Name="ƽ��ֵ"
                                },
                                new List<MarkLineData>()
                                {
                                    new(){Symbol="none",X="90%",YAxis="max" },
                                    new(){Symbol="circle",Type=Sampling.Max,Name="��ߵ�",Label=new MarkLineDataLabel(){Position=Location.Start,Formatter="���ֵ" } }
                                }
                            ]
                        }
                    }
                ]
            };
        }
    }
}