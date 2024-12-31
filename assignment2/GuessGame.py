import random

def isValidGuess(guess):
    return guess.isdigit() and 1 <= int(guess) <= 100

def getUserGuess():
    while True:
        userGuess = input("Guess a number between 1 and 100: ")
        if isValidGuess(userGuess):
            return int(userGuess)
        else:
            print("Invalid input. Please enter a number between 1 and 100.")

def compareGuess(guess, secretNumber):
    if guess < secretNumber:
        return "Too low. Guess again."
    elif guess > secretNumber:
        return "Too high. Guess again."
    else:
        return None  

def playGame():
    secretNumber = random.randint(1, 100)
    numberOfGuesses = 0

    while True:
        guess = getUserGuess()  
        numberOfGuesses += 1
        feedback = compareGuess(guess, secretNumber)

        if feedback:
            print(feedback)
        else:
            print("You guessed it in", {numberOfGuesses}, "guesses!")
            break  

playGame()