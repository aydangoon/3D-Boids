# 3D-Boids
3D simulation of Boids based on the paper by C.W. Reynolds, made in Unity.

![](boids.gif)

This project is a follow up to my previous 2D Boids simulation made in python. Here I added more complex collision behaviors 
to the boids as well as tweaked boid sensory input to align more with the principles of Reynolds' paper.

## What are boids?

Boids are "bird-oid" objects that exhibit flock like motion. The key idea behinds boids is that they are atomized units
that together simulate complex behavior. To create emergent behavior boids must abide by two fundamental rules:

1) All boids have relatively weak perceptive abilities. (Narrow FOV, small sensory radius)
2) inter-boids relations are defined by cohesion, separation, and alignment.

Wikipedia has good graphics explaining cohesion, separation and alignment:

### Separation
<img src="separation.gif"
     alt="Separation"/>

### Cohesion
[<img src="https://en.wikipedia.org/wiki/Boids#/media/File:Rule_cohesion.gif">](https://en.wikipedia.org/)

### Alignment
[<img src="https://en.wikipedia.org/wiki/Boids#/media/File:Rule_alignment.gif">](https://en.wikipedia.org/)


I'm pretty happy with how this turned out but I plan to work on some improvements. Primarily this simulation is not so easy on the computer as iterating through the boids is O(n^2). Additionally I used Sebastian Lague's algorithm for generalizing
points on a sphere and for a large number of points this can be a lot of computational work. There's likely a better way to
do this that satisfies what the simulation needs.

I want to try and improve the simulation by emulating the same behavior with less computation. That'll probably be my goal
over the next week before university starts up again.


