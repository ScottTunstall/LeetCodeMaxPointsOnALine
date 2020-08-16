# LeetCodeMaxPointsOnALine
Submission for max points on a line HARD problem

Idea for using slopes inspired from problem discussion - but I didn't copy any code from there. You can see in my commit history how this evolved.

A fair amount of trial and error required but I am satisfied this solves the problem - or as close as it can get.

Reason I am confident and not 100% sure is because leetcode tells me one answer is wrong yet using the same inputs on my machine give the correct result.

These inputs pass on my machine but fail on leetcode:

points = new int[][]
        {
            new[] {0, 0},
            new[] {94911150, 94911151},
            new[] { 94911151, 94911152}
        };

I *think* there's an issue with the double datatype's precision. Might be CPU specific?  Intel vs Amd maybe?  Just conjecture.

I can't step through the code on Leetcode so I can't see what values are returned by the slope calculation. I don't have another development PC to try this code on. 



 
