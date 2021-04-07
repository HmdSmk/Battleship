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
		
		private Dictionary<CellState, List<CellState>> legitimateStateTransitions;
		public Board Board { get; private set; }
		public StateTracker(Board board)
		{
			Board = board ?? throw new ArgumentNullException(nameof(board));
			legitimateStateTransitions = CreateLegitimateStateTransitions();
		}

		private Dictionary<CellState, List<CellState>> CreateLegitimateStateTransitions()
		{
			return new Dictionary<CellState, List<CellState>>()
			{
				{ CellState.Vacant, new List<CellState>() { CellState.Occupied , CellState.Miss} },
				{ CellState.Occupied, new List<CellState>() { CellState.Hit } },
				{ CellState.Miss, new List<CellState>() { } }, //terminating state
				{ CellState.Hit, new List<CellState>() { } },  //terminating state
			};
		}

		private bool isLegitimateMove(CellState sourceState, CellState targetState)
		{
			return legitimateStateTransitions.ContainsKey(sourceState) &&
				legitimateStateTransitions[sourceState].Contains(targetState);
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
				Board.AddBoardCell(cell); // Cell additions is done only via StateTracker so that required validations are applied before-hand.
			}));
		}

		public void TransitionCellState(BoardCell boardCell, CellState targetState)
		{
			if (boardCell == null)
			{
				throw new ArgumentNullException(nameof(boardCell));
			}

			var gameStatus = GetGameStatus();
			if (gameStatus != GameStatus.InProgress)
			{
				throw new GameStatusProhibitingException(gameStatus);
			}

			if (!isLegitimateMove(boardCell.State, targetState))
			{
				throw new IllegitimateTransitionException(boardCell, targetState);
			}

			boardCell.SetState(targetState);
		}

		public GameStatus GetGameStatus()
		{
			//todo: check ships are placed or not....

			var cells = Board.GetCells()?.ToList() ?? new List<BoardCell>();
			if (cells.Count == 0)
			{
				return GameStatus.NotSetUp;
			}

			var countOfTransitionedCells = cells.Where(x => x.State != CellState.Occupied).Count();
			if (countOfTransitionedCells == 0)
			{
				return GameStatus.NotStarted;
			}

			var countOfOccupiedCells = cells.Where(x => x.State == CellState.Occupied).Count();
			if (countOfOccupiedCells == 0)
			{
				return GameStatus.GameOver;
			}

			return GameStatus.InProgress;
		}
	}
}
