using Battleship.StateTracker.Factories;
using Battleship.StateTracker.Models;
using System;
using System.Collections.Generic;

namespace Battleship.StateTracker.Bootstrap
{
	class Program
	{
		static void Main(string[] args)
		{
			IShipFactory shipFactory = new ShipFactory();
			IStateTrackerFactory stateTrackerFactory = new StateTrackerFactory(shipFactory);
			var sampleScenarios = new SampleScenarios(stateTrackerFactory);

			sampleScenarios.RunHappyPathWithVerticalShip();
			sampleScenarios.RunHappyPathWithHorizontalShip();
			sampleScenarios.RunUnHappyPath1();
			sampleScenarios.RunUnHappyPath2();

		}
	}
}
