using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using MidiJack;

public class VideoMidi : MonoBehaviour
{

    VideoPlayer video;

    void Start()
    {
        video = GetComponent<VideoPlayer>();

        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.knobDelegate += Knob;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        if (note == 4) video.enabled = !video.enabled;
    }

    void Knob(MidiChannel channel, int knobNumber, float knobValue){
        if (video.enabled == true){
            Vector3 scale = transform.localScale;
            Vector3 pos = transform.position;
            switch (knobNumber)
            {
                case 21:
                    scale.x = knobValue * 5;
                    break;
                case 22:
                    scale.y = knobValue * 5;
                    break;
                case 23:
                    video.playbackSpeed = knobValue * 10;
                    break;
                case 41:
                    pos.x = knobValue * 5;
                    break;
                case 42:
                    pos.y = knobValue * 5;
                    break;
            }
            transform.localScale = scale;
            transform.position = pos;
        }
    }
}