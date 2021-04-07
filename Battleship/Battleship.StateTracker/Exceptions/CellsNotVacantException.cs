using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Exceptions
{
	public class CellsNotVacantException : Exception
	{
		public BoardCellLocation Location { get; private set; }
		public CellsNotVacantException(BoardCellLocation location)
			: base(nameof(CellsNotVacantException))
		{
			Location = location;
		}
	}
}
