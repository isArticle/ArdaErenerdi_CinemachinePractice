using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCameraSequence", menuName = "Cinemachine/Camera Sequence Data")]
public class CameraSequenceData : ScriptableObject
{
    [Tooltip("List of times (in seconds) to switch to the next camera.")]
    public List<float> activationTimes;
}