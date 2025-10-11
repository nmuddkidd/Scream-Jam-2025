# EVIL PONG!

10/11/25

## controls:
- 

### Current Features:
- Classic paddle vs AI gameplay
- Dynamic ball spawning system
- Lives-based survival gameplay
- Boundary-constrained paddle movement
- Tag-based goal detection

### Planned Evil Features:
- Chaotic mix-ups every 30 seconds
- Multiple balls spawning simultaneously
- Paddle size/speed modifications
- Visual chaos modes
- Progressive difficulty

## State Controller

- Handles logic for managing game states.
- handles player score and lives, scoring mechanics

- Notes:
    Currently skips start menu, which will receive an input in future to bypass start menu
    Will control mix ups in future based on timer

## Ball Manager

- Spawns ball when playing
- methods to intialize new balls which state mixup handler can utilize

## Ball (script component for ball prefab)

- handles internal speed mechanics
- ability to add new methods for update to change sprite, color, angle modifier, splitting call?

## AI and player scripts

- UP1 and AP1, "User" and "AI", naming convention for future implementation that will be needed to make more than one paddle depending on mixups, planning to add paddle handler script to do this?