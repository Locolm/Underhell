using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void OnEnable()
    {
        // Abonnez-vous aux �v�nements SceneManager.sceneLoaded et SceneManager.sceneUnloaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        // D�sabonnez-vous des �v�nements SceneManager.sceneLoaded et SceneManager.sceneUnloaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Actions � effectuer lors du chargement d'une sc�ne
        if (scene.name == "MaScene") // Remplacez "MaScene" par le nom de votre sc�ne sp�cifique
        {
            // Effectuez des actions sp�cifiques � cette sc�ne
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // Actions � effectuer lors du d�chargement d'une sc�ne
        if (scene.name == "MaScene") // Remplacez "MaScene" par le nom de votre sc�ne sp�cifique
        {
            // Effectuez des actions sp�cifiques � cette sc�ne avant qu'elle ne soit d�charg�e
        }
    }
}
