using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] Canvas creditsCanvas;
    CanvasesController canvasesController;
    void Start()
    {
        canvasesController = FindObjectOfType<CanvasesController>();
    }

    void Update()
    {

    }

    public void ShowCredits()
    {
        canvasesController.UpdateCanvasToDisplay(creditsCanvas);
        canvasesController.ExitCurrentCanvas();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
