using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseSelectorView : MonoBehaviour
{
	[SerializeField] private List<Toggle> exerciseSelectingToggles = new List<Toggle>();
	[SerializeField] private List<Button> exerciseButtons = new List<Button>();

	public Action<int, bool> OnClickSelectingExerciseButton;

	private void Start()
	{
		for (int i = 0; i < exerciseSelectingToggles.Count; i++)
		{
			// This below local variable resolves problem with lambda expression, when pushing variable
			// from loop. Because, lambda expressions like below can't capture needing variables from loop
			// during executing loops (Example, counters from loop).
			int localIndex = i;
			exerciseButtons[localIndex].onClick.AddListener(() => OnExerciseButtonClicked(localIndex));
		}
	}

	private void OnExerciseButtonClicked(int exerciseIndex)
	{
		bool isThisExerciseActive = exerciseSelectingToggles[exerciseIndex].isOn;
		bool currentStateOfSelectingThisExercise;
        
		if (isThisExerciseActive)
        {
			currentStateOfSelectingThisExercise = false;
		} else
		{
			currentStateOfSelectingThisExercise = true;
		}

		exerciseSelectingToggles[exerciseIndex].isOn = currentStateOfSelectingThisExercise;
		OnClickSelectingExerciseButton?.Invoke(exerciseIndex, currentStateOfSelectingThisExercise);
	}

	public void ShowSelectedExercise(int exerciseIndex, bool isSelected) => exerciseSelectingToggles[exerciseIndex].isOn = isSelected;
}
