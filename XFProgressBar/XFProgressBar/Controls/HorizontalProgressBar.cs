using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using Xamarin.Forms;

namespace XFProgressBar.Controls
{
    public class HorizontalProgressBar : SKCanvasView
    {
        public HorizontalProgressBar()
        {
            WidthRequest = 300;
            HeightRequest = 300;
        }

        public static readonly BindableProperty BGColorProperty =
           BindableProperty.Create(nameof(BGColor), typeof(Color), typeof(VerticalProgressBar), Color.White);

        public Color BGColor
        {
            get { return (Color)GetValue(BGColorProperty); }
            set { SetValue(BGColorProperty, value); }
        }

        public static readonly BindableProperty BarColorProperty =
           BindableProperty.Create(nameof(BarColor), typeof(Color), typeof(VerticalProgressBar), Color.FromRgba(52 / 255, 181 / 255, 240 / 255, 1));

        public Color BarColor
        {
            get { return (Color)GetValue(BarColorProperty); }
            set { SetValue(BarColorProperty, value); }
        }

        public static readonly BindableProperty FrameColorProperty =
           BindableProperty.Create(nameof(FrameColor), typeof(Color), typeof(VerticalProgressBar), Color.FromRgba(161 / 255, 161 / 255, 161 / 255, 1));

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty StartColorProperty =
           BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(VerticalProgressBar), Color.Default);

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
           BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(VerticalProgressBar), Color.Default);

        public Color FrameColor
        {
            get { return (Color)GetValue(FrameColorProperty); }
            set { SetValue(FrameColorProperty, value); }
        }

        public static readonly BindableProperty FrameBoldProperty =
           BindableProperty.Create(nameof(FrameBold), typeof(float), typeof(VerticalProgressBar), 0.1f);

        public float FrameBold
        {
            get { return (float)GetValue(FrameBoldProperty); }
            set { SetValue(FrameBoldProperty, value); }
        }

        public static readonly BindableProperty PGHeightProperty =
          BindableProperty.Create(nameof(PGHeight), typeof(float), typeof(VerticalProgressBar), 200f);

        public float PGHeight
        {
            get { return (float)GetValue(PGHeightProperty); }
            set { SetValue(PGHeightProperty, value); }
        }

        public static readonly BindableProperty PGWidthProperty =
          BindableProperty.Create(nameof(PGWidth), typeof(float), typeof(VerticalProgressBar), 20f);

        public float PGWidth
        {
            get { return (float)GetValue(PGWidthProperty); }
            set { SetValue(PGWidthProperty, value); }
        }

        public static readonly BindableProperty ProgressProperty =
           BindableProperty.Create(nameof(Progress), typeof(int), typeof(VerticalProgressBar), 0);

        public int Progress
        {
            get { return (int)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            int width = e.Info.Width;
            int height = e.Info.Height;

            SKPaint backPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.WhiteSmoke,
            };

            canvas.DrawRect(new SKRect(0, 0, width, height), backPaint);

            canvas.Save();

            canvas.Translate(width / 2, height / 2);
            canvas.Scale(Math.Min(width / 210f, height / 520f));

            var rect = new SKRect(-100, -100, 100, 100);

            // Add a buffer for the rectangle
            rect.Inflate(-10, -10);

            var bgColorPaint = new SKPaint
            {
                Color = BGColor.ToSKColor(),
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                StrokeWidth = 0
            };


            var barColorPaint = new SKPaint
            {
                Color = BarColor.ToSKColor(),
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                StrokeWidth = 0
            };

            var frameColorPaint = new SKPaint
            {
                Color = FrameColor.ToSKColor(),
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2
            };

            var skrect = new SKRect(0, 0, PGWidth, PGHeight);
            var skRoundRect = new SKRoundRect(skrect, PGHeight / 2, PGHeight / 2);

            canvas.DrawRoundRect(0, 0, PGWidth, PGHeight, PGHeight / 2, PGHeight / 2, bgColorPaint);
            canvas.DrawRoundRect(skRoundRect, frameColorPaint);
            canvas.ClipRoundRect(skRoundRect, SKClipOperation.Intersect);

            if (StartColor != Color.Default && EndColor != Color.Default)
            {
                barColorPaint.Shader = SKShader.CreateLinearGradient(
                               new SKPoint(skrect.Left, skrect.Top),
                               new SKPoint(skrect.Right, skrect.Bottom),
                               new SKColor[] { StartColor.ToSKColor(), EndColor.ToSKColor() },
                               new float[] { 0, 1 },
                               SKShaderTileMode.Repeat);
            }
            canvas.DrawRoundRect(PGWidth-PGWidth * Progress / 100, 0, PGWidth, PGWidth, PGHeight / 2, 0, barColorPaint);

            

            canvas.Restore();

        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Determine when to change. Basically on any of the properties that we've added that affect
            // the visualization, including the size of the control, we'll repaint
            if (propertyName == BGColorProperty.PropertyName
                || propertyName == BarColorProperty.PropertyName
                || propertyName == FrameColorProperty.PropertyName
                || propertyName == FrameBoldProperty.PropertyName
                || propertyName == PGHeightProperty.PropertyName
                || propertyName == PGWidthProperty.PropertyName
                || propertyName == ProgressProperty.PropertyName
                || propertyName == StartColorProperty.PropertyName
                || propertyName == EndColorProperty.PropertyName)
            {
                InvalidateSurface();
            }
        }


    }
}



