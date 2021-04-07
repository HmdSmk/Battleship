using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System.Collections.Generic;

namespace Battleship.StateTracker
{
	public interface IStateTracker
	{
		Board Board { get; }

		Ship AddShip(List<BoardCellLocation> cellLocations);
		CellState Attack(BoardCellLocation location);
		void BuildBoard();
		GameStatus GetGameStatus();
		void TransitionCellState(BoardCell boardCell, CellState targetState);
	}
}