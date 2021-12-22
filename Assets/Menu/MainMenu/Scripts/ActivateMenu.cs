using UnityEngine;

public class ActivateMenu : MonoBehaviour
{
    [SerializeField] private GameObject MenuToActivate;

    public void SetDifrentState()
    {
        MenuToActivate.SetActive(!MenuToActivate.activeSelf);
    }
}
