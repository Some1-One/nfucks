﻿using nFucks;

namespace nFucksTest
{
    class MainClass
    {
		static TermSize resv = TermSize.CurrentTermSize;
		static TermResolution res = new TermResolution(resv.X, resv.Y, 2, 1);
		static FucksManager fucksManager = new FucksManager();
		//FucksSurfaceManager fucksManager = new FucksSurfaceManager(res, new TermColorProvider((TermPosition pos, bool fore) => fore ? System.ConsoleColor.DarkBlue : System.ConsoleColor.Red));
        public static void Main(string[] args)
        {
			var surface0 = fucksManager.CreateAndInitializeSurface(new TermPosition(0, 0), new TermResolution(10, 40));
			var surface1 = fucksManager.CreateAndInitializeSurface(new TermPosition(4, 6), new TermResolution(14, 42, 1, 2));
			surface1.defaultProvider = new TermColorProvider((arg1, arg2) => arg2 ? System.ConsoleColor.White : System.ConsoleColor.DarkGreen);
			var pos = new TermPosition(1, 2);
			surface0.PutString("Hello, World", ref pos);
			surface0.SetBackColor(pos, BasicColor.Green);
			pos.advance(surface0.bounds);
			surface0.PutChar('!', pos);
			pos.Set(2, 2);
			surface1.PutString("what", ref pos);
			surface0.PutChar('*', ref pos);
			surface0.drawBounds();
			surface1.drawBounds();
			fucksManager.renderOnce();
			System.Console.ReadKey();
			fucksManager.Focus(surface0);
			fucksManager.renderOnce();
			// fucksManager.runInternLoop((char c) => fucksManager.PutChar(c, pos));
		}
    }
}
