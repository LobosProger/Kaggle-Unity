using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExerciseCompletionModel : MonoBehaviour
{
    [SerializeField] private ExerciseSelectorModel exerciseSelectorModel;

	public bool[] AllExercises => exerciseSelectorModel.GetSelectedExercises();
	
	private float timeOfCompletingEachExercise => (timeForCompletingExercises / AllExercises.Where(selected => selected == true).Count());
	private float timeForCompletingExercises;

	public void AddMinuteToTime()
	{
		timeForCompletingExercises += 60;
	}

	public void RemoveMinuteToTime()
	{
		if(timeForCompletingExercises - 60 >= 0)
		{
			timeForCompletingExercises -= 60;
		}
	}

	public float GetTimeOfCompletingCurrentExercise() => timeOfCompletingEachExercise;

	public float GetTimeOfCompletionInSeconds() => timeForCompletingExercises;
}
