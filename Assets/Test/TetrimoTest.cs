using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;
using System.Collections;
using System.Reflection;
using Test = NUnit.Framework.TestAttribute;

public class TetrimoTest {

    private void setupTetrimo(ref Tetrimo t)
    {
        // var go = new GameObject();
        // go.AddComponent<Tetrimo>();
        // var t = go.GetComponent<Tetrimo>();
        // setupTetrimo(ref t);
        // t.State = Tetrimo.TetrimoState.Spawning;
        t.TetrimoPartPrefab = Resources.Load("TetrimoPartPrefab") as GameObject;
        t.TetrimoPrefab = Resources.Load("TetrimoPrefab") as GameObject;
        t.TetrimoExplosionPrefab = Resources.Load("TetrimoExplosionPrefab") as GameObject;
        t.FourLinesLabelPrefab = Resources.Load("FourLinesLabelPrefab") as GameObject;
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

}
