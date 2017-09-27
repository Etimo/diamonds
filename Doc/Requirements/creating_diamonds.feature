Feature: Creating Diamonds
    There is a limited minimum and maximum of Diamonds on the Playground at every moment.
    At the Game start, the number of Diamonds on the Playground equals to miminum.
    If the Player picks Diamond and the total number of remaining Diamonds less than 
    the given limit, the new Diamond is added automatically.
    If the number of Diamonds is within the limit, new Diamonds are added at random time and position.
    If the munber of Diamonds equals to maximum, new Diamonds are not added.

    Background: Setting Diamond Limits
        Given Minimum Number of Diamond is 20
        And Maximum Number of Diamond is 40

    Scenario: Beginning of the Game
        When Game starts
        Then Number of Diamond is now 20

    Scenario: Picking Diamond when Diamond Number is Miminum
        Given Number of Diamond is 20
        When I pick a Diamond
        Then new Diamond is added
        And Number of Diamond is now 20

    Scenario: Can add Diamond when their Number is within Limits
        Given Number of Diamond is 22
        Then new Diamond now can be added

    Scenario: Cannot add Diamond when the Number is Maximum
        Given Number of Diamond is 40
        Then new Diamond cannot be added