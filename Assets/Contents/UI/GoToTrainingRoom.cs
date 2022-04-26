using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTrainingRoom : ButtonWrapper
{
    public LoadingManager manager;

    protected override void ButtonAction()
    {
        manager.ChangeScene(LoadingManager.SceneName.TrainingRoom);
    }
}
