using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FirstTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void FirstTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }
    [Test]
    public void AddNumbersTest()
    {
        int num = 2;
        Assert.Greater(num,4);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator FirstTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
