using Battleship.StateTracker.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Models
{
	public class BoardCell
	{
		protected internal BoardCell
			(
			Board board,
			BoardCellLocation location
			)
		{
			State = CellState.Vacant;
			Board = board ?? throw new ArgumentNullException(nameof(board));
			Location = location ?? throw new ArgumentNullException(nameof(location));
		}

		public Board Board { get; private set; }
		public CellState State { get; private set; }
		public BoardCellLocation Location { get; private set; }

		internal void SetState (CellState newState)
		{
			State = newState;
		}
	}
}
