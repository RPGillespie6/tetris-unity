using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;
using System.Collections;
using Test = NUnit.Framework.TestAttribute;

public class MatchCameraTest {

    // Checks that match stats are set to correct defaults on new game
    [UnityTest]
    public IEnumerator FourLinesLabel_MovesUp()
    {
        var go = new GameObject();
        go.AddComponent<MatchCamera>();
        var mc = go.GetComponent<MatchCamera>();

        //Wait 1 frame
        yield return new WaitForEndOfFrame();

        //NewGame() will have been called by now
        Assert.AreEqual(MatchCamera.Scores, 0);     //Start game with score 0
        Assert.AreEqual(MatchCamera.Level, 1);      //Start game at level 1
        Assert.AreEqual(MatchCamera.Continuous, 0); //Start game with no streak
        Assert.AreEqual((int) Tetrimo.TetrimoCount, 0);   //Start game with no block count
    }
}
