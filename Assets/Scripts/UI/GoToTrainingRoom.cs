using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTrainingRoom : ButtonWrapper
{
    protected override void ButtonAction()
    {
        SceneManager.LoadScene("TrainingRoom");
    }
}
