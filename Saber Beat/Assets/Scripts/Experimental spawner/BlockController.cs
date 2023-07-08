using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour
{
    public float speed;
    public GameObject ArrowMesh;
    public GameObject DotMesh;

    public bool IsBomb;
    private bool fastspeed;
    public void GetBlockRotation(int cutDirection)
    {
        if(!IsBomb){
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
                case 8:
                    ArrowMesh.SetActive(false);
                    DotMesh.SetActive(true);
                    rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                default:
                    Debug.LogWarning("Unknown cut direction: " + cutDirection);
                    rotation = Quaternion.identity;
                    break;
            }
            transform.rotation = rotation;
        }
    }
    void Update(){
        if(fastspeed){
            transform.Translate(Vector3.right * speed * 2 * Time.deltaTime);
        }
        else{
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
    void Start(){
        StartCoroutine(SpeedConstroller());
    }

    IEnumerator SpeedConstroller(){
        fastspeed = true;
        yield return new WaitForSeconds(1f);
        fastspeed = false;
    }
}