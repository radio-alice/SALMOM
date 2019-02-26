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
    Feedback feedback;
    Datamosh datamosh;
    Mirror mirror;
    Slitscan slitscan;
    Voronoi voronoi;
    List<MonoBehaviour> fx;

    void Start()
    {
        analog = GetComponent<AnalogGlitch>();
        binary = GetComponent<Binary>();
        digital = GetComponent<DigitalGlitch>();
        feedback = GetComponent<Feedback>();
        datamosh = GetComponent<Datamosh>();
        mirror = GetComponent<Mirror>();
        slitscan = GetComponent<Slitscan>();
        voronoi = GetComponent<Voronoi>();
        fx = new List<MonoBehaviour>() 
            { binary, analog, feedback, datamosh, mirror, slitscan, voronoi, digital };

        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.knobDelegate += Knob;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        fx[note].enabled = true;
    }

    void NoteOff(MidiChannel channel, int note)
    {
        fx[note].enabled = false;
    }

    void Knob(MidiChannel channel, int knobNumber, float knobValue)
    {
        Debug.Log("Knob: " + knobNumber + "," + knobValue);
    }
}
