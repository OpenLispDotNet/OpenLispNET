﻿
using OpenLisp.Core.Kernel.OS.System.Graphics.UI.GUI.Components;
using OpenLisp.Core.Kernel.OS.System.Processing;
using OpenLisp.Core.Kernel.OS.System.Processing.Processes;
using System.Collections.Generic;
using Cosmos.HAL;
using System;
using Cosmos.System;
using Cosmos.System.Graphics;
using OpenLisp.Core.Kernel.OS.System.Utils;

namespace OpenLisp.Core.Kernel.OS.System.Input
{
    public enum CursorState
    {
        Normal,
        ResizeHorizontal,
        ResizeVertical,
        Grab
    }

    /// <summary>
    /// Manages mouse and cursor position for AuraOS. 
    /// </summary>
    public class MouseManager : Process, IManager
    {
        /// <summary>
        /// Represents the top component currently under the mouse cursor.
        /// </summary>
        public Component TopComponent;

        /// <summary>
        /// Indicates whether the left mouse button is currently being held down.
        /// </summary>
        public bool IsLeftButtonDown;

        /// <summary>
        /// Indicates whether the right mouse button is currently being held down.
        /// </summary>
        public bool IsRightButtonDown;

        /// <summary>
        /// Focused component (set by click)
        /// </summary>
        public Component FocusedComponent;

        public static CursorState CursorState = CursorState.Normal;

        /// <summary>
        /// The maximum time interval in milliseconds to detect a double click.
        /// </summary>
        private const int doubleClickTime = 500;

        /// <summary>
        /// Timestamp of the last left mouse button click. Used for double click detection.
        /// </summary>
        private DateTime _lastLeftClickTime;

        /// <summary>
        /// Timestamp of the last right mouse button click. Used for double click detection.
        /// </summary>
        private DateTime _lastRightClickTime;

        /// <summary>
        /// Flag to indicate whether the left mouse button is currently pressed.
        /// </summary>
        private bool _leftButtonPressed;

        /// <summary>
        /// Flag to indicate whether the right mouse button is currently pressed.
        /// </summary>
        private bool _rightButtonPressed;

        private Bitmap _cursorNormal;
        private Bitmap _cursorResizeHorizontal;
        private Bitmap _cursorResizeVertical;
        private Bitmap _cursorGrap;

        public MouseManager() : base(nameof(MouseManager), ProcessType.KernelComponent)
        {
        }

        /// <summary>
        /// Initializes the mouse manager and prepares buttons states.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            CustomConsole.WriteLineInfo("Starting mouse manager...");

            _lastLeftClickTime = DateTime.MinValue;
            _lastRightClickTime = DateTime.MinValue;
            _leftButtonPressed = false;
            _rightButtonPressed = false;
            IsLeftButtonDown = false;
            IsRightButtonDown = false;

            CustomConsole.WriteLineInfo("Starting mouse...");
            Cosmos.System.MouseManager.ScreenWidth = Kernel.ScreenWidth;
            Cosmos.System.MouseManager.ScreenHeight = Kernel.ScreenHeight;

            CursorState = CursorState.Normal;

            _cursorNormal = Kernel.ResourceManager.GetIcon("00-cursor.bmp");
            _cursorResizeHorizontal = Kernel.ResourceManager.GetIcon("00-resize-horizontal.bmp");
            _cursorResizeVertical = Kernel.ResourceManager.GetIcon("00-resize-vertical.bmp");
            _cursorGrap = Kernel.ResourceManager.GetIcon("00-grab.bmp");

            Kernel.ProcessManager.Register(this);
            Kernel.ProcessManager.Start(this);
        }

        /// <summary>
        /// Updates the state of the mouse, processing clicks, double clicks, and scrolling.
        /// </summary>
        public override void Update()
        {
            if (Cosmos.System.MouseManager.MouseState == MouseState.Left)
            {
                if (!_leftButtonPressed)
                {
                    ProcessLeftClick();
                    _leftButtonPressed = true;
                }
                IsLeftButtonDown = true;
            }
            else
            {
                if (_leftButtonPressed)
                {
                    _leftButtonPressed = false;
                }
                IsLeftButtonDown = false;
            }

            if (Cosmos.System.MouseManager.MouseState == MouseState.Right)
            {
                if (!_rightButtonPressed)
                {
                    ProcessRightClick();
                    _rightButtonPressed = true;
                }
                IsRightButtonDown = true;
            }
            else
            {
                if (_rightButtonPressed)
                {
                    _rightButtonPressed = false;
                }
                IsRightButtonDown = false;
            }

            HandleScroll();

            DrawCursor(Cosmos.System.MouseManager.X, Cosmos.System.MouseManager.Y);
        }

