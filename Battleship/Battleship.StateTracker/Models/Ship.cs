using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.StateTracker.Models
{
	public class Ship
	{
		public List<BoardCellLocation> OccupiedCells { get; private set; }
		protected internal Ship(List<BoardCellLocation> occupiedCells)
		{
			occupiedCells = occupiedCells ?? throw new ArgumentNullException(nameof(occupiedCells));
			if (!occupiedCells.Any())
			{
				throw new ArgumentException(nameof(occupiedCells));
			}
			OccupiedCells = occupiedCells;
		}
	}
}
