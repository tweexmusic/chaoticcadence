using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class MidiManagerBeat : MonoBehaviour
{
    public static MidiManagerBeat instance;

    // Delegate for NoteChange event.
    public delegate void BeatChange(int currentBeatValue, string currentBeatName);

    /// <summary>
    /// Event used to tell other classes when beat in music has changed.
    /// Takes int and string parameters from delegate.
    /// </summary>
    public static event BeatChange OnBeatChange;

    public FMODUnity.StudioEventEmitter musicEvent;
    private int previousEventTimelinePosition;

    // Data type from DryWetMidi class library. Used to store raw data from midi file.
    private static MidiFile midiFile;

    // String path for midi file in editor.
    public string midiFileLocation;

    // Stores the individual notes in their own array index.
    private Note[] noteArray;

    // Counter used to track what note name or note timestamp is currently being referenced.
    private int indexCounter = 0;

    // Stores the start time of each note in milliseconds.
    private List<int> timeStamps = new List<int>();

    // List that holds all 12 chromatic note possibilities. The index of a note is the value assigned to the currentNote variable.
    private List<string> beatList = new List<string>(new string[]
        {"C#", "D", "D#", "E", "F" });

    // Value that is used to set FMOD parameter.
    private int currentBeat;

    public float musicTempo;


    void Awake()
    {
        instance = this;
        SetInitMidiFileData();
    }

    void Update()
    {
        CheckForBeatChange();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    /// <summary>
    /// Function that is called in Start() to get all midi data set and ready for music playback.
    /// </summary>
    private void SetInitMidiFileData()
    {
        ReadFromFile();
        GetDataFromMidi();
        SetTimeStamps(noteArray);
    }


    /// <summary>
    /// Reads the midi file and stores data as type MidiFile.
    /// </summary>
    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + midiFileLocation);
    }


    /// <summary>
    /// Takes data from type MidiFile and copies the note data to array.
    /// </summary>
    private void GetDataFromMidi()
    {
        var midiNotes = midiFile.GetNotes();
        noteArray = new Note[midiNotes.Count];
        midiNotes.CopyTo(noteArray, 0);
    }


    /// <summary>
    /// Takes in note array as parameter and converts the timing of the notes to milleseconds.
    /// Specifically used to keep track of when a note begins playing.
    /// </summary>
    /// <param name="noteArray"></param>
    private void SetTimeStamps(Note[] noteArray)
    {
        foreach (var note in noteArray)
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midiFile.GetTempoMap());
            timeStamps.Add((metricTimeSpan.Minutes * 60 + metricTimeSpan.Seconds) * 1000 + metricTimeSpan.Milliseconds);
        }
    }


    /// <summary>
    /// Triggers the OnNoteChange event and sets the currentNote value at the time the note plays.
    /// if/else handles logic for looping music.
    /// </summary>
    public void CheckForBeatChange()
    {
        if (GetEventTimelinePosition > previousEventTimelinePosition && indexCounter < timeStamps.Count)
        {
            if (timeStamps[indexCounter] <= GetEventTimelinePosition)
            {
                SetBeatValue();
                OnBeatChange?.Invoke(currentBeat, beatList[currentBeat]);
                indexCounter++;
            }
        }
        else
        {
            indexCounter = 0;
            foreach (var noteTimeStamp in timeStamps)
            {
                if (GetEventTimelinePosition >= noteTimeStamp)
                {
                    indexCounter++;
                }
                else
                {
                    break;
                }
            }
        }

        previousEventTimelinePosition = GetEventTimelinePosition;
    }


    /// <summary>
    /// Sets the value of currentNote so that FMOD parameter value can be set and play correct tone.
    /// </summary>
    public void SetBeatValue()
    {
        string currentBeatString = noteArray[indexCounter].ToString();
        currentBeatString = currentBeatString.Remove(currentBeatString.Length - 1);
        currentBeat = beatList.IndexOf(currentBeatString);
    }


    /// <summary>
    /// Property returns current FMOD event timeline position in milliseconds.
    /// </summary>
    private int GetEventTimelinePosition
    {
        get
        {
            musicEvent.EventInstance.getTimelinePosition(out int position);
            return position;
        }
    }
}