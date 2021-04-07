using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Exceptions
{
	public class InvalidGameStatusException : Exception
	{
		public GameStatus GameStatus { get; private set; }
		public InvalidGameStatusException(GameStatus gameStatus)
			: base(nameof(InvalidGameStatusException))
		{
			GameStatus = gameStatus;
		}
	}
}
