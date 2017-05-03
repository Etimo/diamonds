Feature: User Experience
 In order to play Game
 As a human player
 I want to see the Playground, Diamonds, MyPosition and OtherPlayers 

Scenario: User Experience - Playground
    Given I am playing Game
    Then I see myself at MyPosition
    And I see all Diamonds
    And I see OtherPlayers
    And I see my Base

Scenario: User Experience - User Info
    Given I am playing Game
    Then I see my Score
    And I see number of Diamonds I currently picked
    And I see number of Diamonds I can pick more

Scenario: User Experience - Game is Over
    When Game is Over
    Then I see my final Score