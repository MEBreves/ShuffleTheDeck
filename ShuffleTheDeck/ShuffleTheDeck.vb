'Miranda Breves
'RCET0265
'Spring 2022
'Shuffle the Deck
'https://github.com/MEBreves/ShuffleTheDeck

Option Strict On
Option Explicit On

Module ShuffleTheDeck

    'Returns a random number between the lower and upper limit values given by the user
    Function randomGenerator(ByVal lowerLimit As Integer, ByVal upperLimit As Integer) As Integer

        Randomize()
        Return CInt(Math.Floor((upperLimit - lowerLimit + 1) * Rnd())) + lowerLimit

    End Function

    'Returns true if every entry in the array is true, and false if not
    Function CheckAllCardsDealt(ByVal dealtCards(,) As Boolean) As Boolean

        For i As Integer = 0 To 3
            For j As Integer = 0 To 10
                If dealtCards(i, j) = False Then
                    Return False
                End If
            Next
        Next

        Return True

    End Function

    'Checks the user response to see if an option has been selected, and calling the option to happen if so
    Sub CheckUserInput(ByVal userInput As String, ByRef cards(,) As Boolean, ByRef exitMain As Boolean)

        Dim checkResponse As Boolean = False

        Do Until checkResponse
            If userInput.ToLower() = "q" Then
                exitMain = True
                checkResponse = True
            ElseIf userInput.ToLower() = "s" Then
                Shuffle(cards)
                Console.WriteLine("Your deck has been shuffled." & vbNewLine)
                checkResponse = True
            ElseIf userInput.Trim() = "" Then
                'Do nothing
                checkResponse = True
            Else
                Console.WriteLine("Your response was not valid. Please enter a different response.")
                userInput = Console.ReadLine()
                checkResponse = False
            End If
        Loop

    End Sub

    Sub Shuffle(ByRef cardTracker(,) As Boolean)

        For i As Integer = 0 To 3
            For j As Integer = 0 To 10
                cardTracker(i, j) = False
            Next
        Next

    End Sub

    Sub Main()

        'Declaring variables
        Dim suits As String() = {"Spades", "Clubs", "Hearts", "Diamonds"}
        Dim randomSuitIndex As Integer
        Dim cardValues As String() = {"2", "3", "4", "5", "6", "7", "8", "9", "J", "Q", "K", "A"}
        Dim randomCardIndex As Integer
        Dim dealtCards(3, 10) As Boolean
        Dim newCardFound As Boolean = False
        Dim userResponse As String
        Dim finishProgram As Boolean = False

        'Initializing the card tracker array with all false values
        Shuffle(dealtCards)

        'Letting the user know how to control the program
        Console.WriteLine("Here are your options:" & vbNewLine & vbTab &
                          "Enter: Continue" & vbNewLine & vbTab &
                          "S: Shuffle" & vbNewLine & vbTab &
                          "Q: Quit" & vbNewLine)

        Do Until finishProgram

            'Checking if the deck has been completely gone through, and if so, shuffling the deck
            If CheckAllCardsDealt(dealtCards) Then
                Shuffle(dealtCards)
                Console.WriteLine("All cards have been dealt. Shuffling the deck.")
            End If

            'Using the random generator and dealt card array to display a new card to the user
            Do Until newCardFound

                randomSuitIndex = randomGenerator(0, 3)
                randomCardIndex = randomGenerator(0, 10)

                newCardFound = Not dealtCards(randomSuitIndex, randomCardIndex) 'if the array shows false for the card, display it

            Loop

            'Clearing the loop variable so that the program does not return the same card value forever
            newCardFound = False

            'Setting the shown card value to true in the array so the user does not see it again
            dealtCards(randomSuitIndex, randomCardIndex) = True

            'Displaying the card to the user
            Console.WriteLine($"Here is a card: {cardValues(randomCardIndex)} of {suits(randomSuitIndex)}")

            'Allowing the user to make a choice to choose another card, shuffle, or quit
            userResponse = Console.ReadLine()
            CheckUserInput(userResponse, dealtCards, finishProgram)

        Loop

        'Letting the user know that the program has finished
        Console.WriteLine(vbNewLine & "Please press Enter to exit the program.")
        Console.ReadLine()

    End Sub


End Module
