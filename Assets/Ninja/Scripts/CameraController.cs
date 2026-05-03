using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Sequence Settings")]
    public CameraSequenceData sequenceData; 
    public List<GameObject> virtualCameras;

    private float timer = 0f;
    private int activeCameraIndex = 0; // Şu an açık olan kameranın sırası
    private int targetTimeIndex = 0;   // Scriptable Object'teki hangi süreyi bekliyoruz?

    void Start()
    {
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            virtualCameras[i].SetActive(i == 0);
        }
    }

    void Update()
    {
        if (sequenceData == null || virtualCameras.Count == 0 || sequenceData.activationTimes.Count == 0) return;

        timer += Time.deltaTime;

        if (timer >= sequenceData.activationTimes[targetTimeIndex])
        {
            virtualCameras[activeCameraIndex].SetActive(false);
            activeCameraIndex = (activeCameraIndex + 1) % virtualCameras.Count;
            virtualCameras[activeCameraIndex].SetActive(true);
            targetTimeIndex++;

            if (targetTimeIndex >= sequenceData.activationTimes.Count)
            {
                targetTimeIndex = 0;
                timer = 0f;
            }
        }
    }
}