using UnityEngine;
using UnityEngine.Animations;

public class MoveCamera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = player.transform.position;
    }
}