using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatSaberBlockSpawner : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public TextAsset beatmapDataFile;
    public GameObject redBlockPrefab;
    public GameObject blueBlockPrefab;
    public GameObject bombBlockPrefab;
    public GameObject dotBlockRedPrefab;
    public GameObject dotBlockBluePrefab;
    public GameObject wallPrefab;

    public Transform offsetTransform;

    public BeatmapData beatmapData;
    private float audioStartTime;
    public float PreNoteSpeed;

    private void Start()
    {
        if (beatmapDataFile != null)
        {
            beatmapData = JsonUtility.FromJson<BeatmapData>(beatmapDataFile.text);
        }

        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        PreNoteSpeed = GetBPM(audioClip);
    }

    public void Play()
    {
        if (beatmapData == null || audioSource == null)
        {
            Debug.LogError("Beatmap data or audio source is not set!");
            return;
        }
        StartCoroutine(SpawnBlocksAndWalls());
        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio(){
        float offset = transform.position.x - offsetTransform.position.x;
        float speed = offset / PreNoteSpeed;
        yield return new WaitForSeconds(speed);
        audioSource.Play();
    }

    void Update()
    {
        audioStartTime = audioSource.time;
    }

    private IEnumerator SpawnBlocksAndWalls()
    {
        foreach (var blockData in beatmapData._notes)
        {
            yield return new WaitForSeconds(blockData._time);

            if (blockData._type == 0 && blockData._cutDirection != 8) // Red Block
            {
                SpawnColorBlock(redBlockPrefab, blockData._cutDirection, blockData._lineIndex, blockData._lineLayer);
            }
            else if (blockData._type == 1 && blockData._cutDirection != 8) // Blue Block
            {
                SpawnColorBlock(blueBlockPrefab, blockData._cutDirection, blockData._lineIndex, blockData._lineLayer);
            }
            else if (blockData._type == 3 && blockData._cutDirection != 8) // Bomb Block
            {
                SpawnBombBlock(blockData._lineIndex, blockData._lineLayer);
            }
            else if (blockData._cutDirection == 8) // Dot Block
            {
                if (blockData._type == 0) // Red Dot Block
                {
                    SpawnDotBlock(dotBlockRedPrefab, blockData._lineIndex, blockData._lineLayer);
                }
                else if (blockData._type == 1) // Blue Dot Block
                {
                    SpawnDotBlock(dotBlockBluePrefab, blockData._lineIndex, blockData._lineLayer);
                }
                else
                {
                    Debug.LogWarning("Unknown dot block color: " + blockData._type);
                }
            }
            else
            {
                Debug.LogWarning("Unknown block type: " + blockData._type);
            }
        }

        foreach (var wallData in beatmapData._obstacles)
        {
            yield return new WaitForSeconds(wallData._time);
            SpawnWall(wallData._lineIndex, wallData._duration, wallData._width);
        }
    }

    private float GetBPM(AudioClip audioClip)
    {
        float bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip);
        float baseNoteSpeed = 5f; // Base note speed (adjust as needed)
        float baseBPM = 120f; // Base BPM used for calibration
        float noteSpeed = baseNoteSpeed * (bpm / baseBPM);
        return noteSpeed;
    }

    private void SpawnColorBlock(GameObject blockPrefab, int cutDirection, int lineIndex, int lineLayer)
    {
        Quaternion rotation = GetBlockRotation(cutDirection);
        Vector3 position = GetBlockPosition(lineIndex, lineLayer);

        GameObject spawnedBlock = Instantiate(blockPrefab, position, rotation);
        spawnedBlock.AddComponent<NoteMovement>();
        NoteMovement noteMovement = spawnedBlock.GetComponent<NoteMovement>();
        float noteSpeed = PreNoteSpeed;
        noteMovement.SetSpeed(noteSpeed);
    }

    private void SpawnDotBlock(GameObject dotBlockPrefab, int lineIndex, int lineLayer)
    {
        Vector3 position = GetBlockPosition(lineIndex, lineLayer);

        GameObject spawnedBlock = Instantiate(dotBlockPrefab, position, Quaternion.identity);
        spawnedBlock.AddComponent<NoteMovement>();
        NoteMovement noteMovement = spawnedBlock.GetComponent<NoteMovement>();
        float noteSpeed = PreNoteSpeed;
        noteMovement.SetSpeed(noteSpeed);
    }

    private void SpawnBombBlock(int lineIndex, int lineLayer)
    {
        Vector3 position = GetBlockPosition(lineIndex, lineLayer);

        GameObject spawnedBlock = Instantiate(bombBlockPrefab, position, Quaternion.identity);
        spawnedBlock.AddComponent<NoteMovement>();
        NoteMovement noteMovement = spawnedBlock.GetComponent<NoteMovement>();
        float noteSpeed = PreNoteSpeed;
        noteMovement.SetSpeed(noteSpeed);
    }

    private void SpawnWall(int lineIndex, float duration, float width)
    {
        Vector3 position = GetWallPosition(lineIndex);
        Vector3 scale = new Vector3(width, 1f, duration);

        GameObject spawnedWall = Instantiate(wallPrefab, position, Quaternion.identity);
        spawnedWall.transform.localScale = scale;
        spawnedWall.AddComponent<NoteMovement>();
        NoteMovement noteMovement = spawnedWall.GetComponent<NoteMovement>();
        float noteSpeed = PreNoteSpeed;
        noteMovement.SetSpeed(noteSpeed);
    }

    private Quaternion GetBlockRotation(int cutDirection)
    {
        Quaternion rotation;
        switch (cutDirection)
        {
            case 0: // Up
                rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 1: // Down
                rotation = Quaternion.Euler(180f, 0f, 0f);
                break;
            case 2: // Left
                rotation = Quaternion.Euler(90f, 0f, 0f);
                break;
            case 3: // Right
                rotation = Quaternion.Euler(270f, 0f, 0f);
                break;
            case 4: // Up-Left
                rotation = Quaternion.Euler(45f, 0f, 0f);
                break;
            case 5: // Up-Right
                rotation = Quaternion.Euler(315f, 0f, 0f);
                break;
            case 6: // Down-Left
                rotation = Quaternion.Euler(135f, 0f, 0f);
                break;
            case 7: // Down-Right
                rotation = Quaternion.Euler(225f, 0f, 0f);
                break;
            default:
                Debug.LogWarning("Unknown cut direction: " + cutDirection);
                rotation = Quaternion.identity;
                break;
        }

        return rotation;
    }

    private Vector3 GetBlockPosition(int lineIndex, int lineLayer)
    {
        float positionX = lineIndex;
        float positionY = lineLayer;
        Vector3 position = transform.position + new Vector3(0f, positionY, positionX);
        return position;
    }

    private Vector3 GetWallPosition(int lineIndex)
    {
        float posX = lineIndex;
        Vector3 position = transform.position + new Vector3(posX, 0f, 0f);
        return position;
    }
}

[System.Serializable]
public class BeatmapData
{
    public List<BlockData> _notes;
    public List<WallData> _obstacles;
}

[System.Serializable]
public class BlockData
{
    public float _time;
    public int _lineIndex;
    public int _lineLayer;
    public int _type; // 0: Red, 1: Blue, 3: Bomb
    public int _cutDirection; // 0-7: Different directions, 8: Dot
}

[System.Serializable]
public class WallData
{
    public float _time;
    public int _lineIndex;
    public float _duration;
    public float _width;
}
