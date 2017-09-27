@authentication
Feature: Register new player
    In order to play a game
    As a human player
    I want to register myself

    # UserName is UserEmail

    Background:
        Given I am at RegistrationForm page
        And I am not LoggedIn

    Scenario: Successful registration
        Given I enter correct UserName
        And this entered UserName was not registered before
        And I entered valid Password
        And I entered RepeatPassword correctly
        And I agreed with TermsAndConditions
        When I submit RegistrationForm
        Then I am registered in UserDatabase
        And I am redirected to WelcomePage
        And my UserName is shown at the WelcomePage
        And I am now LoggedIn

    Scenario: UserName already exists 
        Given I enter correct UserName
        But this UserName already registered
        When I submit RegistrationForm
        Then ErrorMessage shows that UserName already registered
        And I am redirected to RegistrationForm page
        And UserName textbox is empty
        And Password textbox is empty
        And RepeatPassword textbox is empty
        And accept TermsAndConditions is unchecked
        And I am still not LoggedIn

    Scenario: Valid UserName not entered
        Given I did not enter valid UserName
        When I submit RegistrationForm
        Then ErrorMessage shows that UserName is required
        And I am redirected to RegistrationForm page
        And UserName textbox is empty
        And Password textbox is empty
        And RepeatPassword textbox is empty
        And accept TermsAndConditions is unchecked
        And I am still not LoggedIn

    Scenario: Valid Password was not entered 
        Given I enter correct UserName
        But valid Password is not entered
        When I submit RegistrationForm
        Then ErrorMessage shows that Password is required
        And I am redirected to RegistrationForm page
        But UserName textbox contains entered UserName
        And Password textbox is empty
        And RepeatPassword textbox is empty
        And accept TermsAndConditions is unchecked
        And I am still not LoggedIn

    Scenario: Valid RepeadPassword was not entered 
        Given I enter correct UserName
        And valid Password is entered
        But RepeadPassword is not entered
        When I submit RegistrationForm
        Then ErrorMessage shows that RepeadPassword is not correct
        And I am redirected to RegistrationForm page
        But UserName textbox contains entered UserName
        And Password textbox is empty
        And RepeatPassword textbox is empty
        And accept TermsAndConditions is unchecked
        And I am still not LoggedIn

    Scenario: Terms and Conditions were not accepted
        Given I enter correct UserName
        And this entered UserName was not registered before
        And I entered valid Password
        And I entered RepeatPassword correctly
        But I did not agreed with TermsAndConditions
        When I submit RegistrationForm
        Then ErrorMessage shows that accepting TermsAndConditions is required
        And I am redirected to RegistrationForm page
        But UserName textbox contains entered UserName
        And Password textbox is empty
        And RepeatPassword textbox is empty
        And accept TermsAndConditions is unchecked
        And I am still not LoggedIn