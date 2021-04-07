using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Battleship.StateTracker.Exceptions;

namespace Battleship.StateTracker
{
	public class StateTracker
	{
		public Board Board { get; private set; }
		public StateTracker(Board board)
		{
			Board = board ?? throw new ArgumentNullException(nameof(board));
		}
		public void BuildBoard()
		{
			var columns = Enum.GetValues(typeof(ColumnIndexes)).Cast<ColumnIndexes>().ToList();
			var rows = Enum.GetValues(typeof(RowIndexes)).Cast<RowIndexes>().ToList();

			columns.ForEach(column => rows.ForEach(row =>
			{
				var cell = Board.CreateBoardCell(new BoardCellLocation(column, row));
				if (Board.CellExists(cell))
				{
					throw new BoardCellAlreadyExistsException(cell);
				}
				Board.AddBoardCell(cell);
			}));

		}
	}
}
