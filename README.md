__Penguin Run Project Description__

This game was created as part of a university project. 
It focuses on Unity Editor Tooling and Level Design Patterns.

You can find a playable demo here: https://szmahdis.itch.io/penguinrun

---

__Implemented Level Design Patterns__

<!-- The player is chased by enemies in maze-like city and has to collect the collectibles to complete the level. The focus of this game is on level design. Map is divided into districts (Miyamoto’s Miniature Garden). Starting point acts as vantage point so the player can view the whole map and move in desired section. Multiple paths between sectors promote the  player's choice. Collectibles are placed as wayfinding cues and decoration serve as “borders” for the map. Verticality is another level design that was implemented in this game. -->


The player is chased by enemies through a maze-like urban environment and must collect collectibles to complete the level. The game's core focus is level design, structured around the following principles:

- Miyamoto's Miniature Garden: the map is divided into distinct districts, each with its own character and layout
- Vantage Point Start: the starting location sits elevated above the map, giving players an immediate read of the full environment before committing to a direction
- Multiple Paths Between Sectors: interconnected routes encourage player agency and replayability
- Collectibles as Wayfinding: pickups are placed deliberately to guide players through the space, while decorative elements act as soft borders between zones
- Verticality: elevation changes are used as a core level design mechanic, creating layered traversal and sightline variety
---

__Implemented Unity Editor Tooling__

As part of the project, we developed custom Unity editor tools and inspector extensions to accelerate level design, testing, and debugging workflows.

- Built scene-authoring utilities includes:
    - Object spawning systems
    - Waypoint generation tools
    - Movement path editors
    - Runtime debugging inspectors

- Implemented custom waypoint editing workflows with:
    - Custom gizmo visualization for waypoints and paths directly in the Scene view
    - Procedural waypoint generation based on scene geometry and configurable parameters
    - Path interpolation for smooth, runtime-ready movement curves
    - Scene-aware waypoint management that adapts to level layout changes

- Created runtime debugging inspectors:
    - Live modification of player state during Play Mode (health, score, collectible count)
    - Spawn location overrides for rapid repositioning during testing
    - Runtime controls for collectable and enemy state for isolated testing

- Designed reusable platform movement systems:
    - Linear movement behavior
    - Circular movement behavior
    - Trigger-based event configuration
    

---
Members: Carolina Kawabata, Mahdis Sabzevarzadeh, Jacob Lütje, Lorena Vitale