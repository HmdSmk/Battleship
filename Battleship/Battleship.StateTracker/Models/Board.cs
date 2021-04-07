using Battleship.StateTracker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
		internal BoardCell[] GetCells()
		{
			return cells.ToArray();
		}
	}
}
