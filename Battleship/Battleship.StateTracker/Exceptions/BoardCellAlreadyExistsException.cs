using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Exceptions
{
	public class BoardCellAlreadyExistsException : Exception
	{
		public BoardCell BoardCell { get; private set; }
		public BoardCellAlreadyExistsException(BoardCell boardCell)
			: base(nameof(BoardCellAlreadyExistsException))
		{
			BoardCell = boardCell;
		}
	}
}
