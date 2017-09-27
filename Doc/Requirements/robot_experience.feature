Feature: Robot Experience
 In order to play Game
 As a robot player
 I want to know my position, location of Diamonds and OtherPlayers
 So that I could plan my movement strategy
 
    Background:
        Given I am playing the Game

    Scenario: Getting current status
        When I request current status
        Then I receive MyPosition
        And I receive locations of Diamonds
        And I receive positions of OtherPlayers

    Scenario: Getting player info
        When I request player info
        Then I receive my Score
        And I receive number of Diamonds that I picked
        And I receive number of Diamonds I can pick more

    Scenario: Sending movement
        When I send nem movement
        Then I receive MyPosition
        And I receive locations of Diamonds
        And I receive positions of OtherPlayers
        And I receive number of Diamonds that I picked
        And I receive number of Diamonds I can pick more

    Scenario: Getting player info when Game is Over
        Given Game is Over
        When I request player info
        Then I receive my Score

    Scenario: Getting current status when Game is Over
        Given Game is Over
        When I request current status
        Then I receive ErrorMessage that Game is Over

    Scenario: Trying to move when Game is Over
        Given Game is Over
        When I send nem movement
        Then I receive ErrorMessage that Game is Over
