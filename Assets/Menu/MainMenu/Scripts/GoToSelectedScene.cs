using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSelectedScene : MonoBehaviour
{
    [SerializeField] private int ScenNumber;
    public void LoadDefinedScene()
    {
        SceneManager.LoadScene(ScenNumber);
    }
}
