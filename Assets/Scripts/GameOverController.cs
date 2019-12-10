using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // try { Destroy(FindObjectOfType<PlayerController>().gameObject); } catch { }
        // try { Destroy(FindObjectOfType<UIManager>().gameObject); } catch { }
        // try { Destroy(FindObjectOfType<AudioManager>().gameObject); } catch { }

        //try { FindObjectOfType<PlayerController>().gameObject.SetActive(false); } catch { }
        //try { FindObjectOfType<UIManager>().gameObject.SetActive(false); } catch { }
        //try { FindObjectOfType<AudioManager>().gameObject.SetActive(false); } catch { }

        StatsManager playerStatsManager = FindObjectOfType<PlayerController>().GetStats();
        if(playerStatsManager.GetStrength() > 25) { playerStatsManager.SetStrength(playerStatsManager.GetStrength() - 5); }
        if(playerStatsManager.GetAgility() > 20) { playerStatsManager.SetAgility(playerStatsManager.GetAgility() - 5); }
        if(playerStatsManager.GetIntelligence() > 15) { playerStatsManager.SetIntelligence(playerStatsManager.GetIntelligence() - 5); }

        playerStatsManager.RecoverFullHealth();
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<PlayerController>().HidePlayer();
    }
}
