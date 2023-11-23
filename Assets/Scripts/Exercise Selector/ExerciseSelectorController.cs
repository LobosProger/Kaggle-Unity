using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSelectorController : MonoBehaviour
{
    [SerializeField] private ExerciseSelectorModel exerciseSelectorModel;
    [SerializeField] private ExerciseSelectorView exerciseSelectorView;

	private void Start()
	{
		bool[] selectedExercises = exerciseSelectorModel.GetSelectedExercises();
		for(int i = 0; i < selectedExercises.Length; i++)
		{
			exerciseSelectorView.ShowSelectedExercise(i, selectedExercises[i]);
		}
	}

	private void OnEnable()
	{
		exerciseSelectorView.OnClickSelectingExerciseButton += OnSelectExerciseButton;
	}

	private void OnDisable()
	{
		exerciseSelectorView.OnClickSelectingExerciseButton -= OnSelectExerciseButton;
	}

	private void OnSelectExerciseButton(int exerciseIndex, bool isSelected)
	{
		exerciseSelectorModel.SetSelectedExercise(exerciseIndex, isSelected);
	}
}
