using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSelectorModel : MonoBehaviour
{
	private const string keyToAccessSelectedExercises = "Selected exercises";
	private const int maxAmountOfExercises = 6;
	private bool[] selectedExerciseToComplete = new bool[maxAmountOfExercises];

	private void Awake()
	{
		// When user opened application once and nothing was stored in memory,
		// make all exercises selected on default
		MakeAllExercisesSelectedOnDefaultState();
		LoadSelectedExercisesFromMemory();
	}

	private void LoadSelectedExercisesFromMemory()
	{
		string json = PlayerPrefs.GetString(keyToAccessSelectedExercises);

		if (json != "" && json != "[]")
		{
			selectedExerciseToComplete = JsonConvert.DeserializeObject<bool[]>(json);
		}
	}

	private void SaveSelectedExercisesInMemory()
	{
		string json = JsonConvert.SerializeObject(selectedExerciseToComplete);
		PlayerPrefs.SetString(keyToAccessSelectedExercises, json);
	}

	private void MakeAllExercisesSelectedOnDefaultState()
	{
		for (int i = 0; i < maxAmountOfExercises; i++)
		{
			selectedExerciseToComplete[i] = true;
		}
	}

	private void OnApplicationQuit()
	{
		SaveSelectedExercisesInMemory();
	}

	public bool[] GetSelectedExercises()
	{
		return selectedExerciseToComplete;
	}

	public void SetSelectedExercise(int exerciseIndex, bool isSelected)
	{
		selectedExerciseToComplete[exerciseIndex] = isSelected;
	}
}