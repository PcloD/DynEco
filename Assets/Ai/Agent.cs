using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Agent/agent")]
public class Agent : ScriptableObject
{
    [SerializeField]
    private Personality personality;
    public Personality Personality { get { return personality; } }

    [SerializeField]
    private SocialLevel socialLevel;
    public SocialLevel SocialLevel { get { return socialLevel; } }

    public Material color;

    public string spawnName;
    public string race;
    public string NameGenerator() {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[3];

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[Random.Range(0, chars.Length)];
        }

       return new string(stringChars) + '_' + race;
    }

    public List<AAction> actions;

    public void PrepareAllActions()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Prepare();
        }
    }

}
