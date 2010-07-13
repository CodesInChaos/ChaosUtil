using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Chaos.Util
{
	public class ActionQueue
	{
		private readonly object Lock = new object();
		private readonly Thread BoundThread;
		private readonly List<Action> Actions = new List<Action>();
		private bool handling = false;
		private readonly Action OnChanged;

		public void Enqueue(Action action)
		{
			lock (Lock)
			{
				Actions.Add(action);
				if (!handling && OnChanged != null)
					OnChanged();
			}
		}

		public void HandleActions()
		{
			lock (Lock)
			{
				if (BoundThread != null && Thread.CurrentThread != BoundThread)
					throw new InvalidOperationException("Wrong thread, idiot");
				if (handling)
					throw new InvalidOperationException("Re-entered HandleActions");
				try
				{
					handling = true;

					for (int i = 0; i < Actions.Count; i++)
						Actions[i]();
				}
				finally
				{
					Actions.Clear();
					handling = false;
				}
			}
		}


		public ActionQueue(Thread boundThread, Action onChanged)
		{
			BoundThread = boundThread;
			OnChanged = onChanged;
		}

		public ActionQueue(Action onChanged)
			: this(Thread.CurrentThread, onChanged)
		{
		}
	}
}
