# MMP-Project

A small video game created for the [Multi-Media Programming (MMP)](https://www.medien.ifi.lmu.de/lehre/ss22/mmp/) lecture.  
The course introduced fundamental techniques and programming interfaces for developing multimedia applications on desktop, web, and mobile platforms, with a focus on 2D graphics, sound, video, and animation programming. It emphasized cross-platform programming patterns, techniques, and workflows, using 2D games as primary application examples.

## Overview
The game consists of the following scenes:
- **Start Menu**
- **Options Menu**
- **Play Scene**
- **Pause Menu**
- **Win Menu**
- **Death Menu**

## Features
- **Player Animations**:
	- Idle, walk, and attack animations (melee weapon and bow) in 4 directions.
- **Player Actions**:
	- Attack with a shovel using the **"K" key**.
	- Shoot arrows with a bow using the **"L" key**.
- **Environment**:
	- A custom-made map created using tiles.
- **Enemy Interaction**:
	- Enemies mostly have 1 life. At 0 life, they fall to the ground and disappear.
	- Bosses occasionally appear with larger size and increased life.

## Difficulty Levels
- **Medium** and **Hard** modes differ in:
	- Enemy spawn points.
	- Number of enemies.
	- Number of waves.

## Results
Depending on the outcome:
- On victory: The time survived is displayed.
- On defeat: The number of waves survived is shown.
