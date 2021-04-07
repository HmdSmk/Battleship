
using Battleship.StateTracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Models
{
	public class BoardCellLocation
	{
		public ColumnIndexes Column { get; }
		public RowIndexes Row { get; }

		public override string ToString()
		{
			return $"{Column}:{Row}";
		}
	}
}
