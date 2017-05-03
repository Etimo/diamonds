Feature: Player's movements

    Player communicates with the server by sending relative directions
    wich are U, D, L, R.
    Each Player knows his initial coordinates.
    Player cannot move outside of the border.
    If the Player moves outside of the Playground he falls down and dies.
    Ininial position of the Player is the Base.
    If the next position of the movement is the diamond, 
    the Player takes it and moves to that position.
    If the next position is Diamond and Player can take Diamond, 
    he takes it and Diamond removed from the Playground.
    If the Player cannot take any Diamond, when he moves to the position
    with Diamond he moves to that position but does not take Diamond
    and the Diamond does not disappeare and his score does not change.
    If the next position of the movement is another player, Player does not move.
    Top left position has coordinates (0,0)


    Background: Initial position
        In order to play the Game
        As a human or robot Player
        I want to know my initial status

        Given my Position is (5,5)
        And Position of Base is (5,5) 
        And my score is 0
        And I can pick 5 Diamond
        And Playground size is 10x10

    Scenario: Movement to empty Position
        Given Position to the right is empty
        And I can pick 5 Diamond
        And my score is 0
        When I move to R 
        Then my Position is (6,5)
        And my Score now is 0
        And I can pick now 5 Diamond

    Scenario Outline: Movement outside of the Playground
        Given my Position is (<X>,<Y>)
        When I move to <direction>
        Then my score now is 0
        And Game is over
        Examples:
        | X | Y | direction|
        | 5 | 0 | D        |
        | 5 | 9 | U        |
        | 0 | 5 | L        |
        | 9 | 0 | R        |

    Scenario: Picking a Diamond
        Given Diamond at (5,6)
        And I have 0 Diamond
        And I can pick 5 Diamond
        When I move to U
        Then my Position is (5,6)
        And I have now 1 Diamond
        And I can pick now 4 Diamond
        And my score now is 0
        And Diamond removed from (5,6) 

    Scenario: Picking the last Diamond that can pick
        Given Diamond at (5,6)
        And I have 4 Diamond
        And I can pick 1 Diamond
        When I move to U
        Then I have now 5 Diamond
        And I can pick now 0 Diamond
        And Diamond removed from (5,6)

    Scenario: Cannot pick a Diamond
        Given Diamond at (5,6)
        And I have 5 Diamond
        And I can pick 0 Diamond
        And my score is 0
        When I move to U
        Then my Position now is (5,6)
        And I have now 5 Diamond
        And I can pick now 0 Diamond
        And my score now is 0
        But Diamond remains at (5,6)

    Scenario: Storing Diamond at the Base
        Given my Position is (4,5)
        And my score is 0
        And I have 1 Diamond
        And I can pick 4 Diamond
        When I move to R
        Then I have now 0 Diamond
        And my score now is 1
        And I can pick now 5 Diamond

    Scenario: Returning to Base without Diamonds
        Given my Position is (4,5)
        And my score is 0
        And I have 0 Diamond
        And I can pick 5 Diamond
        When I move to R
        Then I have now 0 Diamond
        And my score now is 0
        And I have now 0 Diamond
        And I can pick now 5 Diamond

    Scenario: Clashing with Other Player who stands on empty Position
        Given my Position is (4,5)
        And OtherPlayer at (3,5)
        When I move to L
        Then my Position remains at (4,5)
        And OtherPlayer remains at (3,5)
    
    Scenario: Clashing with Other Player who stands on Diamond
        Given my Position is (4,5)
        And OtherPlayer at (3,5)
        And Diamond at (3,5)
        And my score is 0
        And I can pick 5 Diamond
        When I move to L
        Then my Position remains at (4,5)
        And OtherPlayer remains at (3,5)
        And Diamond remains at (3,5)
        And my score now is 0
        And I can pick now 5 Diamond

    Scenario: OtherPlayer took Diamond faster
        Given my Position is (4,5)
        And I have 0 Diamond
        And I can pick 5 Diamond
        And Diamond at (3,5)
        But Diamond removed from (3,5) by OtherPlayer
        When I move to L
        Then my Position now is (3,5)
        And I have now 0 Diamond
        And I can pick now 5 Diamond

    Scenario: Picking Diamond and returning to the same Position again
        Given my Position is (4,5)
        And I have 0 Diamond
        And I can pick 5 Diamond
        And Diamond at (3,5)
        When I move to L
        And I move to U
        And I move to D
        Then my Position is (3,5)
        And I can pick now 4 Diamond
        And I have 1 Diamond