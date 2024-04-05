using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObjects;
    [SerializeField] private float spawnDonutSize = 10.0f;
    [SerializeField] private float spawnRate = 0.25f;
    [SerializeField] private float spawnRateIncrease = 0.005f;
    private float waited = 0.0f;

    private Vector2 GetSpawnPos()
    {
        return Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector2.right * spawnDonutSize + transform.position;
    }
    private void Update()
    {
        spawnRate += spawnRateIncrease * Time.deltaTime;
        waited += Time.deltaTime;
        if (waited < 1.0f / spawnRate) return;
        waited = 0.0f;
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], GetSpawnPos(), Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnDonutSize);
    }
}
