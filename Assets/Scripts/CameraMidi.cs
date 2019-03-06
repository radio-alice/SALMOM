using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;
using MidiJack;

public class CameraMidi : MonoBehaviour
{
    AnalogGlitch analog;
    Binary binary;
    DigitalGlitch digital;
    Mirror mirror;
 
    List<MonoBehaviour> fx;
    int currentNote;

    void Start()
    {
        analog = GetComponent<AnalogGlitch>();
        binary = GetComponent<Binary>();
        digital = GetComponent<DigitalGlitch>();        
        mirror = GetComponent<Mirror>();

        fx = new List<MonoBehaviour>() 
            { binary, analog, mirror, digital };

        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        Screen.fullScreen = true;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        if (fx[note] != null) fx[note].enabled = true;
        currentNote = note;
    }

    void NoteOff(MidiChannel channel, int note)
    {
        if (fx[note] != null) fx[note].enabled = false;
    }
}