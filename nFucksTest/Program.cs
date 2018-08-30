﻿using nFucks;

namespace nFucksTest
{
    class MainClass
    {
		static TermSize resv = TermSize.CurrentTermSize;
		// get a manager
		static FucksManager fucksManager = new FucksManager();
        public static void Main(string[] args)
        {
			// get a surface at 8,10 with size of 10x40 real characters
			var surface0 = fucksManager.CreateAndInitializeSurface(new TermPosition(8, 10), new TermResolution(10, 40));
			// get a surface at 0,0 with a scaled size (two real characters per Y cell) of 10x20 characters, and set the "skipped" cells to '*'
			var surface1 = fucksManager.CreateAndInitializeSurface(new TermPosition(0, 0), new TermResolution(10, 40, 1, 2), new char[,] { { '*', FucksSurfaceManager.FillValue } });
			// set the default color scheme of the first surface (foreground is black, background is gray)
			surface0.defaultProvider = new StaticTermColorProvider(System.ConsoleColor.Black, System.ConsoleColor.Gray);

			// write "Hello, world" starting at 1,2 of the first surface
			var pos = new TermPosition(1, 2);
			surface0.PutString("Hello, World", ref pos);
			// make the 'd' background green
			surface0.SetBackColor(pos, BasicColor.Green);

			// put a '!' after the 'd'
			pos.advanceRight(surface0.bounds);
			surface0.PutChar('!', pos);

			// put the string "what" starting at 2,2 of the second surface
			pos.Set(2, 2);
			surface1.PutString("what", ref pos);

			// put a '*' where the 't' would be if it were rendered in the first surface
			surface0.PutChar('*', ref pos);

			// draw a frame for both windows
			surface0.drawBounds();
			surface1.drawBounds();
            TermPosition termPosition = new TermPosition(1, 1);
            // render one iteration

            // burrow three cells and spread them in a nice surface next to each other
            using (var tempSurface = surface1.burrowCells(new TermPosition(2, 2), new TermPosition(2, 4), new TermPosition(4, 3)))
            {
                // write "te" "st" "th" in those individual cells
                var posv = new TermPosition(0, 0);
                tempSurface.PutString("testth", ref posv);
                // the cells will be automatically merged back into the main surface when this block ends
            }

			fucksManager.renderOnce();
            int i = 0;
			while(true)
            {
                i++;
                fucksManager.PutChar(surface0, System.Console.ReadKey(true).KeyChar, ref termPosition);
                // focus the first surface and render it
                // fucksManager.Focus(i%2==1 ? surface0 : surface1);
                fucksManager.renderOnce();
                
                // focus the second surface, move it around and render it again
                // fucksManager.Focus(surface1);
                // move the surface around a bit            
				// fucksManager.Translate(surface1, (i % 4 - 1), (i % 2 - 1) * -1);
				fucksManager.renderOnce();
            }
		}
    }
}
