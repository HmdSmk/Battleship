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
		private IStateTrackerFactory stateTrackerFactory;
		public SampleScenarios(IStateTrackerFactory stateTrackerFactory)
		{
			this.stateTrackerFactory = stateTrackerFactory ?? throw new ArgumentNullException(nameof(stateTrackerFactory));
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

		public void RunUnHappyPath1()
		{
			Log($"Starting : {nameof(RunUnHappyPath1)}");
			var shipLocations = new List<BoardCellLocation>()
			{
				new BoardCellLocation(Enums.ColumnIndexes.ColA, Enums.RowIndexes.Row0),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row1),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row2),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row3),
			};

			Run(shipLocations);
			Log($"---------------------");
		}

		public void RunUnHappyPath2()
		{
			Log($"Starting : {nameof(RunUnHappyPath2)}");
			var shipLocations = new List<BoardCellLocation>()
			{
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row5),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row1),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row2),
				new BoardCellLocation(Enums.ColumnIndexes.ColC, Enums.RowIndexes.Row3),
			};

			Run(shipLocations);
			Log($"---------------------");
		}

		private bool Run(List<BoardCellLocation> shipLocations)
		{
			try
			{
				IStateTracker stateTracker = stateTrackerFactory.Create();

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
				return true;
			}
			catch (Exception ex)
			{
				Log(ex.Message);
				return false;
			}
		}

		private void LogGameStatus(IStateTracker stateTracker)
		{
			Log($"Game Status : {stateTracker.GetGameStatus()}");
		}

		private void Log(string msg)
		{
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}:{msg}");
		}
	}
}
