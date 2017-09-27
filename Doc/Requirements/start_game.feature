Feature: Starting game
    In order to play Game
    As a human player
    I want to see the MyPosition and my name

Scenario: Game starts
    Given I am LoggedIn
    When Game starts
    Then MyPosition is in the Base
    And I see myself at MyPosition
    And my name is shown on the screen

