using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayableManager : MonoBehaviour
{
    public List<TimelineAsset> timelines;
    public List<SceneStateAssociations> SceneStateAssociations;
    public int currentIndex;
    public TimelineAsset currentClip
    {
        get
        {
            return timelines[currentIndex];
        }
    }
    public PlayableDirector director;
    public float[] speeds = { 1, 2, 4, 8};
    private int speedIndex;
    public Text speedLabel;
    public static PlayableManager Instance;
    public SceneState sceneState;
    public TimelineAsset startTimeline;
    public TimelineAsset currentTimeline;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlaySequence(startTimeline);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySequence(string sequenceName)
    {
        var sequence = timelines.Find(t => t.name == sequenceName);
        currentIndex = timelines.IndexOf(sequence);
        PlaySequence(sequence);
    }

    public void PlayNext()
    {
        currentIndex = timelines.IndexOf(director.playableAsset as TimelineAsset);
        currentIndex++;
        if (currentIndex >= timelines.Count)
        {
            currentIndex = 0;
        }
        PlaySequence(timelines[currentIndex]);
    }


    public void PlayPreview()
    {
        currentIndex = timelines.IndexOf(director.playableAsset as TimelineAsset);
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = timelines.Count - 1;
        }
        PlaySequence(timelines[currentIndex]);
    }

    public void RestartSequence()
    {
        EndCurrent();
        director.playableAsset = currentTimeline;
        director.time = 0;
        PlaySequence(currentTimeline);
    }

    public void PlaySequence(TimelineAsset sequence)
    {
        currentTimeline = sequence;
        var association = SceneStateAssociations.Find(s => s.timelineAsset == sequence);
        sceneState = (association == null) ? SceneState.Unknown : association.sceneState;
        EndCurrent();
        director.playableAsset = sequence;
        director.time = 0;
        director.Play();
    }

    void EndCurrent()
    {
        director.time = currentClip.duration;
        director.Evaluate();
    }

    public void ToggleSpeed()
    {
        speedIndex++;
        if (speedIndex >= speeds.Length)
        {
            speedIndex = 0;
        }
        Time.timeScale = speeds[speedIndex];
        speedLabel.text = "x"+speeds[speedIndex];
    }

    public enum SceneState
    {
        SessionStarted,
        LinaIntro,
        TakeUmbrella,
        Pfalh,
        MakingOfGiessen,
        CityComesAlive,
        Volksbad,
        Kirchplatz,
        Garden,
        Unknown,
    }
}

[Serializable]
public class SceneStateAssociations
{
    public TimelineAsset timelineAsset;
    public PlayableManager.SceneState sceneState; 
}
