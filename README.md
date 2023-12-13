# ray-tracing
Ray Tracing Project for Computer Graphics. 

# What is so special about this ray tracer?
Aside from normal futures like different kinds of projection, I paralelized and added an antialiaser to my raytracer. Fairly simple and straightforward! 

![alt text](./images/Spheres.bmp)

![alt text](./images/SphereArray.bmp)


But I still can't draw spheres and planes on paper.

# How to use it?
This ray-tracer implements 4 different anti-aliasing techniques. I will mention how to use each technique below
## Gaussian Filtering
Before the image is being transformed into a bitmap in the image class, you may pass the image through Gaussian Filter. Parameters: Sigma and Size
## Sample Antialiasing
While the rays are being traced into the scene, increase the area they can be casted into (as a square) and do this n times. Parameters: Square width, and sampling number. Use AntialiasedColor() function inside ParallelRenderImage(), and the corresponding private variables in the class to adjust parameters.
## Ray Differentials
Do slight variations in origin and direction of the rays. Parameters: differential constants. Use CalculateRayDifferentials() function inside AntialiasedColor() function. Note: Make sure to set AntialiasedColor parameters to 1 and 0 to simulate no Sample Antialiasing.
## Enlarge -> Squish
Draw the image x times bigger and average the pixel colors. Parameters: enlargement constant. use the corresponding rendering method for this. 
