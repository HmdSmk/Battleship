
using Battleship.StateTracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Models
{
	public class BoardCellLocation
	{
		public BoardCellLocation(ColumnIndexes column, RowIndexes row)
		{
			Column = column;
			Row = row;
		}
		public ColumnIndexes Column { get; }
		public RowIndexes Row { get; }

		public override string ToString()
		{
			return $"{Column}:{Row}";
		}

		public static bool operator ==(BoardCellLocation left, BoardCellLocation right)
		{
			if (left?.Column == null || left?.Row == null || right?.Column == null || right?.Row == null)
			{
				return false;
			}

			return left.Column == right.Column && left.Row == right.Row;
		}

		public static bool operator !=(BoardCellLocation left, BoardCellLocation right)
		{
			if (left?.Column == null || left?.Row == null || right?.Column == null || right?.Row == null)
			{
				return false;
			}

			return !(left.Column == right.Column && left.Row == right.Row);
		}

	}
}
