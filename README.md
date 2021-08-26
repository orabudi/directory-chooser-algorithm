# directory-chooser-algorithm
An algorithm to choose the most preferenced directory out of many. The preference is calculated by the size of files in a directory and the creation date of the directory.

The algorithm is implemented in C# (used in a .NET Core project).

### Constants and Variables
-  QUEUE_DIR - The folder containing all the directories from which we want to find the preferenced directory
-  FIRST_DIRECTORIES - The number of the oldest directories (by date created) out of all directories we want to calculate the preference algorithm on.
-  TIME - The time past from the creation of the directory until now.
-  DIRECTORY_SIZE - The size of all files in a single directory.
-  ALGORITHM_TIME_COEFFICIENT - The time coefficient for the algorithm.
-  ALGORITHM_SIZE_COEFFICIENT - The size coefficient for the algorithm.

### The Algorithm
**If ALGORITHM_TIME_COEFFICIENT * TIME >= 0:**
** ***Preference*** ** = (ALGORITHM_TIME_COEFFICIENT * TIME - ALGORITHM_SIZE_COEFFICIENT * DIRECTORY_SIZE) / sqrt{DIR_PLACE}

**If ALGORITHM_TIME_COEFFICIENT * TIME < 0:**
** ***Preference*** ** = (ALGORITHM_TIME_COEFFICIENT * TIME - ALGORITHM_SIZE_COEFFICIENT * DIRECTORY_SIZE) / (DIR_PLACE)^2
