using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using MidiJack;

public class VideoMidi : MonoBehaviour
{
    public int setNote;

    public GameObject[] videos;

    void Start()
    {
        MidiMaster.noteOnDelegate += NoteOn;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        if (note >= setNote)
        {
            GameObject video = videos[note - setNote];
            video.SetActive(!video.activeInHierarchy);
        }
    }
}