using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Models
{
	public class Board
	{
		private List<BoardCell> cells;
		public Board()
		{
			cells = new List<BoardCell>();
		}
		internal BoardCell CreateBoardCell(BoardCellLocation location)
		{
			return new BoardCell(this, location);
		}

		internal void AddBoardCell(BoardCell cell)
		{
			cells.Add(cell);
		}

		internal bool CellExists(BoardCell cell)
		{
			return cells.Exists(x => x.Location.Equals(cell.Location));
		}
	}
}
