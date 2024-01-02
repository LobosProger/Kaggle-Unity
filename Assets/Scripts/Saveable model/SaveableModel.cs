using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableModel : MonoBehaviour
{
    protected virtual void Awake()
    {
        ReadDataFromMemory();
	}

	private void OnApplicationQuit()
	{
		SaveDataInMemory();
	}

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
			SaveDataInMemory();
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			SaveDataInMemory();
		}
	}

	protected abstract void ReadDataFromMemory();

    protected abstract void SaveDataInMemory();
}
