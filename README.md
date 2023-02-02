# MasterMind_23
 Trying to create a mastermind game in c# for exam in the C# Course at EC Utbildning early 2023. 
 To run the program open it in Visual Studio and click the start button. 
 When you start the application there are instructions in the beginning on how to play. 
 This is a simple version of the old board game Master Mind.
 Your goal is to correctly guess a set of 4 different colors. 
 There are 8 different colors to choose between.
 You have 10 guesses.
 After each guess the game will show you how many colors are in the correct space, which of the colors are among the randomly generated 4 and which ones are not correct at all. 
 When you have a correct guess you get a green Checkmark in the position of the correct guess. If you chose the right color but it is in the wrong place you get a white X and if the color is not among the random 4 you will get a blank space. 
 The syntax to play is first letter of color you guess on. You guess on 4 colors each try and you divide the letters with an -. The colors in the game is White, Cyan, Blue, Yellow, Green, Red, Silver and Magenta.
 If you type in the wrong syntax for the guess you will get an invalid input message and get to try again. Once the game is finished, you have won or lost, the game will close. 
 Restart the game to play again. 
# Things to add in order to expand and make the game better:
 * add an intro
 * make random generator able to have multiple instances of the same color
 * add better representation of the game board, easier to see what is happening
 * add user to be able to record how many games played and record wins and losses
 * add outro
