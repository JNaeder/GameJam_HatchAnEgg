using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class MusicScript : MonoBehaviour {

    [FMODUnity.EventRef]
    public string music;

    FMOD.Studio.EventInstance musicInst;


    public static MusicScript musicScript;

    private void Awake()
    {
        if (musicScript == null)
        {
            musicScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        
       

        DontDestroyOnLoad(gameObject);


        musicInst = FMODUnity.RuntimeManager.CreateInstance(music);
        musicInst.start();
	}
}
