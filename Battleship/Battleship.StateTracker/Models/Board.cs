using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Models
{
	public class Board
	{
		public BoardCell CreateBoardCell(BoardCellLocation location)
		{
			return new BoardCell(this, location);
		}
	}
}
