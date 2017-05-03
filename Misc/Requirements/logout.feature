@authentication
Feature: Logout

    Scenario: Succesful Logout
        Given I am LoggedIn
        When I select LogOut 
        Then I am not LoggedIn any more
        And Game is over
