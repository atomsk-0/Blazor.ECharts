# Blazor.ECharts

## 介绍

**Blazor版本的ECharts图表组件**

### 重新出发

**本项目源自[https://github.com/caopengfei/BlazorECharts](https://github.com/caopengfei/BlazorECharts)，由于原作者有好长一段时间没有更新和处理PR，故在此基础上，重新做了这个**

[![GitHub license](https://img.shields.io/github/license/lishewen/Blazor.ECharts.svg)](https://github.com/lishewen/Blazor.ECharts/blob/master/LICENSE)

开源地址：[https://github.com/lishewen/Blazor.ECharts](https://github.com/lishewen/Blazor.ECharts)

国内镜像：[https://gitee.com/lishewen/Blazor.ECharts](https://gitee.com/lishewen/Blazor.ECharts)

ECharts配置请参考：

[https://echarts.apache.org/examples/zh/index.html](https://echarts.apache.org/examples/zh/index.html)

## 使用方式
1. 创建Blazor项目。
2. 在NuGet中安装包`Blazor.ECharts` [![NuGet](https://img.shields.io/nuget/v/Blazor.ECharts.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Blazor.ECharts/) ![downloads](https://img.shields.io/nuget/dt/Blazor.ECharts.svg)。
3. 在`_Imports.razor`中添加`@using Blazor.ECharts.Components`。
4. 在`wwwroot/index.html`文件的`Head`中引入：
```html
<script src="https://lib.baomitu.com/echarts/5.6.0/echarts.min.js"></script>
```
**需要使用地图相关功能的则需要额外添加地图js的引用**
```html
<script type="text/javascript" src="https://api.map.baidu.com/api?v=2.0&ak=[Your Key Here]"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/echarts@5/dist/extension/bmap.min.js"></script>
```
5. 在`wwwroot/index.html`文件的`Body`中引入：
```html
<script type="module" src="_content/Blazor.ECharts/core.js"></script>
```
6. 修改`Program.cs`增加
```csharp
builder.Services.AddECharts();
```
7. 在页面中使用组件（可参考Demo项目）。

**注意：因为没有设置默认的样式，所以需要在组件上设置`Class`或者`Style`来控制宽度和高度**

**Demo中也提供示范样式**
```css
.chart-container {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: flex-start;
    padding-left: 20px;
    padding-bottom: 20px;
    padding-right: 0px;
    padding-top: 0px;
    height: 95%;
    width: 95%;
}

.chart-normal {
    border-radius: 4px;
    height: 300px;
    width: 400px;
    margin-top: 20px;
}

.chart-fill {
    width: 100%;
    height: 720px;
    margin-top: 20px;
    margin-right: 20px;
}
```

## JS function的输出问题
由于function不是json的标准数据类型，所以json数据中若含function，则转换后，function会丢失。此库为解决这个问题通过`JFuncConverter`来实现转译输出。使用时传入一个`JFunc`对象即可。例如：
```csharp
Position = new JFunc()
{
    RAW = """
    function (pt) {
        return [pt[0], '10%'];
    }
    """
}
```

## 功能实现进度
- [ ] **公共配置**
  - [x] title
  - [x] legend
  - [x] grid（部分）
  - [x] xAxis（部分）
  - [x] yAxis（部分）
  - [x] polar（部分）
  - [x] radiusAxis（部分）
  - [x] angleAxis（部分）
  - [x] radar（部分）
  - [x] dataZoom
  - [x] visualMap（部分）
  - [x] tooltip（部分）
  - [x] axisPointer（部分）
  - [x] toolbox（部分）
  - [ ] brush
  - [ ] geo
  - [ ] parallel
  - [ ] parallelAxis
  - [ ] singleAxis
  - [x] timeline
  - [x] graphic
  - [ ] calendar
  - [ ] dataset
  - [ ] aria
  - [x] series（部分）
  - [x] color
  - [x] backgroundColor
  - [x] textStyle
  - [x] animation
  - [x] animationThreshold
  - [x] animationDuration
  - [x] animationEasing
  - [x] animationDelay
  - [x] animationDurationUpdate
  - [x] animationEasingUpdate
  - [x] animationDelayUpdate
  - [x] blendMode
  - [x] hoverLayerThreshold
  - [x] useUTC
- [ ] **图表**
  - [x] 折线图（部分）
  - [x] 柱状图（部分）
  - [x] 饼图（部分）
  - [x] 散点图（部分）
  - [x] 地理坐标/地图（部分）
  - [x] K线图（部分）
  - [x] 雷达图（部分）
  - [ ] 盒须图
  - [ ] 热力图
  - [x] 关系图（部分）
  - [x] 路径图（部分）
  - [x] 树图（部分）
  - [x] 矩形树图（部分）
  - [x] 旭日图（部分）
  - [ ] 平行坐标系
  - [x] 桑基图（部分）
  - [x] 漏斗图（部分）
  - [x] 仪表盘（部分）
  - [ ] 象形柱图
  - [ ] 主题河流图
  - [ ] 日历坐标系
  - [x] 词云图(使用方法：[Blazor.ECharts.WordCloud/README.md](Blazor.ECharts.WordCloud/README.md))

### Nuget Package中没有打包echarts.js的原因
1. 减少包的体积
2. 方便自由更换cdn
3. 方便echarts小版本更新时，作者没有来得及更新Package内的js时，可自行在页面上更换