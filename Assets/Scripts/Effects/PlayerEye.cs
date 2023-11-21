using System.Collections;
using UnityEngine;

	public class PlayerEye : MonoBehaviour
	{

		[SerializeField]
		private Transform pupil = default;
		[SerializeField]
		private float pupilDistanceFromCenterEye = default;

		[SerializeField]
		private float randomMovementIntervalMin = default;
		[SerializeField]
		private float randomMovementIntervalMax = default;

		private Vector2 pupilVelocity;
		private Vector2 pupilTarget;

	

		private IEnumerator Start()
		{
			// Move eye target randomly as well
			while (true)
			{
				yield return new WaitForSeconds(Random.Range(randomMovementIntervalMin, randomMovementIntervalMax));
				pupilTarget = Random.insideUnitCircle * pupilDistanceFromCenterEye;
			}
		}

		private void Update()
		{
			pupil.localPosition = Vector2.SmoothDamp(pupil.localPosition, pupilTarget, ref pupilVelocity, 0.2f);
		}
	}
