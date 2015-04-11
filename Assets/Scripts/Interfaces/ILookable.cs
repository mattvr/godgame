using UnityEngine;
using System.Collections;

public interface ILookable {

	float TimeToActivate { get; }
	void StartLooking (GameObject looker);
	void Looking (GameObject looker);
	void Activate (GameObject looker);
	void StopLooking (GameObject looker);

}