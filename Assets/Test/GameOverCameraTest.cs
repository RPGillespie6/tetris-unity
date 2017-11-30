using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;
using System.Collections;

public class GameOverCameraTest {

    // Checks to make rectangle dimensions are as expected
    [UnityTest]
    public IEnumerator GameOverCameraTest_Rect()
    {
        Rect test = GameOverCamera.CreateCenteredRect(100.0f, 100.0f);
        Assert.AreEqual(test.width, 100);
        Assert.AreEqual(test.height, 100);
        yield return null;
    }
}
