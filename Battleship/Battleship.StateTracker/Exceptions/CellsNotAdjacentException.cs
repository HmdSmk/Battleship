using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Exceptions
{
	public class CellsNotAdjacentException : Exception
	{
		public List<BoardCellLocation> Locations { get; private set; }
		public CellsNotAdjacentException(List<BoardCellLocation> locations)
			: base(nameof(CellsNotAdjacentException))
		{
			Locations = locations;
		}
	}
}
