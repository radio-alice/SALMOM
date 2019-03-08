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
        MidiMaster.knobDelegate += Knob;
        Screen.fullScreen = true;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        if (note < 4) fx[note].enabled = true;
    }

    void NoteOff(MidiChannel channel, int note)
    {
        if (note < 4) fx[note].enabled = false;
    }
    void Knob(MidiChannel channel, int knobNumber, float knobValue)
    {
        if (mirror.enabled)
        {
            switch(knobNumber)
            {
                case 21:
                    mirror._repeat = (int)(knobValue * 30);
                    break;
                case 22:
                    mirror._roll = (int)(knobValue * 100);
                    break;
                case 23:
                    mirror._offset = (int)(knobValue * 100);
                    break;
                case 24:
                    mirror._symmetry = knobValue < 0.5;
                    break;
            }

        }
        if (analog.enabled){
            switch(knobNumber)
            {
                case 21:
                    analog.verticalJump = knobValue;
                    break;
                case 22:
                    analog.scanLineJitter = knobValue;
                    break;
                case 23:
                    analog.colorDrift = knobValue;
                    break;
                case 24:
                    analog.horizontalShake = knobValue;
                    break;
            }
        }
        if (digital.enabled)
        {
            switch (knobNumber)
            {
                case 21:
                    digital.intensity = knobValue;
                    break;
            }
        }
        if(binary.enabled)
        {
            float h0, s0, v0;
            float h1, s1, v1;

            Color.RGBToHSV(binary.color0, out h0, out s0, out v0);
            Color.RGBToHSV(binary.color1, out h1, out s1, out v1);

            switch (knobNumber)
            {
                case 21:
                    binary.Opacity = knobValue;
                    break;
                case 22:
                    binary.ditherScale = (int)(knobValue*8);
                    break;
                case 41:
                    h0 = knobValue;
                    break;
                case 42:
                    s0 = knobValue;
                    break;
                case 43:
                    v0 = knobValue;
                    break;                
                case 45:
                    h1 = knobValue;
                    break;
                case 46:
                    s1 = knobValue;
                    break;
                case 47:
                    v1 = knobValue;
                    break;
                
            }

            binary.color0 = Color.HSVToRGB(h0, s0, v0);
            binary.color1 = Color.HSVToRGB(h1, s1, v1);
        }
    }
}