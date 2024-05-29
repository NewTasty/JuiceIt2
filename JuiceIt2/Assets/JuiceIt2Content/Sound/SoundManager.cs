using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    
    public List<CS_Sound> soundList = new List<CS_Sound>();
    public GameObject soundPrefab;
    private GameObject newObject;
    private AudioSource newSource;
    private CS_Sound newScriptable;

    public void SoundInstantiate(int Sound)
    {
        newObject = GameObject.Instantiate(soundPrefab);
        newScriptable = soundList[Sound];
        newSource = newObject.GetComponent<AudioSource>();
        newSource.clip = newScriptable.audioClip;
        newSource.volume = Random.Range(newScriptable.minVolume, newScriptable.maxVolume);
        newSource.pitch = Random.Range(newScriptable.minPitch, newScriptable.maxPitch);
        newSource.panStereo = newScriptable.panStereo;
        newSource.spatialBlend = newScriptable.spatialBlend;
        newSource.Play();
        GameObject.Destroy(newObject, newSource.clip.length+1);
    }
}
