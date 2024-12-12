using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del jugador
    public Sprite[] rightSprites; // Array de sprites para movimiento hacia la derecha
    public Sprite[] leftSprites; // Array de sprites para movimiento hacia la izquierda
    private SpriteRenderer mySpriteRenderer; // Componente SpriteRenderer
    private int index = 0; // Índice para los sprites

    private Vector2 movement;
    private Sprite[] currentSprites; // Array de sprites activo

    public int maxHits = 3; // Golpes máximos permitidos antes de "morir"
    private int currentHits = 0; // Contador de golpes actuales

    private bool isDead = false; // Estado del jugador

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        if ((rightSprites == null || rightSprites.Length == 0) || (leftSprites == null || leftSprites.Length == 0))
        {
            Debug.LogError("Sprites no asignados. Por favor, asigna los sprites en el Inspector.");
        }
        currentSprites = rightSprites; // Por defecto, usar los sprites de la derecha
        StartCoroutine(WalkCoroutine()); // Iniciar la animación de caminar
    }

    void Update()
    {
        if (isDead) return; // Detener el movimiento si el jugador está muerto

        // Detectar las teclas A y D
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 para izquierda, 1 para derecha

        // Establecer el movimiento
        movement = new Vector2(horizontalInput, 0);

        // Aplicar movimiento
        transform.Translate(movement * speed * Time.deltaTime);

        // Cambiar los sprites según la dirección del movimiento
        if (horizontalInput > 0) // Movimiento hacia la derecha
        {
            currentSprites = rightSprites;
        }
        else if (horizontalInput < 0) // Movimiento hacia la izquierda
        {
            currentSprites = leftSprites;
        }
    }

    IEnumerator WalkCoroutine()
    {
        while (true)
        {
            if (movement.x != 0) // Cambiar sprite solo si hay movimiento
            {
                mySpriteRenderer.sprite = currentSprites[index];
                index = (index + 1) % currentSprites.Length; // Ciclar entre los sprites
            }
            yield return new WaitForSeconds(0.1f); // Controlar la velocidad de animación
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FallingObject")) // Verificar si el objeto es "FallingObject"
        {
            currentHits++; // Incrementar contador de golpes
            Debug.Log("Golpes recibidos: " + currentHits);

            if (currentHits >= maxHits)
            {
                PlayerDeath();
            }
        }
    }

    void PlayerDeath()
    {
        isDead = true;
        Debug.Log("¡Jugador muerto!");
        // Aquí puedes agregar lógica adicional, como reiniciar el nivel o mostrar una pantalla de Game Over
    }
}
