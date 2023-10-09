using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCalendarControlDemo
{
    /// <summary>
    /// CalendarControl.xaml 的交互逻辑
    /// </summary>
    public partial class CalendarControl : UserControl
    {
        /// <summary>
        /// 设置MonthValue来显示当前月份 如果MonthValue未指定则显示当月
        /// </summary>
        public DateTime MonthValue
        {
            get { return (DateTime)GetValue(MonthValueProperty); }
            set { SetValue(MonthValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Today.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthValueProperty =
            DependencyProperty.Register("MonthValue", typeof(DateTime), typeof(CalendarControl), new FrameworkPropertyMetadata(DateTime.Today, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnMonthValueChanged)));

        private static void OnMonthValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var calendarControl = d as CalendarControl;
            if (calendarControl != null)
            {
                calendarControl.GenerateLayout();
            }
        }

        public void NextMonth()
        {
            MonthValue = MonthValue.AddMonths(1);
        }

        public void PrevMonth()
        {
            MonthValue = MonthValue.AddMonths(-1);
        }

        public void CurrMonth()
        {
            MonthValue = MonthValue = DateTime.Today;
        }

        public bool IsSundayFirstDayOfWeek
        {
            get { return (bool)GetValue(IsSundayFirstDayOfWeekProperty); }
            set { SetValue(IsSundayFirstDayOfWeekProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSundayFirstDayOfWeek.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSundayFirstDayOfWeekProperty =
            DependencyProperty.Register("IsSundayFirstDayOfWeek", typeof(bool), typeof(CalendarControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsSundayFirstDayOfWeekChanged)));

        private static void OnIsSundayFirstDayOfWeekChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var calendarControl = d as CalendarControl;
            if (calendarControl != null)
            {
                calendarControl.GenerateLayout();
            }
        }

        public CalendarControl()
        {
            InitializeComponent();
            this.Loaded -= CalendarControl_Loaded;
            this.Loaded += CalendarControl_Loaded;
        }

        private bool isLoaded = false;

        private void CalendarControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                return;
            }
            isLoaded = true;
            GenerateLayout();
        }

        private void GenerateLayout()
        {
            mainContainer.Children.Clear();
            var items = GenerateDayItem();
            foreach (var item in items)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = item;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                mainContainer.Children.Add(textBlock);
            }
        }


        private List<string> GenerateDayItem()
        {
            var listStr = new List<string>() { "周一", "周二", "周三", "周四", "周五", "周六", "周日" };
            if (IsSundayFirstDayOfWeek)
            {
                listStr = new List<string>() { "周日", "周二", "周三", "周四", "周五", "周六", "周一" };
            }
            //这个月的第一天是周几
            var firstDayOfCurrentMonth = new DateTime(MonthValue.Year, MonthValue.Month, 1);
            var fistDayOfWeek = firstDayOfCurrentMonth.DayOfWeek;
            var remainCount = (7 * 7) - 7;
            if (IsSundayFirstDayOfWeek)
            {
                switch (fistDayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        //前面不用补
                        remainCount -= 0;
                        break;
                    case DayOfWeek.Monday:
                        //前面补一个
                        remainCount -= 1;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Tuesday:
                        //前面补两个
                        remainCount -= 2;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Wednesday:
                        //前面补三个
                        remainCount -= 3;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Thursday:
                        //前面补四个
                        remainCount -= 4;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-4).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Friday:
                        //前面补五个
                        remainCount -= 5;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-5).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-4).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Saturday:
                        //前面补六个
                        remainCount -= 6;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-6).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-5).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-4).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    default:
                        throw new ArgumentException($"{nameof(fistDayOfWeek)} is invalid");
                }
            }
            else
            {
                switch (fistDayOfWeek)
                {
                    case DayOfWeek.Monday:
                        //前面不用补
                        remainCount -= 0;
                        break;
                    case DayOfWeek.Tuesday:
                        //前面补一个
                        remainCount -= 1;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Wednesday:
                        //前面补两个
                        remainCount -= 2;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Thursday:
                        //前面补三个
                        remainCount -= 3;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Friday:
                        //前面补四个
                        remainCount -= 4;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-4).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Saturday:
                        //前面补五个
                        remainCount -= 5;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-5).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-4).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    case DayOfWeek.Sunday:
                        //前面补六个
                        remainCount -= 6;
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-6).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-5).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-4).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-3).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-2).Day.ToString());
                        listStr.Add(firstDayOfCurrentMonth.AddDays(-1).Day.ToString());
                        break;
                    default:
                        throw new ArgumentException($"{nameof(fistDayOfWeek)} is invalid");
                }
            }

            for (int i = 0; i < remainCount; i++)
            {
                listStr.Add(firstDayOfCurrentMonth.AddDays(i).Day.ToString());
            }
            return listStr;
        }
    }
}
