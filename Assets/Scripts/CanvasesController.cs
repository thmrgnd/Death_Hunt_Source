using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasesController : MonoBehaviour
{
    Canvas currentCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas HUD;

    [SerializeField] Queue<Canvas> canvasesToDisplay = new Queue<Canvas>();
    ThemeMusicManager themeMusicManager;


    void Start()
    {
        themeMusicManager = FindObjectOfType<ThemeMusicManager>();
    }

    void Update()
    {

    }

    public void UpdateCanvasToDisplay(Canvas nextCanvasToDisplay)
    {
        HUD.gameObject.SetActive(false);
        canvasesToDisplay.Enqueue(nextCanvasToDisplay);
        if (currentCanvas == null)
        {
            currentCanvas = canvasesToDisplay.Dequeue();
            Time.timeScale = 0f;
            currentCanvas.gameObject.SetActive(true);
        }

    }

    public void ExitCanvas(InputAction.CallbackContext context)
    {
        if (context.started)
            ExitCurrentCanvas();
    }

    public void ExitCurrentCanvas()
    {
        //if game is running - pause it
        if (currentCanvas == null)
        {
            PauseGame();
            return;
        }

        currentCanvas.gameObject.SetActive(false); //to reset last canvas
        Canvas c;
        //if there are more canvases to see
        if (canvasesToDisplay.TryPeek(out c))
        {
            NextCanvas();
        }
        //exit canvas or pause menu
        else
        {
            ContinueGame();
        }

    }

    private void PauseGame()
    {
        UpdateCanvasToDisplay(pauseCanvas);
        pauseCanvas.gameObject.GetComponentInChildren<TextRandomizer>().UpdateText();
        HUD.gameObject.SetActive(false);
        themeMusicManager.SetLowVolume();
    }

    private void NextCanvas()
    {
        currentCanvas = canvasesToDisplay.Dequeue();
        currentCanvas.gameObject.SetActive(true);
    }

    private void ContinueGame()
    {
        themeMusicManager.SetHighVolume();
        Time.timeScale = 1f;
        currentCanvas = null;
        HUD.gameObject.SetActive(true);
    }
}
