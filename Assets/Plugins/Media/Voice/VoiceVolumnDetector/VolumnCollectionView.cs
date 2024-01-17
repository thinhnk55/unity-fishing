using Framework;
using System.Collections.Generic;
using UnityEngine;

public class VolumnCollectionView : CollectionViewBase<VolumnInfo>
{
    [SerializeField] VoiceRecorder VoiceRecorder;
    [SerializeField] float mul;
    private void Start()
    {
        VoiceRecorder.OnVolumnDetect += OnVolumnDetect;
        VoiceRecorder.IsRecording.OnDataChanged += OnIsRecordingChange;
    }
    private void OnIsRecordingChange(bool isRecording)
    {
        if (isRecording)
            BuildView();
    }
    private void OnVolumnDetect(float volumn, int sample)
    {
        if (sample / 1024  > cards.Count)
        {
            AddCard(new VolumnInfo() { Volumn = volumn * mul });
        }
    }

    public override void BuildView()
    {
        BuildView(new List<VolumnInfo>());
    }
}
