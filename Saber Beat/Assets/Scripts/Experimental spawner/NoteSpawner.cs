using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public TextAsset beatMapJson;
    public AudioSource source;
    public AudioClip Song;
    public GameObject redBlockPrefab;
    public GameObject blueBlockPrefab;
    public GameObject bombPrefab;
    public float spacingX = 2.0f;
    public float offsetX = 3.0f;
    public float spacingY = 2.0f;
    public Transform offsetTransform;

    private BeatSaberMapData mapData;
    private float noteSpeed;

    void Start(){
        noteSpeed = GetBPM(Song);
        mapData = JsonUtility.FromJson<BeatSaberMapData>(beatMapJson.text);
        source.clip = Song;
    }
    public void Play()
    {
        StartCoroutine(PlayAudio());
        foreach (BeatSaberBlockData blockData in mapData._notes)
        {
            float spawnTime = blockData._time;
            StartCoroutine(SpawnBlockRoutine(blockData, spawnTime));
        }
    }

    IEnumerator PlayAudio(){
        float offset = transform.position.x - offsetTransform.position.x;
        float speed = offset / noteSpeed;
        yield return new WaitForSeconds(speed);
        source.Play();
    }

    private float GetBPM(AudioClip audioClip)
{
    float baseNoteSpeed = 10f; // Base note speed (adjust as needed)
    float baseBPM = 120f; // Base BPM used for calibration
    float noteSpeed = baseNoteSpeed;
    float bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip);
    noteSpeed = baseNoteSpeed * (bpm / baseBPM);
    return noteSpeed;
}

    private IEnumerator SpawnBlockRoutine(BeatSaberBlockData blockData, float spawnTime)
    {
        float startTime = Time.realtimeSinceStartup;
        float elapsedTime = 0f;

        while (elapsedTime < spawnTime)
        {
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        SpawnBlock(blockData);
    }

    private void SpawnBlock(BeatSaberBlockData blockData)
    {
        GameObject blockPrefab;
        switch (blockData._type)
        {
            case 0:
                blockPrefab = redBlockPrefab;
                break;
            case 1:
                blockPrefab = blueBlockPrefab;
                break;
            case 3:
                blockPrefab = bombPrefab;
                break;
            default:
                Debug.LogWarning($"Invalid block type: {blockData._type}");
                return;
        }

        float xPosition = blockData._lineIndex * spacingX - offsetX;
        float yPosition = blockData._lineLayer * spacingY;
        Vector3 blockPosition = new Vector3(transform.position.x, yPosition + transform.position.y, xPosition + transform.position.z);

        GameObject spawnedBlock = Instantiate(blockPrefab, blockPosition, Quaternion.identity, transform);

        BlockController blockController = spawnedBlock.GetComponent<BlockController>();
        if (blockController != null)
        {
            blockController.GetBlockRotation(blockData._cutDirection);
            blockController.speed = noteSpeed;
        }
    }
    [System.Serializable]
    public class BeatSaberMapData{
        public List<BeatSaberBlockData> _notes;
        public List<WallData> _obstacles;
    }
    [System.Serializable]
    public class BeatSaberBlockData
    {
        public float _time;
        public int _lineIndex;
        public float _lineLayer;
        public float _type;
        public int _cutDirection;
    }
    [System.Serializable]
    public class WallData
    {
        public float _time;
        public int _lineIndex;
        public float _duration;
        public float _width;
    }
    [System.Serializable]
    public class CustomData{

    }
}