        /// <summary>
        /// Processes a left click action. Determines if the click is a single or double click based on the time elapsed since the last click.
        /// </summary>
        private void ProcessLeftClick()
        {
            if ((DateTime.Now - _lastLeftClickTime).TotalMilliseconds < doubleClickTime)
            {
                HandleLeftDoubleClick();
            }
            else
            {
                HandleLeftSingleClick();
            }

            _lastLeftClickTime = DateTime.Now;
        }

        /// <summary>
        /// Processes a right click action. Determines if the click is a single or double click based on the time elapsed since the last click.
        /// </summary>
        private void ProcessRightClick()
        {
            if ((DateTime.Now - _lastRightClickTime).TotalMilliseconds < doubleClickTime)
            {
                HandleRightDoubleClick();
            }
            else
            {
                HandleRightSingleClick();
            }

            _lastRightClickTime = DateTime.Now;
        }

        /// <summary>
        /// Handles the action for a single left click. Determines the top component for the click and manages the context menu's state.
        /// </summary>
        private void HandleLeftSingleClick()
        {
            Component topComponent = DetermineTopComponent();

            if (topComponent != null)
            {
                topComponent.HandleLeftClick();
            }
        }

        /// <summary>
        /// Handles the action for a double left click.
        /// </summary>
        private void HandleLeftDoubleClick()
        {

        }

        /// <summary>
        /// Handles the action for a single right click. Determines the top component for the click and manages the context menu's state.
        /// </summary>
        private void HandleRightSingleClick()
        {
            Component topComponent = DetermineTopComponent();

            if (topComponent != null)
            {
                topComponent.HandleRightClick();
            }
        }

        /// <summary>
        /// Handles the action for a double right click.
        /// </summary>
        private void HandleRightDoubleClick()
        {

        }

        /// <summary>
        /// Handles the mouse scroll action. Resets the scroll delta after processing.
        /// </summary>
        private void HandleScroll()
        {
            //if (Cosmos.System.MouseManager.ScrollDelta != 0)
            //{
            //    Cosmos.System.MouseManager.ResetScrollDelta();
            //}
            if (Cosmos.System.MouseManager.ScrollDelta != 0)
            {
                Cosmos.System.MouseManager.ResetScrollDelta();
            }
        }

        /// <summary>
        /// Determines the top-level component. This method checks all applications and finds the one with the highest Z index under the mouse cursor.
        /// </summary>
        private Component DetermineTopComponent()
        {
            Component topComponent = null;
            int topZIndex = -1;

            void CheckComponent(Component component)
            {
                if (component.Visible && component.IsInside((int)Cosmos.System.MouseManager.X, (int)Cosmos.System.MouseManager.Y))
                {
                    if (component.zIndex > topZIndex)
                    {
                        topComponent = component;
                        topZIndex = component.zIndex;
                    }
                }

                for (int i = 0; i < component.Children.Count; i++)
                {
                    Component child = component.Children[i];
                    CheckComponent(child);
                }
            }

            for (int i = 0; i < Component.Components.Count; i++)
            {
                Component component = Component.Components[i];
                CheckComponent(component);
            }

            return topComponent;
        }

        public void DrawCursor(uint x, uint y)
        {
            if (CursorState == CursorState.Normal)
            {
                Explorer.Screen.DrawImageAlpha(_cursorNormal, (int)x, (int)y);
            }
            else if (CursorState == CursorState.ResizeHorizontal)
            {
                Explorer.Screen.DrawImageAlpha(_cursorResizeHorizontal, (int)x - 23 / 2, (int)y);
            }
            else if (CursorState == CursorState.ResizeVertical)
            {
                Explorer.Screen.DrawImageAlpha(_cursorResizeVertical, (int)x, (int)y - 23 / 2);
            }
            else if (CursorState == CursorState.Grab)
            {
                Explorer.Screen.DrawImageAlpha(_cursorGrap, (int)x, (int)y);
            }
        }

        /// <summary>
        /// Returns the name of the manager.
        /// </summary>
        /// <returns>The name of the manager.</returns>
        public string GetName()
        {
            return nameof(MouseManager);
        }
    }
}
