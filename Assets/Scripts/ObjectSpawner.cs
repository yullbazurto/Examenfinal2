using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] fallingObjects; // Array de prefabs de objetos que caerán
    public float spawnIntervalMin = 1f; // Tiempo mínimo entre spawns
    public float spawnIntervalMax = 2f; // Tiempo máximo entre spawns
    public float spawnRangeX = 8f; // Rango horizontal del spawn

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            // Elegir un prefab aleatorio y una posición aleatoria
            GameObject selectedObject = fallingObjects[Random.Range(0, fallingObjects.Length)];
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

            // Instanciar el objeto
            GameObject obj = Instantiate(selectedObject, spawnPosition, Quaternion.identity);

            // Destruir el objeto después de 5 segundos
            Destroy(obj, 5f);

            // Esperar un tiempo aleatorio antes del siguiente spawn
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
