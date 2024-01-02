using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseSelectorModel : SaveableModel
{
	private const string keyToAccessSelectedExercises = "Selected exercises";
	private const int maxAmountOfExercises = 6;
	private bool[] selectedExerciseToComplete = new bool[maxAmountOfExercises];

	protected override void Awake()
	{
		MakeAllExercisesSelectedOnDefaultState();
		base.Awake();
	}

	private void MakeAllExercisesSelectedOnDefaultState()
	{
		for (int i = 0; i < maxAmountOfExercises; i++)
		{
			selectedExerciseToComplete[i] = true;
		}
	}

	protected override void ReadDataFromMemory()
	{
		string json = PlayerPrefs.GetString(keyToAccessSelectedExercises);

		if (json != "" && json != "[]")
		{
			selectedExerciseToComplete = JsonConvert.DeserializeObject<bool[]>(json);
		}
	}

	protected override void SaveDataInMemory()
	{
		string json = JsonConvert.SerializeObject(selectedExerciseToComplete);
		PlayerPrefs.SetString(keyToAccessSelectedExercises, json);
	}

	public bool[] GetAllExercises() => selectedExerciseToComplete;

	public void SetSelectedExercise(int exerciseIndex, bool isSelected) => selectedExerciseToComplete[exerciseIndex] = isSelected;
}