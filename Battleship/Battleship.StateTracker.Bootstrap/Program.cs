using Battleship.StateTracker.Models;
using System;

namespace Battleship.StateTracker.Bootstrap
{
	class Program
	{
		static void Main(string[] args)
		{
			var board = new Board();
			var stateTracker = new StateTracker(board);
			stateTracker.BuildBoard();
		}
	}
}
