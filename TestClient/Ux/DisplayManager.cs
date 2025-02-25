using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

// https://dotnetcodr.com/2016/09/27/how-to-redirect-standard-output-for-a-net-console-application/

namespace TestClient.Ux
{
	public class DisplayManager
	{
		public static DisplayManager Instance { get; private set; }

		public CommandManager CommandManager { get; set; }
		private AutoResetEvent refreshEvent;

		public event EventHandler Refreshed;

		public int Height { get { return Console.WindowHeight; } }
		public int Width { get { return Console.WindowWidth; } }

		public DisplayManager()
		{
			Instance = this;

			refreshEvent = new AutoResetEvent(false);

			Console.Clear();

			// Console.WindowTop = 0;
			// Console.BufferHeight = Console.WindowHeight;
		}

		public System.Drawing.Point GetCursor()
		{
			return new System.Drawing.Point(Console.CursorLeft, Console.CursorTop);
		}

		public void Move(int col, int line)
		{
			if (col < 0) col = Console.WindowWidth + col;
			if (line < 0) line = Console.WindowHeight + line;

			Console.SetCursorPosition(col, line);
		}

		public void MoveToInput()
		{
			Move(2, -1);
		}

		public void Refresh()
		{
			refreshEvent.Set();
			try
			{
				if (Refreshed != null)
					Refreshed(this, EventArgs.Empty);
			}
			catch
			{
			}
		}

		public void Wait()
		{
			refreshEvent.WaitOne();
		}

		public void Clear()
		{
			Console.Clear();
		}

		public void Clear(int line)
		{
			Move(0, line);
			Console.Write(new string(' ', Console.WindowWidth));
		}

		public void Write(int col, int line, string text)
		{
			Move(col, line);
			Console.Write(text);
		}

		public void WriteClear(int col, int line, int length, string text)
		{
			Write(col, line, new string(' ', length));
			Write(col, line, text);
		}

		public void WriteLine(string text)
		{
		}
	}
}
