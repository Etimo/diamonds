@authentication
Feature: Login
    In order to play game
    As a human player
    I want to login 

    Background: 
        Given I am at the InitialPage
        And I am not LoggedIn

    Scenario: Succesful Login
        Given I enter existing UserName
        And I enter correct Password
        When I submit my credentials
        Then I am redirected to WelcomePage
        And my UserName is shown at WelcomePage
        And I am now LoggedIn

    Scenario: Player with given UserName not registered
        Given I enter UserName that was not registered 
        When I submit my credentials
        Then I am redirected to InitialPage
        And ErrorMessage shows that UserName was not registered
        And UserName textbox is empty
        And Password textbox is empty
        And I am still not LoggedIn

    Scenario: Incorrect password entered
        Given I enter registered UserName 
        But the Password was not correct
        When I submit my credentials
        Then I am redirected to InitialPage
        And ErrorMessage shows that Password was not correct
        And UserName textbox contains entered UserName
        And Password textbox is empty
        And I am still not LoggedIn

    Scenario: Recover forgotten Password
        When I select RecoverPassword
        Then I am redirected to RecoverPassword page
        And I am still not LoggedIn

    Scenario: Register new Player
        When I select RegisterNewPlayer
        Then I am redirected to RegistrationForm page
        And I am still not LoggedIn

    