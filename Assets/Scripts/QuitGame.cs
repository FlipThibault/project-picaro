using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void QuitApp()
    {
        // Check if we are running in the Unity Editor
#if UNITY_EDITOR
        // Exit play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application
        Application.Quit();
#endif
    }
}
