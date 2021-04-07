using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Factories
{
	public interface IStateTrackerFactory
	{
		IStateTracker Create();
	}
	public class StateTrackerFactory : IStateTrackerFactory
	{
		private IShipFactory shipFactory;
		public StateTrackerFactory(IShipFactory shipFactory)
		{
			this.shipFactory = shipFactory ?? throw new ArgumentNullException(nameof(shipFactory));
		}
		public IStateTracker Create()
		{
			Board board = new Board();
			return new StateTracker(board, shipFactory);
		}
	}
}
