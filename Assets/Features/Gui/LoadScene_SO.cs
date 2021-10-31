using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LoadScene_SO : ScriptableObject
{
    [SerializeField] private int sceneIndex;

    public void LoadScene()
    {
        var scenePath = SceneUtility.GetScenePathByBuildIndex(this.sceneIndex);
        if (string.IsNullOrEmpty(scenePath)) return;
        SceneManager.LoadSceneAsync(scenePath);
    }
}
