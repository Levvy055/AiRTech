﻿using AiRTech.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AiRTech.Core.Misc
{
    public partial class ZoomContainer : ContentView
    {
        private double _currentScale = 1;
        private double _startScale = 1;
        private double _xOffset = 0;
        private double _yOffset = 0;
        private double x, y;

        public ZoomContainer()
        {
            var pinchGesture = new PinchGestureRecognizer();
            var panGesture = new PanGestureRecognizer();
            var tapGesture = new TapGestureRecognizer();

            pinchGesture.PinchUpdated += OnPinchUpdated;
            panGesture.PanUpdated += OnPanUpdated;
            tapGesture.Tapped += Tapped;

            GestureRecognizers.Add(pinchGesture);
            GestureRecognizers.Add(panGesture);
            GestureRecognizers.Add(tapGesture);
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    Content.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - AiRTechApp.ScreenWidth));
                    Content.TranslationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(Content.Height - AiRTechApp.ScreenHeight));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = Content.TranslationX;
                    y = Content.TranslationY;
                    break;
            }
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                _startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                _currentScale += (e.Scale - 1) * _startScale;
                _currentScale = Math.Max(1, _currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + _xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * _startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + _yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * _startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = _xOffset - (originX * Content.Width) * (_currentScale - _startScale);
                double targetY = _yOffset - (originY * Content.Height) * (_currentScale - _startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = targetX.Clamp(-Content.Width * (_currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (_currentScale - 1), 0);

                // Apply scale factor
                Content.Scale = _currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                _xOffset = Content.TranslationX;
                _yOffset = Content.TranslationY;
            }
        }

        private void Tapped(object sender, EventArgs e)
        {
            CoreManager.Current.App.NavigateBack();
        }
    }
}
