using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
#if UNITY_IOS
using UnityEngine.XR.ARKit;
#endif

[RequireComponent(typeof(ARPlaneManager))]
public class CoachingOverlay : MonoBehaviour
{
	[SerializeField] private ARSession arSession;
	[SerializeField] private Transform putOnPlane;
	[SerializeField] private Vector3 offset;

	private ARPlaneManager planeManager;

	private bool alreadyPlaced = false;

	private void Awake()
	{
		planeManager = GetComponent<ARPlaneManager>();
	}

	private void OnEnable()
	{
		if (alreadyPlaced) return;
		alreadyPlaced = true;

#if UNITY_IOS
		if (arSession.subsystem is ARKitSessionSubsystem sessionSubsystem)
		{
			sessionSubsystem.requestedCoachingGoal = ARCoachingGoal.HorizontalPlane;
			sessionSubsystem.coachingActivatesAutomatically = true;
			sessionSubsystem.sessionDelegate = new SessionDelegate(this);
		}
#else
        PutOnPlane();
#endif
	}

	private void PutOnPlane()
	{
		var lowestY = transform.position.y;
		foreach (var plane in planeManager.trackables)
		{
			lowestY = Mathf.Min(lowestY, plane.transform.position.y);
		}

		var position = transform.position;
		position.y = lowestY;
		position += offset;

		putOnPlane.position = position;
	}

#if UNITY_IOS
	class SessionDelegate : DefaultARKitSessionDelegate
	{
		private readonly CoachingOverlay coachingOverlay;

		public SessionDelegate(CoachingOverlay coachingOverlay)
		{
			this.coachingOverlay = coachingOverlay;
		}

		protected override void OnCoachingOverlayViewDidDeactivate(ARKitSessionSubsystem sessionSubsystem)
		{
			base.OnCoachingOverlayViewDidDeactivate(sessionSubsystem);

			sessionSubsystem.coachingActivatesAutomatically = false;
			coachingOverlay.PutOnPlane();
		}
	}
#endif
}