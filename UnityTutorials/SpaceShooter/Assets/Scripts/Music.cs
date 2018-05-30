using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	void Awake()
    {
        GameObject music = GameObject.Find("Music");
        DontDestroyOnLoad(music);
    }
}
