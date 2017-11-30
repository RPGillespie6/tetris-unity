using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;
using System.Collections;
using Test = NUnit.Framework.TestAttribute;

public class HighScoreTest {

    // Checks that high score object by default has 0 as scores
    [Test]
    public void HighScoreTest_New()
    {
        HighScore.clearScores();
        Assert.AreEqual(HighScore.get(0), 0);
        Assert.AreEqual(HighScore.get(1), 0);
        Assert.AreEqual(HighScore.get(2), 0);
    }

    // Checks that high scores are added to the correct side
    [Test]
    public void HighScoreTest_Add()
    {
        HighScore.clearScores();
        Assert.IsTrue(HighScore.TryAddHighScore(100));
        Assert.AreEqual(HighScore.get(0), 0);
        Assert.AreEqual(HighScore.get(1), 0);
        Assert.AreEqual(HighScore.get(2), 100);
    }

    // Checks that high score object by default has 0 as scores
    [Test]
    public void HighScoreTest_Add3()
    {
        HighScore.clearScores();
        Assert.IsTrue(HighScore.TryAddHighScore(300));
        Assert.IsTrue(HighScore.TryAddHighScore(400));
        Assert.IsTrue(HighScore.TryAddHighScore(500));
        Assert.AreEqual(HighScore.get(0), 300);
        Assert.AreEqual(HighScore.get(1), 400);
        Assert.AreEqual(HighScore.get(2), 500);
    }

    // Checks that high score object that is too low cannot be added
    [Test]
    public void HighScoreTest_AddBad()
    {
        HighScore.clearScores();
        Assert.IsFalse(HighScore.TryAddHighScore(-1));
        Assert.AreEqual(HighScore.get(0), 0);
        Assert.AreEqual(HighScore.get(1), 0);
        Assert.AreEqual(HighScore.get(2), 0);
    }
}
