# Evolution_Sim [WIP]
 An evolution simulator
 
 Requires: Unity v2019.1.7f1 or higher
 
 To Run:
 
 1. Open project file "Main.unity" located in Assets/Scenes
 2. Press Play button at top of Unity window
 
 Features:
 
 - Many adjustable world values such as world size, food spawn rate, and initial food number
 - 11 characteristic stats used to define organisms (See Stat Info)
 - Simple death system of energy replenished by food
 - Reproduction system which can form new generations
   - New generations have varied characteristic stats based off parent values
 - Graphical interface for comparing each stat individually
 - Graphical interface for comparing generational averages of each stat

 Stat Info
 
Size       |	mass, volume
Endurance  |	energy capacity
Efficiency |	extracting nourishment, using energy
Speed      |	movement quickness
Agility    |	maneuvering, turning
Finesse    |	using tools deftly, delicate actions
Reasoning  |	logic, learning
Memory     |	retaining past events
Fertility  |	offspring count modifier
Sense	     | perceiving surroundings, identifying
Special    |	host specific trability value

Other Notes:

While Unity may not have been the most efficient engine for this application, especially considering limited user input during runtime, it presented the most readily available API for running a simulation with presentable graphics due to my previous experience with C#.
