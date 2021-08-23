using System.Collections;
using System.Collections.Generic;
using WindowsInput.Native;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using WindowsInput;

public class A4_BoundsCheck
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player;
    [UnityTest, Order(1)]
    public IEnumerator LeftBoundsCheck()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        player = GameObject.Find("Player");
        yield return null;
        GameObject.Destroy(GameObject.Find("LevelEnd"));
        yield return null;
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_A);
        Vector3 firstPos = player.transform.position;
        yield return new WaitForSeconds(7);
        Vector3 middlePos = player.transform.position;
        yield return new WaitForSeconds(2);
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_A);
        Vector3 secondPos = player.transform.position;
        Assert.True(Mathf.Abs(firstPos.y-secondPos.y)<0.01f,"Level's ground should not change it's height!");
        Assert.True(Mathf.Abs(middlePos.x-secondPos.x)<0.01f,"Left bound of level was not added or placed too far!");
    }
    [UnityTest, Order(2)]
    public IEnumerator RightBoundsCheck()
    {
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return null;
        player = GameObject.Find("Player");
        yield return null;
        GameObject.Destroy(GameObject.Find("LevelEnd"));
        yield return null;
        IS.Keyboard.KeyDown(VirtualKeyCode.VK_D);
        Vector3 firstPos = player.transform.position;
        yield return new WaitForSeconds(7);
        Vector3 middlePos = player.transform.position;
        yield return new WaitForSeconds(2);
        IS.Keyboard.KeyUp(VirtualKeyCode.VK_D);
        Vector3 secondPos = player.transform.position;
        Assert.True(Mathf.Abs(firstPos.y-secondPos.y)<0.01f,"Level's ground should not change it's height!");
        Assert.True(Mathf.Abs(middlePos.x-secondPos.x)<0.01f,"Right bound of level was not added or placed too far!");
    }
}
