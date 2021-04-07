using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Exceptions
{
	public class IllegitimateTransitionException : Exception
	{
		public BoardCell BoardCell { get; private set; }
		public CellState TargetState { get; private set; }
		public IllegitimateTransitionException(BoardCell boardCell, CellState targetState)
			: base(nameof(IllegitimateTransitionException))
		{
			BoardCell = boardCell;
			TargetState = targetState;
		}
	}
}
