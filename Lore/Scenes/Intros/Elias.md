# Elias — Merchants on the Road

## Full Scene Map

The small diagrams below are the only place each section's internal structure is defined — one source of truth. This map shows the high-level shape of the whole scene by treating each *section* as a single node, linked the way the sections actually connect. No node or edge is duplicated from the diagrams below; scroll to a section's own diagram for its internal detail.

```mermaid
flowchart TD
    Merchants[Traveling Merchants] --> Market[Cindral Market]
    Market --> Lorean[Lorean, Mayor's Clerk]
    Lorean --> Rest[Rest and Search]

    Rest -->|Deception| Scenario1[Scenario 1: Guards present]
    Rest -->|Stealth| Scenario2[Scenario 2: Elias alone]
    Rest -->|Trap| Scenario3[Scenario 3: Bandits alerted]

    Scenario1 -->|Whistle / assault| CombatStart([Combat Start])
    Scenario1 -->|Success / submit| LordOffice([Lord's Office])
    CombatStart --> LordOffice

    Scenario2 -->|Success / submit| LordOffice
    Scenario2 -->|Fight| CombatStart

    Scenario3 -->|Fight| CombatStart
    Scenario3 -->|Submit / not caught| LordOffice

    LordOffice --> Resolution[Resolution]
```

---

## Encounter: Traveling Merchants

```mermaid
flowchart TD
    Start([Main road]) --> M0[Start of interaction]

    M0 -->|Cindral| M1_Q[Ask: Cindral]
    M1_Q --> M1_A[Decent for business]
    M1_A --> M0

    M0 -->|The job| M2_Q[Ask: the job]
    M2_Q --> M2_A[Clerk was hush-hush]
    M2_A --> M0

    M0 -->|Blacksmith| M3_Q[Ask: blacksmith]
    M3_Q --> M3_A[Young, skilled smith]
    M3_A --> M0

    M0 -->|Done| M4[Walks to Cindral]
```

**Start.** Elias is traveling along a main road taking inventory of his supplies. On a recent job his sword was damaged, and he's seeking to repair or replace it, but he isn't near any major cities.

**M0.** Elias may ask the merchants about Cindral, the job, or the blacksmith, in any order, before continuing on. Each answer loops back to this state.

**M1_Q — Ask About Cindral.** Elias: "What could you tell me about Cindral?"
**M1_A.** Merchant: "It's a decent enough place to do business, from what we could tell. There's not much in the way of competition."

**M2_Q — Ask About the Job.** Elias: "What could you tell me about this job?"
**M2_A.** Merchant: "Not much in the way of details. The scrawny punk who asked us was all hush about the whole thing."

**M3_Q — Ask About the Blacksmith.** Elias: "Does Cindral have a decent blacksmith?"
**M3_A.** Merchant: "Yes, we met him while we were in town. Younger man, but skilled enough to get the job done."

**M4.** He continues walking along the road until he reaches the village.

---

## Encounter: Cindral Market

```mermaid
flowchart TD
    MK1[Browses market] --> MK2[Sword too damaged]
    MK2 --> MK3[Offers his wares]
    MK3 --> MK4[Buys shortsword]
```

**MK1.** Upon arrival, Elias observes a busy local market, with a commodity/bartering based trading system. He walks past a blacksmith's stall, and stops to inspect the wares.

**MK2.** The smith tells him that it's a shame that the sword was damaged so heavily, as it is of very high quality and was very intricately crafted.

**MK3.** He doesn't have the tools or materials on hand to do such specialized work. He does, however, encourage Elias to browse his wares and select a new sword.

**MK4.** He picks out a shortsword, but keeps his old damaged one in hopes of eventually getting it fixed.

---

## Encounter: Lorean, the Mayor's Clerk

```mermaid
flowchart TD
    L1[Meets clerk] --> L2[Asks directions]
    L2 --> L3[Clerk: Lorean]
    L3 --> L4[Asks about work]
    L4 --> L5[Explains bandits]
    L5 --> L6[Tasked: find camp]
```

**L1.** As Elias searches for the mayor's office, a strange man stands in the road. He has a checklist, a stack of books, and several important looking papers balanced precariously in his arms. He looks a bit tired/overworked.

**L2.** Elias thinks this person might be able to point him in the right direction, and asks if he knows where the mayor's office is located.

**L3.** **Lorean:** "I might be who you're looking for, as I am the mayor's clerk, Lorean." He asks Elias what he can help with.

**L4.** Elias inquires about the need for mercenary work.

**L5.** Lorean tells him about the recent issue with bandits robbing the villagers, as well as the merchants who pass through along the nearby road.

**L6.** Elias is tasked with aiding the village guards in tracking down the bandit's camp, and eliminating the threat.

---

## Rest and Search

```mermaid
flowchart TD
    Inn[Rests at inn] --> Weeks[Weeks, no progress]
    Weeks -->|Deception| Decep[Ambush wagon]
    Weeks -->|Stealth| Stealth[Follow bandits]
    Weeks -->|Trap| Trap[Trap + torture]
```

**Inn.** He is pointed to the local inn, where he is able to have a meal and rest for the night.

**Weeks.** Over the next few weeks, he works with the guards in locating the bandit camp. After a few weeks with little to no progress, he…
- **Deception / active:** Disguised as a small group of merchants, Elias and the guards camp in the woods by the main road, with a wagon of goods from the village left relatively unattended. As expected, the bandits strike in the middle of the night, and Elias must fight off the attack.
- **Stealth / passive:** Focus on the village, find out how the bandits are getting in, watch them leave and follow them back to their camp.
- **Trap / Aggressive:** Find out how they're getting into the village, set a bear trap, and torture information out of the caught bandit.

