using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Personality
{
    /*
     * Will find conflicts and will prefer atacking.
     */
    AGRESSIVE,
    /*
     * Will not find conflicts but when in one prefers to stand his ground.
     */
    DEFENSIVE,
    /*
     *  Will not find conflicts and when in a conflict, prefers to run away.
     */
    PACIFIST
}


public enum SocialLevel
{
    BIG_GROUPS,
    SMALL_GROUPS,
    SOLO
}
