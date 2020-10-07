using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUIManager : MonoBehaviour
{
    // External Slot Position References
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject specialSlot;

    // Power Up Prefabs
    public GameObject attackPrefab;
    public GameObject defensePrefab;
    public GameObject speedPrefab;
    public GameObject vibePrefab;
    
    // External Referece
    public CharacterStatus stats;


    // Interal State
    GameObject activeSlot1;
    GameObject activeSlot2;
    GameObject activeSlot3;
    GameObject activeSpecialSlot;
    bool attackActive = false;
    bool defenseActive = false;
    bool speedActive = false;
    bool vibeActive = false;


    void FixedUpdate() {
        if(!activeSlot1 && attackActive) {
            activeSlot1 = Instantiate(attackPrefab, slot1.transform.position, Quaternion.identity);
            activeSlot1.transform.parent = transform;
        } else if (!attackActive && activeSlot1) {
            Destroy(activeSlot1);
        }
        if(!activeSlot2 && defenseActive) {
            activeSlot2 = Instantiate(defensePrefab, slot2.transform.position, Quaternion.identity);
            activeSlot2.transform.parent = transform;
        }else if (!defenseActive && activeSlot2) {
            Destroy(activeSlot2);
        }

        if(!activeSlot3 && speedActive) {
            activeSlot3 = Instantiate(speedPrefab, slot3.transform.position, Quaternion.identity);
            activeSlot3.transform.parent = transform;
        }else if (!attackActive && activeSlot3) {
            Destroy(activeSlot3);
        }

        if(!activeSpecialSlot && vibeActive) {
            activeSpecialSlot = Instantiate(vibePrefab, specialSlot.transform.position, Quaternion.identity);
            activeSpecialSlot.transform.parent = transform;
        }else if (!attackActive && activeSpecialSlot) {
            Destroy(activeSpecialSlot);
        }
    }


    void Update() {
        // Get States of Buffs
        attackActive = stats.hasBuff(PowerUp.BUFF_TYPE.ATTACK);
        defenseActive = stats.hasBuff(PowerUp.BUFF_TYPE.DEFENSE);
        speedActive = stats.hasBuff(PowerUp.BUFF_TYPE.SPEED);
        vibeActive = stats.hasBuff(PowerUp.BUFF_TYPE.VIBE);
    }

}
