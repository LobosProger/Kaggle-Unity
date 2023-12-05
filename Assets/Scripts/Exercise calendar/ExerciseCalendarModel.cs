using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class ExerciseCalendarModel : MonoBehaviour
{
	private const string keyToAccessCompletedExercises = "Completed exercises";
	private HashSet<DateTime> completedDatesByExercise = new HashSet<DateTime>();

	private void Awake()
	{
		LoadCompletedExercisesFromMemory();
	}

	public HashSet<DateTime> GetCompletedExercisesByDate()
	{
		if(completedDatesByExercise.Count == 0)
		{
			LoadCompletedExercisesFromMemory();
		}

		return completedDatesByExercise;
	}

	public void AddCompletedExerciseDate(DateTime dateTime)
	{
		completedDatesByExercise.Add(dateTime);
	}

	private void LoadCompletedExercisesFromMemory()
	{
		string json = PlayerPrefs.GetString(keyToAccessCompletedExercises);
		
		if(json != "")
		{
			completedDatesByExercise = JsonConvert.DeserializeObject<HashSet<DateTime>>(json);
		}
	}

	private void SaveCompletedExercisesInMemory()
	{
		string json = JsonConvert.SerializeObject(completedDatesByExercise);
		PlayerPrefs.SetString(keyToAccessCompletedExercises, json);
	}

	private void OnApplicationQuit()
	{
		SaveCompletedExercisesInMemory();
	}

	private void OnApplicationFocus(bool focus)
	{
		if (focus)
			SaveCompletedExercisesInMemory();
	}

	private void OnApplicationPause(bool pause)
	{
		if(!pause)
			SaveCompletedExercisesInMemory();
	}
}
