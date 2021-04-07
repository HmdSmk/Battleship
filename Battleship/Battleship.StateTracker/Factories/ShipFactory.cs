using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Factories
{
	public interface IShipFactory
	{
		Ship Create(List<BoardCellLocation> cells);
	}

	public class ShipFactory : IShipFactory
	{
		public Ship Create(List<BoardCellLocation> cells)
		{
			return new Ship(cells);
		}
	}
}
