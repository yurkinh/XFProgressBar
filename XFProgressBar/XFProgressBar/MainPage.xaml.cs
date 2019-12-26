using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Timers;
using SkiaSharp.Views.Forms;

namespace XFProgressBar
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private Timer _timer;
        private Random RAND = new Random();
        public MainPage()
        {
            InitializeComponent();

            _timer = new Timer()
            {
                Interval = 2000
            };
            //Trigger event every second      
            _timer.Elapsed += OnTimedEvent;

            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => await AnimateProgress1(RAND.Next(100)));
            Device.BeginInvokeOnMainThread(async () => await AnimateProgress2(RAND.Next(100)));
        }

        async Task AnimateProgress1(float progress)
        {
            if (progress == VGaugeControl.Progress)
            {
                return;
            }
            if (progress <= VGaugeControl.Progress)
            {
                for (int i = VGaugeControl.Progress; i >= progress; i--)
                {
                    VGaugeControl.Progress = i;
                    await Task.Delay(1);
                }
            }
            else
            {
                for (int i = VGaugeControl.Progress; i <= progress; i ++)
                {
                    VGaugeControl.Progress = i;
                    await Task.Delay(1);
                }
            }

        }

        async Task AnimateProgress2(float progress)
        {
            if (progress == HGaugeControl.Progress)
            {
                return;
            }
            if (progress <= HGaugeControl.Progress)
            {
                for (int i = HGaugeControl.Progress; i >= progress; i--)
                {
                    HGaugeControl.Progress = i;
                    await Task.Delay(1);
                }
            }
            else
            {
                for (int i = HGaugeControl.Progress; i <= progress; i++)
                {
                    HGaugeControl.Progress = i;
                    await Task.Delay(1);
                }
            }

        }
    }
}
