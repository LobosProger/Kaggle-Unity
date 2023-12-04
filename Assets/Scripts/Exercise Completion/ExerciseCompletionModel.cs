using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExerciseCompletionModel : MonoBehaviour
{
    [SerializeField] private ExerciseSelectorModel exerciseSelectorModel;

	public bool[] AllExercises => exerciseSelectorModel.GetSelectedExercises();
	public int AmountSelectedExercises => AllExercises.Where(selectedExercise => selectedExercise == true).Count();

	private float timeOfCompletingEachExercise => (timeForCompletingExercises / AllExercises.Where(selected => selected == true).Count());
	private float timeForCompletingExercises = 600;

	public void AddMinuteToTime()
	{
		if(timeForCompletingExercises + 60 <= 600)
		{
			timeForCompletingExercises += 60;
		}
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
