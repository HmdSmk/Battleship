
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Exceptions
{
	class CellNotFoundException : Exception
	{
		public BoardCellLocation Location { get; private set; }
		public CellNotFoundException(BoardCellLocation location)
			: base(nameof(CellNotFoundException))
		{
			Location = location;
		}
	}
}
