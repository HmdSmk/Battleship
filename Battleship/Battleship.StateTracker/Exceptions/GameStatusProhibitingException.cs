using Battleship.StateTracker.Enums;
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.StateTracker.Exceptions
{
	public class GameStatusProhibitingException : Exception
	{
		public GameStatus GameStatus { get; private set; }
		public GameStatusProhibitingException(GameStatus gameStatus)
			: base(nameof(GameStatusProhibitingException))
		{
			GameStatus = gameStatus;
		}
	}
}
