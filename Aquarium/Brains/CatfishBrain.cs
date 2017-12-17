﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Aquarium.Aquariums;
using Aquarium.Fishes;

namespace Aquarium.Brains
{
	public class CatfishBrain : Brain
	{
		private readonly Catfish _catfish;

		private readonly IAquarium _aquarium;
		private readonly Stack<Action> _states;

		public CatfishBrain(Catfish catfish, IAquarium aquarium)
		{
			_catfish = catfish;
			_aquarium = aquarium;
			_states = new Stack<Action>();
			_states.Push(Move);
		}

		private void Move()
		{
			_states.Push(Move);
			if (_catfish.GetLocation().Y < _aquarium.GetSize().Height - _aquarium.GetSize().Height / 3)
				_states.Push(MoveDown);
		}

		private void MoveDown()
		{
			if (_catfish.GetLocation().Y < _aquarium.GetSize().Height - _aquarium.GetSize().Height / 3)
				_states.Push(MoveDown);
			var targetLocation = new Point(_aquarium.GetSize().Width, _aquarium.GetSize().Width);
			var neonLocation = _catfish.GetLocation();
			var vector = new Vector(targetLocation.X - neonLocation.X, targetLocation.Y - neonLocation.Y);
			OnDirectionChanged(vector.Angle);
		}

		public override void Think()
		{
			if (_states.Count != 0)
				_states.Pop()();
		}
	}
}