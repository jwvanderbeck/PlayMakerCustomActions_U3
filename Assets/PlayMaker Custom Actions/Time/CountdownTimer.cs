// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Countdown from a certain time to a certain time and possible to display time left")]
	public class CountdownTimer  : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Time To Countdown from")]
		public FsmFloat time;
		[Tooltip("Stop timer when this time is reached. Value must be lower than time")]
		public FsmFloat stopOn;
		public FsmEvent finishEvent;
		public bool realTime;
		public FsmFloat storeValue;
		
		private float startTime;
		private float timer;
		
		public override void Reset()
		{
			time = 1f;
			stopOn = 0f;
			finishEvent = null;
			realTime = false;
			storeValue = null;
		}
		
		public override void OnEnter()
		{
			if (time.Value <= stopOn.Value)
			{
				Fsm.Event(finishEvent);
				Finish();
				return;
			}
			
			startTime = FsmTime.RealtimeSinceStartup;
			timer = 0f;
		}
		
		public override void OnUpdate()
		{
			if (realTime)
			{
				timer = FsmTime.RealtimeSinceStartup - startTime;
				storeValue.Value = time.Value - timer;
			}
			else
			{
				timer += Time.deltaTime;
				storeValue.Value = time.Value - timer;
			}
			
			if (storeValue.Value <= stopOn.Value)
			{
				Finish();
				if (finishEvent != null)
				{
					Fsm.Event(finishEvent);
				}
			}
		}
		
	}
}
