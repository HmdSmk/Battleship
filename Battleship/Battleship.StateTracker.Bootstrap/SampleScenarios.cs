using Battleship.StateTracker.Factories;
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.StateTracker.Bootstrap
{
	public class SampleScenarios
	{
		private IShipFactory shipFactory;
		public SampleScenarios(IShipFactory shipFactory)
		{
			this.shipFactory = shipFactory ?? throw new ArgumentNullException(nameof(shipFactory));
		}

		public void RunHappyPathWithVerticalShip()
		{
			Log($"Starting : {nameof(RunHappyPathWithVerticalShip)}");
			var shipLocations = new List<BoardCellLocation>()
			{
				new BoardCellLocation(Enums.ColumnIndexes.ColA, Enums.RowIndexes.Row0),
				new BoardCellLocation(Enums.ColumnIndexes.ColB, Enums.RowIndexes.Row0),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row0),
				new BoardCellLocation(Enums.ColumnIndexes.ColD, Enums.RowIndexes.Row0),
			};

			Run(shipLocations);
			Log($"---------------------");
		}
		public void RunHappyPathWithHorizontalShip()
		{
			Log($"Starting : {nameof(RunHappyPathWithHorizontalShip)}");
			var shipLocations = new List<BoardCellLocation>()
			{
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row0),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row1),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row2),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row3),
			};

			Run(shipLocations);
			Log($"---------------------");
		}

		private void Run(List<BoardCellLocation> shipLocations)
		{
			var board = new Board();
			var stateTracker = new StateTracker(board, shipFactory);

			LogGameStatus(stateTracker);

			Log($"Building board");
			stateTracker.BuildBoard();
			LogGameStatus(stateTracker);

			//var logText = shipLocations.Select(x => x.ToString()).ToList<string>();
			Log($"Adding ship to : {string.Join(",", shipLocations.Select(x => x.ToString()).ToList<string>())}");
			stateTracker.AddShip(shipLocations);
			LogGameStatus(stateTracker);

			shipLocations.ForEach
				(x =>
				{
					Log($"Attacking : ({x.ToString()})");
					stateTracker.Attack(x);
					LogGameStatus(stateTracker);
				});
		}

		private void LogGameStatus(StateTracker stateTracker)
		{
			Log($"Game Status : {stateTracker.GetGameStatus()}");
		}

		private void Log(string msg)
		{
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{msg}");
		}
	}
}
