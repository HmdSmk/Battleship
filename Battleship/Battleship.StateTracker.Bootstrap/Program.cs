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
			var sampleScenarios = new SampleScenarios(shipFactory);

			sampleScenarios.RunHappyPathWithVerticalShip();
			sampleScenarios.RunHappyPathWithHorizontalShip();

		}
	}
}
