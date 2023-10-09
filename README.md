# CalendarSimpleControl
1. 参考项目 https://github.com/SeaSharpGit/DateWork/blob/master/DateWork/Models/DayType.cs
2. 参考效果 https://element.eleme.cn/#/zh-CN/component/calendar
3. 本项目重在提供日历控件中日期如何生成的思路
    - 日历控件为7(列)*7(行)的网格
    - 找到当前月的第一天是周几是重点
    - 接下来就是如何将填充满这个7*7的网格了