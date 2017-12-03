using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;
using System.Collections;
using System.Reflection;
using Test = NUnit.Framework.TestAttribute;

public class TetrimoTest {

    private void setupTetrimo(ref Tetrimo t)
    {
        t.TetrimoPartPrefab      = Resources.Load("TetrimoPartPrefab") as GameObject;
        t.TetrimoPrefab          = Resources.Load("TetrimoPrefab") as GameObject;
        t.TetrimoExplosionPrefab = Resources.Load("TetrimoExplosionPrefab") as GameObject;
        t.FourLinesLabelPrefab   = Resources.Load("FourLinesLabelPrefab") as GameObject;
    }

    //Checks that all of the tetris shapes are present and have their correct tetrimo positions and rotations
    [Test]
    public void TetrimoTest_Shapes()
    {
        // Use reflection to access protected fields!
        FieldInfo fi = typeof(Tetrimo).GetField("Shapes", BindingFlags.NonPublic | BindingFlags.Static);
        Vector2[,,] shapes = (Vector2[,,]) fi.GetValue(null);
        Vector2[] good_shapes = { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(0, -1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(2, 0), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1), new Vector2(2, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(0, 1), new Vector2(0, 0), new Vector2(0, -1), new Vector2(1, -1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(0, 1), new Vector2(0, 0), new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(1, 0), new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0), new Vector2(0, 1), new Vector2(0, 0), new Vector2(0, -1), new Vector2(0, -2), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0), new Vector2(0, 1), new Vector2(0, 0), new Vector2(0, -1), new Vector2(0, -2), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1), new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1), new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, -1) };

        int total_shapes = shapes.GetLength(0);
        int rotations_per_shape = shapes.GetLength(1);
        int blocks_per_rotation = shapes.GetLength(2);

        for (int i = 0; i < total_shapes; i++)
            for (int j = 0; j < rotations_per_shape; j++)
                for (int k = 0; k < blocks_per_rotation; k++) {
                    Assert.AreEqual(shapes[i,j,k], good_shapes[i*blocks_per_rotation*rotations_per_shape + j*blocks_per_rotation + k]);
                }
    }

    //Checks that the playing field is 19x11
    [Test]
    public void TetrimoTest_Field()
    {
        // Use reflection to access protected fields!
        FieldInfo fi = typeof(Tetrimo).GetField("FieldMatrix", BindingFlags.NonPublic | BindingFlags.Static);
        GameObject[,] FieldMatrix = (GameObject[,])fi.GetValue(null);

        Assert.AreEqual(FieldMatrix.GetLength(0), 19);
        Assert.AreEqual(FieldMatrix.GetLength(1), 11);
    }

    //Checks if tetrimo can move down based on current position
    [UnityTest]
    public IEnumerator TetrimoTest_CanMoveDown()
    {
        var go = new GameObject();
        go.AddComponent<Tetrimo>();
        var t = go.GetComponent<Tetrimo>();
        setupTetrimo(ref t);
        t.State = Tetrimo.TetrimoState.Spawning;

        //Wait for Start() to be called
        yield return new WaitForEndOfFrame();
        
        // Use reflection to access protected fields!
        FieldInfo IsMovingHorizontal = typeof(Tetrimo).GetField("IsMovingHorizontal", BindingFlags.NonPublic | BindingFlags.Instance);
        PropertyInfo CanMoveDown = typeof(Tetrimo).GetProperty("CanMoveDown", BindingFlags.NonPublic | BindingFlags.Instance);
        
        IsMovingHorizontal.SetValue(t, true);
        Assert.IsFalse((bool) CanMoveDown.GetValue(t, null));

        IsMovingHorizontal.SetValue(t, false);
        Assert.IsTrue((bool) CanMoveDown.GetValue(t, null));

        //Move piece to bottom of screen
        go.transform.Translate(19*Vector3.down, Space.World);
        Assert.IsFalse((bool) CanMoveDown.GetValue(t, null));
    }

    //Checks if tetrimo can move down based on current position
    [UnityTest]
    public IEnumerator TetrimoTest_CanMoveRight()
    {
        var go = new GameObject();
        go.AddComponent<Tetrimo>();
        var t = go.GetComponent<Tetrimo>();
        setupTetrimo(ref t);
        t.State = Tetrimo.TetrimoState.Spawning;

        //Wait for Start() to be called
        yield return new WaitForEndOfFrame();
        
        // Use reflection to access protected fields!
        PropertyInfo CanMoveRight = typeof(Tetrimo).GetProperty("CanMoveRight", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.IsTrue((bool) CanMoveRight.GetValue(t, null));

        //Move piece to right of screen
        go.transform.Translate(10*Vector3.right, Space.World);
        Assert.IsFalse((bool) CanMoveRight.GetValue(t, null));
    }

    //Checks if tetrimo can move down based on current position
    [UnityTest]
    public IEnumerator TetrimoTest_CanMoveLeft()
    {
        var go = new GameObject();
        go.AddComponent<Tetrimo>();
        var t = go.GetComponent<Tetrimo>();
        setupTetrimo(ref t);
        t.State = Tetrimo.TetrimoState.Spawning;

        //Wait for Start() to be called
        yield return new WaitForEndOfFrame();
        
        // Use reflection to access protected fields!
        PropertyInfo CanMoveLeft = typeof(Tetrimo).GetProperty("CanMoveLeft", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.IsTrue((bool) CanMoveLeft.GetValue(t, null));

        //Move piece to left of screen
        go.transform.Translate(10*Vector3.left, Space.World);
        Assert.IsFalse((bool) CanMoveLeft.GetValue(t, null));
    }

    //Checks if tetrimo can move down based on current position
    [UnityTest]
    public IEnumerator TetrimoTest_CanRotate()
    {
        var go = new GameObject();
        go.AddComponent<Tetrimo>();
        var t = go.GetComponent<Tetrimo>();
        setupTetrimo(ref t);
        t.State = Tetrimo.TetrimoState.Spawning;

        //Wait for Start() to be called
        yield return new WaitForEndOfFrame();
        
        // Use reflection to access protected fields!
        PropertyInfo CanRotate = typeof(Tetrimo).GetProperty("CanRotate", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.IsTrue((bool) CanRotate.GetValue(t, null));

        //Move piece to left of screen
        go.transform.Translate(10*Vector3.left, Space.World);
        Assert.IsFalse((bool) CanRotate.GetValue(t, null));
    }

    //Checks if tetrimo can move down based on current position
    [UnityTest]
    public IEnumerator TetrimoTest_FindLines()
    {
        var go = new GameObject();
        go.AddComponent<Tetrimo>();
        var t = go.GetComponent<Tetrimo>();
        setupTetrimo(ref t);
        t.State = Tetrimo.TetrimoState.Spawning;

        FieldInfo fi = typeof(Tetrimo).GetField("FieldMatrix", BindingFlags.NonPublic | BindingFlags.Static);
        GameObject[,] FieldMatrix = (GameObject[,])fi.GetValue(null);

        //Wait for Start() to be called
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < FieldMatrix.GetLength(1); i++)
        {
            //Fill in top three lines of field with tetrimos
            FieldMatrix[18,i] = go;   // #
            FieldMatrix[17,i] = go;   // ###
            FieldMatrix[16,i] = go;   // <-- Nothing in this line
        }

        // Use reflection to access protected fields!
        MethodInfo FindLines = typeof(Tetrimo).GetMethod("FindLines", BindingFlags.NonPublic | BindingFlags.Instance);
        ArrayList lines = (ArrayList) FindLines.Invoke(t, null);

        Assert.AreEqual(lines.Count, 2);
        Assert.IsTrue(lines.Contains(18));
        Assert.IsTrue(lines.Contains(17));
        Assert.IsFalse(lines.Contains(16)); //Doesn't count if current piece is not part of the line to be cleared
    }

}
