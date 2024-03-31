﻿using System;
using System.Runtime.CompilerServices;
using Cosmos.HAL;
using Cosmos.System.Graphics;

namespace OpenLisp.Core.Kernel.OS.System.Graphics.UI.CUI
{
    public class Console : UI.Console
    {
        protected int mX = 0;
        public override int X
        {
            get { return mX; }
            set
            {
                mX = value;
                UpdateCursor();
            }
        }

        protected int mY = 0;
        public override int Y
        {
            get { return mY; }
            set
            {
                mY = value;
                UpdateCursor();
            }
        }

        public override int Cols
        {
            get { return mText.Cols; }
        }

        public override int Rows
        {
            get { return mText.Rows; }
        }

        protected TextScreenBase mText;

        public Console(TextScreenBase textScreen)
        {
            Name = "VGA Textmode";
            Type = ConsoleType.Text;

            //Global.debugger.Send("VGA Textmode Class");
            if (textScreen == null)
            {
                mText = new TextScreen();
            }
            else
            {
                mText = textScreen;
            }
        }

        public override void Clear()
        {
            mText.Clear();
            mX = 0;
            mY = 0;
            UpdateCursor();
        }

        public override void Clear(uint color)
        {
            mText.Clear();
            mX = 0;
            mY = 0;
            UpdateCursor();
        }

        //TODO: This is slow, batch it and only do it at end of updates
        public override void UpdateCursor()
        {
            mText.SetCursorPos(mX, mY);
        }

        private void DoLineFeed()
        {
            mY++;
            mX = 0;
            if (mY == mText.Rows)
            {
                mText.ScrollUp();
                mY--;
            }
            UpdateCursor();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DoCarriageReturn()
        {
            mX = 0;
            UpdateCursor();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DoTab()
        {
            Write((byte)Space);
            Write((byte)Space);
            Write((byte)Space);
            Write((byte)Space);
        }

        public void Write(byte aChar)
        {
            mText[mX, mY] = aChar;
            mX++;
            if (mX == mText.Cols)
            {
                DoLineFeed();
            }
            UpdateCursor();
        }

        public override void Write(char[] aText)
        {
            //throw new NotImplementedException();
        }

        //TODO: Optimize this
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Write(byte[] aText)
        {
            if (aText == null)
            {
                return;
            }

            for (int i = 0; i < aText.Length; i++)
            {
                switch (aText[i])
                {
                    case (byte)LineFeed:
                        DoLineFeed();
                        break;

                    case (byte)CarriageReturn:
                        DoCarriageReturn();
                        break;

                    case (byte)Tab:
                        DoTab();
                        break;

                    /* Normal characters, simply write them */
                    default:
                        Write(aText[i]);
                        break;
                }
            }
        }

        public override void DrawImage(ushort X, ushort Y, Bitmap image)
        {
            // Do nothing
        }

        public override ConsoleColor Foreground
        {
            get { return (ConsoleColor)(mText.GetColor() ^ (byte)((byte)Background << 4)); }
            set { mText.SetColors(value, Background); }
        }
        public override ConsoleColor Background
        {
            get { return (ConsoleColor)(mText.GetColor() >> 4); }
            set { mText.SetColors(Foreground, value); }
        }

        public override int CursorSize
        {
            get { return mText.GetCursorSize(); }
            set
            {
                // Value should be a percentage from [1, 100].
                if (value < 1 || value > 100)
                    throw new ArgumentOutOfRangeException("value", value, "CursorSize value " + value + " out of range (1 - 100)");

                mText.SetCursorSize(value);
            }
        }

        public override bool CursorVisible
        {
            get { return mText.GetCursorVisible(); }
            set { mText.SetCursorVisible(value); }
        }

        public override int Width
        {
            get { return 80; }
        }

        public override int Height
        {
            get { return 25; }
        }

    }
}
