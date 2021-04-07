using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Battleship.StateTracker.Exceptions;
using Battleship.StateTracker.Extensions;
using Battleship.StateTracker.Factories;

namespace Battleship.StateTracker
{
	public class StateTracker : IStateTracker
	{

		private readonly Dictionary<CellState, List<CellState>> legitimateStateTransitions;
		private List<Ship> ships;
		private readonly IShipFactory shipFactory;
		public Board Board { get; private set; }
		public StateTracker(Board board, IShipFactory shipFactory)
		{
			Board = board ?? throw new ArgumentNullException(nameof(board));
			this.shipFactory = shipFactory ?? throw new ArgumentNullException(nameof(board));
			legitimateStateTransitions = CreateLegitimateStateTransitions();
			ships = new List<Ship>();
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

		private bool IsLegitimateMove(CellState sourceState, CellState targetState)
		{
			return legitimateStateTransitions.ContainsKey(sourceState) &&
				legitimateStateTransitions[sourceState].Contains(targetState);
		}
		private void ValidateCurrentGameStatus(params GameStatus[] legitimateGameStatuses)
		{
			if ((legitimateGameStatuses?.Count() ?? 0) == 0)
			{
				return;
			}

			var gameStatus = this.GetGameStatus();
			if (!legitimateGameStatuses.Contains(gameStatus))
			{
				throw new InvalidGameStatusException(gameStatus);
			}
		}
		private CellState? GetAttachTargetState(CellState sourceState)
		{
			switch (sourceState)
			{
				case CellState.Hit: return null;
				case CellState.Miss: return null;
				case CellState.Occupied: return CellState.Hit;
				case CellState.Vacant: return CellState.Miss;
				default: return null;
			}
		}

		public void BuildBoard()
		{
			ValidateCurrentGameStatus(GameStatus.NotSetUp);

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
		public Ship AddShip(List<BoardCellLocation> cellLocations)
		{
			ValidateCurrentGameStatus(GameStatus.NotSetUp, GameStatus.NotStarted);

			cellLocations = cellLocations ?? throw new ArgumentNullException(nameof(cellLocations));
			var verticallyAdjacent = cellLocations.AreVerticallyAdjacent();
			var horizontallyAdjacent = cellLocations.AreHorizontallyAdjacent();
			if ((!verticallyAdjacent) && (!horizontallyAdjacent))
			{
				throw new CellsNotAdjacentException(cellLocations);
			}

			var targetCells = Board.GetCells().Where(x => cellLocations.Exists(y => x.Location == y)).ToList();
			var nonVacantCell = targetCells.FirstOrDefault(x => x.State != CellState.Vacant);
			if (nonVacantCell != null)
			{
				throw new CellsNotVacantException(nonVacantCell.Location);
			}
			var ship = shipFactory.Create(cellLocations);
			targetCells.ForEach(x => x.SetState(CellState.Occupied));
			ships.Add(ship);

			return ship;
		}
		public void TransitionCellState(BoardCell boardCell, CellState targetState)
		{
			if (boardCell == null)
			{
				throw new ArgumentNullException(nameof(boardCell));
			}

			ValidateCurrentGameStatus(GameStatus.InProgress, GameStatus.NotStarted);

			if (!IsLegitimateMove(boardCell.State, targetState))
			{
				throw new IllegitimateTransitionException(boardCell, targetState);
			}

			boardCell.SetState(targetState);
		}
		public CellState Attack(BoardCellLocation location)
		{
			if (location == null)
			{
				throw new ArgumentNullException(nameof(location));
			}
			ValidateCurrentGameStatus(GameStatus.InProgress, GameStatus.NotStarted);
			var boardCell = Board.GetCells().Where(x => x.Location == location).FirstOrDefault()
				?? throw new CellNotFoundException(location);

			var targetState = GetAttachTargetState(boardCell.State)
				?? throw new IllegitimateTransitionException(boardCell, CellState.Hit);

			TransitionCellState(boardCell, targetState);
			return targetState;
		}

		public GameStatus GetGameStatus()
		{
			if (ships.Count == 0)
			{
				return GameStatus.NotSetUp;
			}

			var cells = Board.GetCells()?.ToList() ?? new List<BoardCell>();
			if (cells.Count == 0)
			{
				return GameStatus.NotSetUp;
			}

			var countOfAttacks = cells.Where(x => x.State != CellState.Vacant && x.State != CellState.Occupied).Count();
			if (countOfAttacks == 0)
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