---

## Scenario 1: Camp Found, Guards Present

```mermaid
flowchart TD
    Scen1[Guards present] -->|A| S1A[Signal + infiltrate]
    Scen1 -->|B| S1B[Aggressive assault]

    S1A --> S1A_Obj[Gather intel]
    S1A_Obj -->|Success| S1A_Success[Castle location found]
    S1A_Obj -->|Caught, whistle| CombatStart([Combat Start])
    S1A_Obj -->|Caught, submit| S1A_Submit[Brought to lord]

    S1B --> CombatStart
```

**Scen1.** You were able to find the bandit camp. You have the group of soldiers with you. An attack will be easier, but an infiltration will be harder with more people.
- **Option A:** Have the guards wait outside the camp, with a pre-established signal to move in if needed. Elias will attempt to infiltrate the camp and seek out the bandit leader.
- **Option B:** Fight aggressively — find the leader by force.

**S1A_Obj.** Mission objective: obtain information on the bandits and their leader (stealth).
- **Success:** Location of the lord's castle found.
- **Caught, whistle:** Whistle to call soldiers — combat begins.
- **Caught, submit:** Submit to be brought to the bandit's leader, then to the lord.

**S1B.** Combat begins.

---

## Combat Start Resolution

```mermaid
flowchart TD
    CombatStart([Combat Start]) -->|Survivor| C1_Survivor[Info from survivor]
    CombatStart -->|All dead| C1_Explore[Letter in camp]
```

**CombatStart:**
- **If at least 1 bandit survives:** Obtain the lord's location from the survivor(s).
- **If all bandits dead:** Explore the camp for clues. Mission objective: find the lord's location (letter).

---

## Scenario 2: Camp Found, Elias Alone

```mermaid
flowchart TD
    Scen2[Elias alone] -->|A| S2A[Sneak + listen]
    Scen2 -->|B| S2B[Return, alert guard]

    S2A --> S2A_Obj[Gather intel]
    S2A_Obj -->|Success| S2A_Success[Castle location found]
    S2A_Obj -->|Caught, submit| S2A_Submit[Brought to lord]
    S2A_Obj -->|Caught, fight| CombatStart([Combat Start])
```

**Scen2.** You were able to find the bandit camp. You are already at the camp, alone. Infiltration will be much easier alone, but a frontal assault will be much harder.
- **Option A:** Sneak around the camp and listen in until you find the bandit leader.
- **Option B:** Return to Cindral to alert the guard.

**S2A_Obj.** Mission objective: obtain information on the bandits and their leader (stealth).
- **Success:** Location of the lord's castle found.
- **Caught, submit:** Submit to be brought to the bandit's leader, then to the lord.
- **Caught, fight:** Fight your way out. Resolves using the [[#Combat Start Resolution|same Combat Start logic]].

---

## Scenario 3: Camp Found, Bandits Alerted

```mermaid
flowchart TD
    Scen3[Bandits alerted] -->|A| S3A[Impersonate captive]
    Scen3 -->|B| S3B[Full stealth]

    S3A --> S3A_Caught[Not recognized]
    S3A_Caught -->|Submit| S3A_Submit[Brought to lord]
    S3A_Caught -->|Fight| CombatStart([Combat Start])

    S3B -->|Caught, submit| S3B_Submit[Brought to lord*]
    S3B -->|Caught, fight| CombatStart
    S3B -->|Not caught, survivor| S3B_Survivor[Info from survivor]
    S3B -->|Not caught, all dead| S3B_Explore[Letter in camp]
```

**Scen3.** You were able to find the bandit camp. The bandits are on alert, due to a member of their group being captured. Proceeding will be much more difficult.
- **Option A:** Attempt to impersonate the bandit that you captured by wearing his uniform and infiltrating the camp.
- **Option B:** Full stealth mission — evade detection entirely.

**S3A_Caught.** You are caught by the bandits, as they don't recognize you.
- **Submit:** Submit to be brought to the bandit's leader, then to the lord.
- **Fight:** Fight your way out. Resolves using the [[#Combat Start Resolution|same Combat Start logic]].

**S3B — Full Stealth:**
- **Caught, submit:** Submit to be brought to the bandit's leader, then to the lord. *Not available if any bandits were killed.*
- **Caught, fight:** Fight your way out. Resolves using the [[#Combat Start Resolution|same Combat Start logic]].
- **Not caught, survivor:** At least 1 bandit survives — obtain the lord's location from the survivor(s).
- **Not caught, all dead:** Explore the camp for clues. Mission objective: find the lord's location (letter).

---

## Lord's Office

```mermaid
flowchart TD
    LordOffice([Lord's Office]) -->|Alone| LordOpen[Reveals cult]
    LordOffice -->|Guards| LordCryptic[Cryptic, still helps]
    LordOpen --> LordQuestion([Question Lord])
    LordCryptic --> LordQuestion
```

**LordOffice.** All above scenes filter to a conversation in the Lord's office.
- **If Elias is alone:** the lord openly tells him about Cindral's cultish practices and questionable culture.
- **If Elias is with the Cindral town guards:** the lord is more cryptic in telling him about the cult, but still tries to help.

**LordQuestion.** Dialogue options include questioning the lord.

---

## Resolution

```mermaid
flowchart TD
    LordQuestion([Question Lord]) --> Reveal[Ritual is sacrifice]
    Reveal --> Escape[Escape, now wanted]
```

**Reveal.** He realizes that everything the lord said was true when the so-called 'ascension ritual' turns out to be a cultish sacrifice.

**Escape.** Chaos breaks out as he tries to save his newly made friend from this unfortunate fate. They narrowly escape back to the lord's village, now wanted by Cindral's people.
