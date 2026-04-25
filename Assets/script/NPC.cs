using UnityEngine;

public class NPC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter detectado con: " + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Es el Player!");
            SpriteRenderer playerSprite = other.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                playerSprite.color = Color.red;
                Debug.Log("Color cambiado a rojo");
            }
            else
            {
                Debug.LogWarning("No se encontró SpriteRenderer en el Player");
            }
        }
        else
        {
            Debug.Log("No tiene el tag Player. Tag actual: " + other.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger Exit detectado con: " + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            SpriteRenderer playerSprite = other.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                playerSprite.color = Color.white;
                Debug.Log("Color restaurado a blanco");
            }
        }
    }
}