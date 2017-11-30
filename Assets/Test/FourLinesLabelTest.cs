using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;
using System.Collections;

public class FourLinesLabelTest {

    // Checks to make sure label moves up every frame
    [UnityTest]
    public IEnumerator FourLinesLabel_MovesUp()
    {
        var go = new GameObject();
        go.AddComponent<MeshRenderer>();
        go.AddComponent<FourLinesLabel>();
        var fll = go.GetComponent<FourLinesLabel>();

        fll.LiftUpBy = 2;
        fll.LiftSpeed = 2.5f;
        fll.FadeSpeed = 1;

        Assert.AreEqual(go.transform.position.y, 0);

        //Wait 10 frames
        for (int i = 0; i < 10; i++)
            yield return new WaitForEndOfFrame();

        Assert.IsTrue(go.transform.position.y > 0);
    }

    // Checks to make sure label fades out every frame
    [UnityTest]
    public IEnumerator FourLinesLabel_FadesOut()
    {
        var go = new GameObject();
        go.AddComponent<MeshRenderer>();
        var rend = go.GetComponent<MeshRenderer>();
        go.AddComponent<FourLinesLabel>();
        var fll = go.GetComponent<FourLinesLabel>();

        fll.LiftUpBy = 2;
        fll.LiftSpeed = 2.5f;
        fll.FadeSpeed = 1;

        Assert.AreEqual(rend.material.color.a, 1);

        //Wait 10 frames
        for (int i = 0; i < 10; i++)
            yield return new WaitForEndOfFrame();

        Assert.IsTrue(rend.material.color.a < 1);
    }

    // Checks to make sure label destroys self
    [UnityTest]
    public IEnumerator FourLinesLabel_DestroysSelf()
    {
        var go = new GameObject();
        go.AddComponent<MeshRenderer>();
        var rend = go.GetComponent<MeshRenderer>();
        go.AddComponent<FourLinesLabel>();
        var fll = go.GetComponent<FourLinesLabel>();

        fll.LiftUpBy = 0;
        fll.LiftSpeed = 2.5f;
        fll.FadeSpeed = 1;

        Assert.IsNotNull(go);

        //Wait 10 frames
        for (int i = 0; i < 10; i++)
            yield return new WaitForEndOfFrame();

        Assert.IsNull(go);
    }
}
