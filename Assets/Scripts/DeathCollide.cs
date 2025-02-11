using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathCollide : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
