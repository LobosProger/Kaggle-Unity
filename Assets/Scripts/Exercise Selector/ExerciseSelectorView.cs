using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseSelectorView : MonoBehaviour
{
   [SerializeField] private List<Toggle> exerciseSelectingToggles = new List<Toggle>();
   [SerializeField] private List<Image> exerciseImages = new List<Image>();

	private void Start()
	{
		for(int i = 0; i< exerciseSelectingToggles.Count; i++)
		{
			// This below local variable resolves problem with lambda expression, when pushing variable
			// from loop. Because, lambda expressions like below can't capture needing variables from loop
			// during executing loops (Example, counters from loop).
			int localIndex = i; 
			exerciseSelectingToggles[localIndex].onValueChanged.AddListener((toggleState) => OnExerciseToggleClicked(localIndex, toggleState));
		}
	}

	private void OnExerciseToggleClicked(int exerciseIndex, bool isToggledOn)
	{
		exerciseImages[exerciseIndex].enabled = isToggledOn;
	}
}
