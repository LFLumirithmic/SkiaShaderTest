using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using IImage = Microsoft.Maui.Graphics.IImage;
using SkiaSharp.Views.Maui;
using System.Diagnostics;

namespace SkiaShaderTest.Controls
{
    public class Panel : SKCanvasView
    {
        public static BindableProperty ShaderOnProperty = BindableProperty.Create(
            nameof(ShaderOn),
            typeof(bool),
            typeof(Panel),
            false,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var panel = (Panel)bindable;
                panel.InvalidateSurface();
            }
        );

        public bool ShaderOn
        {
            get => (bool)GetValue(ShaderOnProperty);
            set => SetValue(ShaderOnProperty, value);
        }


        protected override void OnPaintSurface(SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs args)
        {
            var canvas = args.Surface.Canvas;
            var info = args.Info;

            canvas.Clear();

            if (ShaderOn)
            {
                var src = @"
                    float4 main(float2 fragCoord) {
                        return float4(1,0,0,1);
                    }";
                using var effect = SKRuntimeEffect.Create(src, out string error);
                using var shader = effect.ToShader(true);


                using (SKPaint paint = new SKPaint() { Shader = shader })
                {
                    try
                    {
                        canvas.DrawRect(info.Rect, paint);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Exception {e.GetType()}: {e.Message}");
                    }
                }
            }
            else
            {
                using (SKPaint paint = new SKPaint())
                {
                    paint.Color = SKColors.BlueViolet;
                    canvas.DrawRect(info.Rect, paint);
                }
            }
        }

    }
}
