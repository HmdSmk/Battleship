using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.StateTracker.Extensions
{
	public static class BoardCellLocationExtensions
	{
		public static bool AreVerticallyAdjacent(this List<BoardCellLocation> cells)
		{
			if (cells == null)
			{
				throw new ArgumentNullException(nameof(cells));
			}

			if (!cells.Any())
			{
				return false;
			}

			var sortedCellValues = cells.OrderBy(x => Convert.ToInt32(x.Column)).ToList().Select(x => Convert.ToInt32(x.Column)).ToList();
			return AreAdjacent(sortedCellValues)// all cells adjacent vertically
				&& cells.Select(x=>x.Row).Distinct().Count() == 1; // and all are on the same column
		}

		public static bool AreHorizontallyAdjacent(this List<BoardCellLocation> cells)
		{
			if (cells == null)
			{
				throw new ArgumentNullException(nameof(cells));
			}

			if (!cells.Any())
			{
				return false;
			}

			var sortedCellValues = cells.OrderBy(x => Convert.ToInt32(x.Row)).ToList().Select(x => Convert.ToInt32(x.Row)).ToList();
			return AreAdjacent(sortedCellValues) // all cells adjacent horizontally
				&& cells.Select(x=>x.Column).Distinct().Count() == 1; // and all are on the same row
		}

		private static bool AreAdjacent(List<int> sortedCellValues)
		{
			var displacements = sortedCellValues
							.Select((current, index) => index == 0 ? null : (int?)(current - sortedCellValues[index - 1]))
							.Where(x => x != null)
							.Cast<int>()
							.Distinct();

			if (displacements.Count() != 1) // only 1s and -1s are expected.
			{
				return false;
			}
			if (Math.Abs(displacements.FirstOrDefault()) != 1)
			{
				return false;
			}

			return true;
		}
	}
}
