using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using WindowsInput;
using WindowsInput.Native;

public class B3_Level1Pass
{
    private InputSimulator IS = new InputSimulator();
    private GameObject player;
    private GameObject end;
    private Collider2D endCol;
    private GameObject[] gems;
    [UnityTest, Order(1)]
    public IEnumerator PlacedCorrectly()
    {
        Time.timeScale = 40;
        SceneManager.LoadScene(PMHelper.curLevel);
        yield return new WaitForSeconds(1);
        player = GameObject.Find("Player");
        end = GameObject.Find("LevelEnd");
        gems = GameObject.FindGameObjectsWithTag("Gem");
        if (gems.Length != 3)
        {
            Assert.Fail("Place only 3 gems on scene");
        }
        endCol = end.GetComponent<Collider2D>();
        endCol.enabled = false;
        foreach (GameObject gem in gems)
        {
            if (gem == null)
            {
                continue;
            }
            Vector3 tmp = player.transform.position;
            tmp.x = gem.transform.position.x;
            player.transform.position = tmp;
            yield return new WaitForSeconds(0.1f);
            if (gem != null)
            {
                IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                yield return new WaitForSeconds(2f);
            }

            if (gem != null)
            {
                Assert.Fail("Gems should be reachable by player");
            }
        }

        endCol.enabled = true;
        Scene cur = SceneManager.GetActiveScene();
        Scene next;
        Vector3 tmp2 = player.transform.position;
        tmp2.x = endCol.ClosestPoint(player.transform.position).x;
        player.transform.position = tmp2;
        yield return new WaitForSeconds(1f);
        next = SceneManager.GetActiveScene();
        if (cur==next)
        {
            IS.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            yield return new WaitForSeconds(2f);
        }

        if (cur == next)
        {
            Assert.Fail("Level's end should be reachable by player");
        }

        if (!next.name.Equals(PMHelper.nextLevel))
        {
            Assert.Fail("Scene \"Level 2\" should be loaded on triggering LevelEnd's collider");
        }
    }
}
