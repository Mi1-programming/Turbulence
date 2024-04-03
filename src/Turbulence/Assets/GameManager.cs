using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameObject _player;

    private void Start()
    {
        _player = player;
    }
}
