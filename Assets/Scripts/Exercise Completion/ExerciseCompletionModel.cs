using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExerciseCompletionModel : MonoBehaviour
{
    [SerializeField] private ExerciseSelectorModel exerciseSelectorModel;

	private const float initialTimeForCompletingAllExercises = 600;

	public bool[] GetAllExercises()
	{
		bool[] selectedExercises = exerciseSelectorModel.GetAllExercises();
		return selectedExercises;
	}

	public int GetAmountOfSelectedExercises()
	{
		int amountOfSelectedExercises = GetAllExercises().Where(selectedExercise => selectedExercise == true).Count();
		return amountOfSelectedExercises;
	}

	public float GetTimeOfCompletingCurrentExercise()
	{
		float timeOfCompletingEachExercise = initialTimeForCompletingAllExercises / GetAllExercises().Where(selected => selected == true).Count();
		return timeOfCompletingEachExercise;
	}

	public float GetTimeOfCompletionAllExercisesInSeconds() => initialTimeForCompletingAllExercises;
}