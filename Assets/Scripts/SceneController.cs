using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void OnEnable()
    {
        // Abonnez-vous aux événements SceneManager.sceneLoaded et SceneManager.sceneUnloaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        // Désabonnez-vous des événements SceneManager.sceneLoaded et SceneManager.sceneUnloaded
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Actions à effectuer lors du chargement d'une scène
        if (scene.name == "MaScene") // Remplacez "MaScene" par le nom de votre scène spécifique
        {
            // Effectuez des actions spécifiques à cette scène
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // Actions à effectuer lors du déchargement d'une scène
        if (scene.name == "MaScene") // Remplacez "MaScene" par le nom de votre scène spécifique
        {
            // Effectuez des actions spécifiques à cette scène avant qu'elle ne soit déchargée
        }
    }
}
