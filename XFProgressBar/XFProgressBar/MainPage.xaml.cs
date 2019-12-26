using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Timers;

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
            Device.BeginInvokeOnMainThread(async () => await AnimateProgress(RAND.Next(100)*0.01f));
        }

        async Task AnimateProgress(float progress)
        {
            if (progress == GaugeControl.Progress)
            {
                return;
            }
            if (progress <= GaugeControl.Progress)
            {
                for (float i = GaugeControl.Progress; i >= progress; i-=0.01f)
                {
                    GaugeControl.Progress = i;
                    await Task.Delay(1);
                }
            }
            else
            {
                for (float i = GaugeControl.Progress; i <= progress; i += 0.01f)
                {
                    GaugeControl.Progress = i;
                    await Task.Delay(1);
                }
            }

        }
    }
}
